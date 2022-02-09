var express = require("express");
var chatRouter = require("./chat");
var chatMessageRouter = require("./chat-message");

var app = express();

app.use("/chat/", chatRouter);
app.use("/message/", chatMessageRouter);

module.exports = app;