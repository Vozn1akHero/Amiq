
var mongoose = require("mongoose");

var Schema = mongoose.Schema;

var ChatSchema = new Schema({
	//firstUser: { type: Number, ref: "User", required: true },
	//secondUser: { type: Number, ref: "User", required: true },
	//messages: {type: Array, ref: "Message"}
	users: [{type:Number, ref: 'User'}],
	messages:  [{type:mongoose.Schema.Types.ObjectId, ref: 'Message'}]
}, {timestamps: true});

const Chat = mongoose.model("Chat", ChatSchema);

export default Chat;