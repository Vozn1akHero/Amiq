import axios from "axios";

export const isBlocked = (userId, cookie) => {
    return axios.get(`/api/user/is-blocked`, {
        params: {
            userId
        },
        baseURL: process.env.USER_SERVICE_DEV,
        headers: {
            cookie
        }
    })
}

