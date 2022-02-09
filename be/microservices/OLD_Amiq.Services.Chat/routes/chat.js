var express = require("express");
const ChatController = require("../controllers/ChatController");

var router = express.Router();

router.get("/preview", ChatController.search);
router.get("/search", ChatController.preview);

module.exports = router;