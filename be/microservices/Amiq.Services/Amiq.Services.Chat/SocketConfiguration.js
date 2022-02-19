import {WebSocketServer} from "ws";
//import cookieParser from 'cookie-parser'
import {parseCookie} from "./utils.js"
import jwt from "jsonwebtoken"
import Chat from "./models/ChatModel";

let wss;

const init = (httpServer) => {
    wss = new WebSocketServer({
        server: httpServer
    });

    wss.on('connection', (ws,req)=>{
        //console.log(parseCookie(req.headers.cookie))
        let userId;
        if(req.headers.cookie)
        {
            const parsedCookie = parseCookie(req.headers.cookie)
            const authToken = parsedCookie.token;
            if(authToken){
                let decodedJwt = jwt.verify(authToken, 'kdas8dad8ah2d10123daslkd2312l213j1k31dmasdjklk123');
                if(decodedJwt){
                    userId = +decodedJwt.sub;
                }
            }
        }

        ws.room = [];
        //ws.send(JSON.stringify({msg:"user joined"}));
        console.log('connected');

        ws.on('message', async (data) => {
            console.log('received: %s', data);
            data = JSON.parse(data);

            switch(data.event){
                case "JoinGroupAsync": {
                    if(userId &&  await issuerBelongsToChat(userId, data.chatId)){
                        ws.room.push(data.chatId)
                    }
                    break;
                }
                case "LeaveGroupAsync": {
                    if(userId && await issuerBelongsToChat(userId, data.chatId)){
                        ws.room = ws.room.filter(x=>x !== data.chatId)
                    }
                    break;
                }
            }
        });

        ws.on('close',(e)=>console.log('websocket closed'+e))

        ws.on('error',e=>console.log(e))
    })
}

const issuerBelongsToChat = async (userId, chatId) => {
    return await Chat.exists({
        _id: chatId,
        users: userId
    })
}

const broadcast = (message) => {
    wss.clients.forEach(client=>{
        const room = JSON.parse(message).room;
        if(client.room.indexOf(room) !== -1)
            client.send(message)
    })
}

export {wss, init, broadcast}