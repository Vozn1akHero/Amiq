require('dotenv').config();
import express from "express";
import mongoose from "mongoose";
import bodyParser from "body-parser";
import cors from "cors";
import initializeRabbitMQListener from "./amqp/RabbitMQListener";

const app = express();
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
app.use('/message', chatMessageRoute);

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

const server = app.listen(PORT, () => console.log(`Server running on port ${PORT}`));

export default server;

