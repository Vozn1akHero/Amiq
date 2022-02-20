import axios from "axios";

const isFriend =  (userId, cookie) => {
    return  axios.get(`/api/friendship/friendship-status`, {
        params: {
            userId
        },
        baseURL: process.env.FRIENDSHIP_SERVICE_DEV,
        headers: {
            cookie
        }
    })
}

export {isFriend}