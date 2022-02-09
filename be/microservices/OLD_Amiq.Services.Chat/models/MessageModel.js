var mongoose = require("mongoose");

var Schema = mongoose.Schema;

var MessageSchema = new Schema({
	chat: { type: Schema.ObjectId, ref: "Chat", required: true },
	textContent: { type: String, required: true },
	createdAt: {type: Date},
	author: { type: Number, ref: "User", required: true },
	receiver: { type: Number, ref: "User", required: true },
	isReadByReceiver: {type: Boolean, default: false}
}, {timestamps: true});

module.exports = mongoose.model("Message", MessageSchema);