var mongoose = require("mongoose");

var Schema = mongoose.Schema;

var UserSchema = new Schema({
	_id: {type: Number, required: true},
	//userId: {type: Number, required: true},
	name: {type: String, required: true},
	surname: {type: String, required: true},
	avatarPath: {type: String},
	//chats: { type: Array, ref: "Chat" }
}, {timestamps: true});

/*// Virtual for user's full name
UserSchema
	.virtual("fullName")
	.get(function () {
		return this.firstName + " " + this.lastName;
	});*/

const User = mongoose.model("User", UserSchema, "users");

export default User;