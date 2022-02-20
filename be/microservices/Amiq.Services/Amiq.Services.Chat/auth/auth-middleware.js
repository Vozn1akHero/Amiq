import jwt from "jsonwebtoken";
import {parseCookie} from "../utils";
import {verifyTokenAndReturnUserId} from "./jwt-extensions";

module.exports = (req, res, next) => {
    let userId;
    if(req.headers.cookie)
    {
        const parsedCookie = parseCookie(req.headers.cookie)
        const authToken = parsedCookie.token;
        userId = verifyTokenAndReturnUserId(authToken);
    }

    if (!userId) {
        return res.status(401)
    } else {
        req.userId = userId;

        next();
    }
};