/*const { body,validationResult } = require("express-validator");
const { sanitizeBody } = require("express-validator");
const apiResponse = require("../helpers/apiResponse");*/

import express from 'express';
import Message from "../models/MessageModel";
import Chat from "../models/ChatModel";
import User from "../models/UserModel";

const router  = express.Router();

router.post("", async (req, res) => {
	const { chatId, textContent, authorId } = req.body;
	const message = { textContent, author: authorId };

	let createdMessage = await Message.create(message);
	createdMessage = await Message.populate(createdMessage, 'author');

	Chat.findOneAndUpdate(
		{ _id: chatId },
		{ $push: { messages: createdMessage._id } }
	);
	//const createdMessage = await Message.create(message);

	const author = createdMessage.author;

	return res.status(201).json({
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
		//receiver:
	})
});

router.delete("/:messageId", async (req, res) => {
	const messageId = req.params.messageId;
	const doc = await Message.findOneAndDelete(messageId);

	return res.status(200).json({
		isBusinessException: false,
		businessException: false,
		entities: doc
	})
});

router.get("/list-by-chat",  async (req, res) => {
	const { page, count, chatId } = req.query;

	const {messages} = await Chat.findOne({_id: chatId}, 'messages');
	const length = messages.length;

	let entities = await Chat.find({_id: chatId})
		.populate({path: 'messages', model: 'Message'})
		//.populate({path: 'firstUser', model: 'User'})
		//.populate({path: 'secondUser', model: 'User'})
		.skip((page-1)*count).limit(+count);

	return res.status(200).json({
		entities,
		length
	});
});

router.delete("/list", (req, res) => {
	const messageIds = req.body.messageIds;
});

module.exports = router;