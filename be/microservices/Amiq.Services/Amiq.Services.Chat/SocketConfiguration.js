import {WebSocketServer} from "ws";
import {parseCookie} from "./utils.js"
import {verifyTokenAndReturnUserId} from "./auth/jwt-extensions"
import Chat from "./models/ChatModel";

let webSocketServer;

const init = (httpServer) => {
    webSocketServer = new WebSocketServer({
        server: httpServer
    });

    webSocketServer.on('connection', (socket, req) => {
        let userId;
        if(req.headers.cookie)
        {
            const parsedCookie = parseCookie(req.headers.cookie)
            const authToken = parsedCookie.token;
            userId = verifyTokenAndReturnUserId(authToken);
        }

        socket.groups = [];

        socket.on('message', async (data) => {
            data = JSON.parse(data);

            switch(data.event){
                case "JoinGroupAsync": {
                    if(userId &&  await issuerBelongsToChat(userId, data.chatId)){
                        socket.groups.push(data.chatId)
                    }
                    break;
                }
                case "LeaveGroupAsync": {
                    if(userId && await issuerBelongsToChat(userId, data.chatId)){
                        socket.groups = socket.groups.filter(x=>x !== data.chatId)
                    }
                    break;
                }
            }
        });
    })
}

const issuerBelongsToChat = async (userId, chatId) => {
    return await Chat.exists({
        _id: chatId,
        users: userId
    })
}

const broadcast = (message) => {
    webSocketServer.clients.forEach(client=>{
        const group = JSON.parse(message).group;
        if(client.groups.indexOf(group) !== -1)
            client.send(message)
    })
}

export {webSocketServer, init, broadcast}