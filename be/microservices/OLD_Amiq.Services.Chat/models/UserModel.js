var mongoose = require("mongoose");

let Schema = mongoose.Schema;

var UserSchema = new mongoose.Schema({
	_id: {type: Schema.Number, required: true},
	//userId: {type: Number, required: true},
	name: {type: String, required: true},
	surname: {type: String, required: true},
	avatarSrc: {type: String, required: true}
}, {timestamps: true});

/*// Virtual for user's full name
UserSchema
	.virtual("fullName")
	.get(function () {
		return this.firstName + " " + this.lastName;
	});*/

module.exports = mongoose.model("User", UserSchema);