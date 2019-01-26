using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TweetSharp;
using System.IO;
using System.Net;

namespace TeamFingerGuns_GGJ
{
    class Program
    {
        public static string customer_key = "MST4X6tD1aS0NbYjAcSziGllE";
        public static string customer_key_secret = "fUEH4c6DHB2U84PXjSAhgUX4sppsvj73NxNMC9CEdpxh5pLBrW";
        public static string access_token = "3008164495-zwSuzmfJUCWbQx8oVKZRBZTP01eLKoXEPe5zZfO";
        public static string access_token_secret = "wgUpq1dCC09PyLNhyOxdLJpnlcU3CpfsB35sR2T8nfoz0";
        public static List<TwitterStatus> m_retweetList = new List<TwitterStatus>();
        public static int m_currentUser = 0;
        public static long m_gameTweetID = 0;
        public static string urlToSend;
        public static long idnum = 0;

        static TwitterService service = new TwitterService(
            customer_key, 
            customer_key_secret, 
            access_token, 
            access_token_secret);

        static void Main(string[] args)
        {
            Console.WriteLine($"<{DateTime.Now}> - Bot Started!");

            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(0.3f);

            if (m_gameTweetID == 0)
                m_gameTweetID = SendTweetWithPic("Game has started, retweet this to not be on my list");


            var timer = new System.Threading.Timer((e) =>
            {
                GetPlayers();
                GetURL();

                if (m_currentUser < m_retweetList.Count)
                {
                    if (m_retweetList.Count > 0)
                    {
                        SendDM(m_retweetList[m_currentUser].User.Id, urlToSend);

                        m_currentUser++;
                    }
                }

            }, null, startTimeSpan, periodTimeSpan);

            Console.Read();
        }

        private static void GetURL()
        {
            WebClient wc = new WebClient();

            Stream data = wc.OpenRead("http://ggj.fsh.zone/getcode");
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();

            urlToSend = "http://ggj.fsh.zone/?id=" + s;
        }

        static long SendTweetWithPic(string TweetText)
        {
            using (var webClient = new WebClient())
            {
                webClient.DownloadFile(
                    new Uri("https://media.discordapp.net/attachments/529114987717853196/538551571944964097/unknown.png"),
                    "1.jpg");

                Stream stream = webClient.OpenRead("1.jpg");

                MediaFile media = new MediaFile();
                media.Content = stream;
                service.UploadMedia(new UploadMediaOptions() { Media = media }, (uploadMedia, response) => 
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"<{DateTime.Now}> - Media Uploaded!");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"<{DateTime.Now}> - Media Not Uploaded! " + response.Errors);
                        Console.ResetColor();
                    }

                    service.SendTweet(new SendTweetOptions { Status = TweetText, MediaIds = new string[] { uploadMedia.Media_Id } }, (tweet, response2) =>
                    {
                        if (response2.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"<{DateTime.Now}> - Tweet Sent!");
                            Console.ResetColor();

                            idnum = tweet.Id;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"<{DateTime.Now}> - Tweet Not Sent! " + response.Errors);
                            Console.ResetColor();
                        }
                    });
                });               
            }
            return idnum;
        }

        static long SendTweet(string TweetText)
        {
            service.SendTweet(new SendTweetOptions { Status = TweetText }, (tweet, response) =>
             {
                 if(response.StatusCode == System.Net.HttpStatusCode.OK)
                 {
                     Console.ForegroundColor = ConsoleColor.Green;
                     Console.WriteLine($"<{DateTime.Now}> - Tweet Sent!");
                     Console.ResetColor();

                     idnum = tweet.Id;
                 }
                 else
                 {
                     Console.ForegroundColor = ConsoleColor.Red;
                     Console.WriteLine($"<{DateTime.Now}> - Tweet Not Sent! " + response.Errors);
                     Console.ResetColor();
                 }
             });

            return idnum;
        }

        static void SendDM(long id, string msg)
        {
            SendDirectMessageOptions msgOpt = new SendDirectMessageOptions();
            msgOpt.Recipientid = id;
            msgOpt.Text = msg;
            var result = service.SendDirectMessage(msgOpt, (DirectMessage, response) => 
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"<{DateTime.Now}> - DM Sent!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<{DateTime.Now}> - DM Not Sent!");
                    Console.WriteLine(response.StatusCode + " " + response.StatusDescription + " " + response.Error);
                    Console.ResetColor();
                }
            });
        }

        static void GetPlayers()
        {
            if (m_gameTweetID == 0)
            {
                RetweetsOptions retweets = new RetweetsOptions();
                retweets.Id = idnum;
                retweets.Count = 100;
                var result = service.Retweets(retweets, (Retweets, response) =>
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"<{DateTime.Now}> - List Gotten!");
                        
                        foreach(TwitterStatus status in (List<TwitterStatus>)Retweets)
                        {
                            if (!m_retweetList.Contains(status))
                            {
                                m_retweetList.Add(status);
                            }
                        }

                        foreach (TwitterStatus l in m_retweetList)
                            Console.WriteLine(l.User.Id);

                        Console.WriteLine(m_retweetList.Count);

                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"<{DateTime.Now}> - List Not Gotten!");
                        Console.WriteLine(response.StatusCode + " " + response.StatusDescription + " " + response.Error);
                        Console.ResetColor();
                    }
                });
            }
        }
    }
}
