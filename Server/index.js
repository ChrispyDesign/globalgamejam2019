var express = require('express');
var uuid = require('uuid');
var fs = require('file-system');

var app = express();
var server = require('http').Server(app);

server.listen(80);

function randomString() {
	return uuid();
}

var secret = '';
if(fs.existsSync('super_secret_password.txt')) {
	secret = fs.readFileSync('super_secret_password.txt');
}

var currentId = randomString();
console.log('current id: ' + currentId);

app.get('/', function(req,res)  {
	if(req.query.id === currentId) {
		res.sendFile(__dirname + '/index.html');
	} else {
		res.sendFile(__dirname + '/nope.html');
	}
});

app.get('/TemplateData/*', function(req,res) {
	var file = req.params[0];
	res.sendFile(__dirname + '/TemplateData/' + file);
});
app.get('/Build/*', function(req,res) {
	var file = req.params[0];
	res.sendFile(__dirname + '/Build/' + file);
});

app.get('/update/', function(req, res) {
	res.sendFile(__dirname + '/nope.html');
	if(req.query.id !== currentId)
		return;
	currentId = randomString();
	console.log('new id: ' + currentId);

	fs.writeFile('gamestate.json', req.query.data, function(err){});
	console.log('updated game state');
});

app.get('/getstate/', function(req, res) {
	res.sendFile(__dirname + '/gamestate.json');
});

app.get('/getcode/', function(req, res) {
	// secret is required to get the code
	//if(req.query.secret !== secret)
		//return;
	res.send(currentId);
});

app.get('/sethandle/', function(req, res) {
	fs.writeFile('handle.txt', req.query.handle);
	res.sendFile(__dirname + '/handle.txt');
});

app.get('/gethandle/', function(req, res){
	res.sendFile(__dirname + '/handle.txt');
});
