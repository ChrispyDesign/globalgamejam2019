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
        public static string customer_key = "ZI1YT2bZYLuHpni6j0yuJu67F";
        public static string customer_key_secret = "1Yyr6FtX73Gd6RNCmySSV2zkMrKrZTnToiE5Wz3NAVlsiaRho4";
        public static string access_token = "767220787-0lDesDaXn2RkZpmEORskO0JhR7YOXiDkeOOrgAAA";
        public static string access_token_secret = "f7uWd2G98mlMgE05uA1mKIwvdkS3LODKn6p8CemV5PJbs";
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
                m_gameTweetID = SendTweet("Game has started, retweet this to get on the list");


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
                retweets.Count = 1;
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
