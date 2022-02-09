const { body,validationResult } = require("express-validator");
//import { body,validationResult } from "express-validator"
const { sanitizeBody } = require("express-validator");
const apiResponse = require("../helpers/apiResponse");
var mongoose = require("mongoose");
var MessageSchema =  require("../models/MessageModel");
var ChatSchema =  require("../models/ChatModel");

exports.search = [
	(res,req) => {
		const text = req.query.text;


	}
]

exports.preview = [
	(res,req) => {

	}
]