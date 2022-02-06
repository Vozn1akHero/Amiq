var mongoose = require("mongoose");

var Schema = mongoose.Schema;

var MessageSchema = new Schema({
	textContent: { type: String, required: true },
	createdAt: {type: Date, required: true, default: Date.now() },
	author: { type: Number, ref: "User", required: true },
	//author: { type: Number, ref: "User", required: true },
	//receiver: { type: Number, ref: "User", required: true },
	isReadByReceiver: {type: Boolean, default: false}
}, {timestamps: true});

const Message = mongoose.model("Message", MessageSchema);

export default Message;