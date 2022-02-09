var express = require("express");
const ChatMessageController = require("../controllers/ChatMessageController");
const MessageSchema = require("../models/MessageModel");

var router = express.Router();

router.post("", ChatMessageController.create);
router.delete("/:messageId", ChatMessageController.delete);

router.get("/list-by-chat",  (res, req) => {
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

});

router.delete("/list", ChatMessageController.deleteMessages);

module.exports = router;