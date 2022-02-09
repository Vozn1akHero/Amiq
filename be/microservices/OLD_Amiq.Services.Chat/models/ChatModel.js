var mongoose = require("mongoose");

var Schema = mongoose.Schema;

var ChatSchema = new Schema({
	firstUser: { type: Schema.ObjectId, ref: "User", required: true },
	secondUser: { type: Schema.ObjectId, ref: "User", required: true },
}, {timestamps: true});

module.exports = mongoose.model("Chat", ChatSchema);