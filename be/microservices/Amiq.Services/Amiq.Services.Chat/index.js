require('dotenv').config();
import express from "express";
import mongoose from "mongoose";
import bodyParser from "body-parser";
import cors from "cors";
import initializeRabbitMQListener from "./messaging/RabbitMQListener";
import {init} from "./SocketConfiguration";
import { createServer } from "http";
import { Server } from "socket.io";
import WebSocket, { WebSocketServer } from 'ws';
const app = express();

const httpServer = createServer(app);
/*const io = new Server(httpServer, {
    // ...
});*/

/*const wss = new WebSocketServer({
    server: httpServer
});*/

const PORT = process.env.PORT || "4000";
const db = `mongodb://host.docker.internal:27017`;
let options = {
    useNewUrlParser: true,
    useUnifiedTopology: true,
    user: "mongoadmin",
    pass: "secret",
    dbName: "amiqChat"
};

const corsOptions = {
    origin: '*',
    optionsSuccessStatus: 200
};

app.use(bodyParser.urlencoded({
    extended: true
}));
app.use(bodyParser.json());
app.use(cors(corsOptions));

//const chatMessageRoute = require('./controllers/ChatMessageController');
import chatMessageRoute from './controllers/ChatMessageController';
app.use('/chat-message', chatMessageRoute);

//const chatRoute = require('./controllers/ChatController');
import chatRoute from './controllers/ChatController'
app.use('/chat', chatRoute);

mongoose
    .connect(
        db,
        options
    )
    .then(() => console.log("MongoDB connected"))
    .catch(err => console.log(err));

initializeRabbitMQListener();

//const server = app.listen(PORT, () => console.log(`Server running on port ${PORT}`));
/*io.on("connection", function (socket) {
    console.log("Socket connection");

    socket.on("JoinGroupAsync", chatId => {
        socket.join(chatId)
    })
    socket.on("RemoveFromGroupAsync", chatId => {
        socket.leave(chatId)
    })
});*/

//global.io = io;

//global.wss = wss;

init(httpServer);

httpServer.listen( PORT, function() {
    console.log("listening on *:" + PORT );
});

///SocketConfiguration.init(server, PORT+1);

//global.io = io;



//export default server;

