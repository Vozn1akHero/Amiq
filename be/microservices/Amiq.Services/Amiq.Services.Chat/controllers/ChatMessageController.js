import express from 'express';
import Message from "../models/MessageModel";
import Chat from "../models/ChatModel";
import User from "../models/UserModel";
import {isBlocked} from "../services/user-service"
import {broadcast} from "../SocketConfiguration";
import mongoose from "mongoose"
import {isFriend} from "../services/friendship-service.js"
import {StatusCodes} from "http-status-codes";


const router  = express.Router();

router.post("", async (req, res) => {
	const { chatId, textContent, authorId, receiverId } = req.body;

	if(authorId !== req.userId)
		return res.status(StatusCodes.FORBIDDEN);

	try{
		const isBlockedRes = (await isBlocked(receiverId, req.headers.cookie)).data;
		const isFriendRes = (await isFriend(receiverId, req.headers.cookie)).data.isIssuerFriend;
		if(isBlockedRes || !isFriendRes)
			return res.status(StatusCodes.FORBIDDEN);
	}
	catch {
		return res.status(StatusCodes.INTERNAL_SERVER_ERROR);
	}

	const message = { textContent, author: authorId };

	let createdMessage = await Message.create(message);
	createdMessage = await Message.populate(createdMessage, 'author');

	const chat = await Chat.findOneAndUpdate(
		{ _id: new mongoose.Types.ObjectId(chatId) },
		{ $push: { messages: createdMessage._id } },
		{ useFindAndModify: false, populate: { path: 'users', model: 'User' } }
	);

	const author = createdMessage.author;
	const receiver = chat.users.find(x=>x._id !== author._id);

	const msgDto = {
		messageId: createdMessage._id,
		chatId,
		textContent,
		createdAt: createdMessage.createdAt,
		author: {
			userId: author._id,
			name: author.name,
			surname: author.surname,
			avatarPath: author.avatarPath
		},
		receiver: {
			userId: receiver._id,
			name: receiver.name,
			surname: receiver.surname,
			avatarPath: receiver.avatarPath
		},
		files: []
	};

	broadcast(JSON.stringify({
		group: chatId.toString(),
		body: msgDto,
		event: "PushMessage"
	}));

	return res.status(StatusCodes.CREATED).json(msgDto)
});

router.delete("/:messageId", async (req, res) => {
	const messageId = req.params.messageId;
	const doc = await Message.findOneAndDelete(messageId);

	return res.status(StatusCodes.OK).json({
		isBusinessException: false,
		businessException: false,
		entities: doc
	})
});

router.get("/list-by-chat",  async (req, res) => {
	const { page, count, chatId } = req.query;

	const {messages} = await Chat.findOne({_id: chatId}, 'messages');
	const length = messages.length;

	let result = await Chat.findOne({_id: chatId})
		.populate({path: 'messages', model: 'Message', options: { sort: { 'createdAt': -1 } }})
		.populate({path: 'users', model: 'User'})
		//.populate({path: 'secondUser', model: 'User'})
		.skip((page-1)*count)
		.limit(+count);

	let mappedEntitiesToDto = [];

	const fUser =  {
		userId: result.users[0]._id,
		name: result.users[0].name,
		surname: result.users[0].surname,
		avatarPath: result.users[0].avatarPath
	}

	const sUser =  {
		userId: result.users[1]._id,
		name: result.users[1].name,
		surname: result.users[1].surname,
		avatarPath: result.users[1].avatarPath
	}

	for(const message of result.messages){
		const ent = {
			messageId: message._id,
			chatId,
			textContent: message.textContent,
			createdAt: message.createdAt,
			author: fUser.userId === message.author ? fUser: sUser,
			receiver: fUser.userId !== message.author ? fUser: sUser,
		};
		mappedEntitiesToDto.push(ent);
	}

	return res.status(StatusCodes.OK).json({
		entities: mappedEntitiesToDto,
		length
	});
});

router.delete("/list", (req, res) => {
	const messageIds = req.body.messageIds;
});

router.get("/test-ws", (req,res) => {
	//wss.emit("HELLO WORLD");

	/*wss.emit("pushMessage", JSON.stringify({
		room: "1",
		body: {
			text: "testdsad",
			chatId: 1
		},
		event: "PushMessage"
	}))
	*/

	broadcast(JSON.stringify({
		group: "1",
		body: {
			text: "testdsad",
			chatId: 1
		},
		event: "PushMessage"
	}));

	return res.status(StatusCodes.OK);
})

module.exports = router;