import express from 'express';
import Message from "../models/MessageModel";
import Chat from "../models/ChatModel";
import User from "../models/UserModel";

const router = express.Router();

router.get("/previews", async (req, res, next) => {
    const userId = +req.query.userId;

    /*const entities = await Chat
        /!*.find({"$or": [
            {"firstUser": DBRef("User", ObjectId(userId))},
            {'secondUser': DBRef("User", ObjectId(userId))}
        ]})*!/
        /!*.find({"$or": [
                {"firstUser._id": userId},
                {'secondUser._id': userId}
        ]})*!/
        .find({
            "$or": [
                {"firstUser": userId},
                {'secondUser': userId}
            ]
        })
        .populate({path: 'messages', model: 'Message', perDocumentLimit: 1})
        .populate({path: 'firstUser', model: 'User'})
        .populate({path: 'secondUser', model: 'User'});*/
    /*const entities = await User.find({_id: userId})
        .populate({path: 'chats', model: 'Chat',
            populate: {
                path: 'messages',
                model: 'Message',
                perDocumentLimit: 1
            }})*/

    const entities = await Chat.find({'users': {"$in": [userId]}})
        .populate(
            {
                path: 'users',
                model: 'User'
            }
        )
        .populate(
            {
                path: 'messages',
                model: 'Message',
                perDocumentLimit: 1,
                populate: {
                    path: 'author',
                    model: 'User'
                }
            }
        )

    let result = []

    for(const chat of entities){
        const message = chat.messages[0];
        const interlocutor = chat.users.filter(e=>e._id !== userId)[0];

        result.push({
            chatId: chat._id,
            messageId: message._id,
            author: {
                userId:  message.author._id,
                name: message.author.name,
                surname: message.author.surname,
                avatarPath: message.author.avatarPath,
            },
            textContent: message.textContent,
            interlocutor: {
                userId:  interlocutor._id,
                name: interlocutor.name,
                surname: interlocutor.surname,
                avatarPath: interlocutor.avatarPath,
            },
            date: message.createdAt,
            writtenByIssuer: message.author._id === userId
        })
    }

    return res.status(200).json(result)
});

router.get("/search", async (req, res) => {
    const text = req.query.text;
    const userId = req.query.userId;

    let result = [];

    const chats = await Chat.find({'users': {"$in": [+userId]}})
        .populate(
            {
                path: 'users',
                model: 'User'
            }
        )
        .populate(
            {
                path: 'messages',
                model: 'Message',
                perDocumentLimit: 1,
                populate: {
                    path: 'author',
                    model: 'User'
                }
            }
        )

    for(const chat of chats){
        const users = chat.users.filter(e=>e._id !== +userId);
        if(users.find(e=>String.prototype.concat(e.name, ' ', e.surname).includes(text))){
            result.push(chat);
        }
    }

    return res.status(200).json(result)
});

module.exports = router;