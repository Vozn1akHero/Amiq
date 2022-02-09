const { body,validationResult } = require("express-validator");
const { sanitizeBody } = require("express-validator");
const apiResponse = require("../helpers/apiResponse");
var mongoose = require("mongoose");
var MessageSchema =  require("../models/MessageModel");


exports.listByChat = [
	(res, req) => {
		const { page, count, chatId } = req.query;

		/*const page = req.query.page;
		const count = req.query.count;
		const chatId = req.query.chatId;*/

		MessageSchema.find({_id: chatId}).skip((page-1)*count).limit(count).then(messages => {
			return res.status(200).json({
				entities: messages,
				length: MessageSchema.find({_id: chatId}).count()
			});
		})

	}
];

exports.delete = [
	(res, req) => {
		const messageId = req.params.messageId;
		MessageSchema.findOneAndDelete(messageId).then(doc => {
			return res.status(200).json({
				isBusinessException: false,
				businessException: false,
				entities: doc
			})
		})
	}
]

exports.create = [
	(res, req) => {
		const _id = req.params.messageId;
		MessageSchema.remove({_id})
	}
]

exports.deleteMessages = [
	(res, req) => {
		const messageIds = req.body.messageIds;
	}
]