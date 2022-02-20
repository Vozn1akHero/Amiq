import jwt from "jsonwebtoken";

const verifyTokenAndReturnUserId = token => {
    let userId;

    if(token){
        let decodedJwt = jwt.verify(token, 'kdas8dad8ah2d10123daslkd2312l213j1k31dmasdjklk123');
        if(decodedJwt){
            userId = +decodedJwt.sub;
        }
    }

    return userId;
}

export {verifyTokenAndReturnUserId}