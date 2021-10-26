
import {PostCommentService} from "features/post/post-comment-service";
import {CREATE_GROUP_POST_COMMENT, GET_GROUP_POST_COMMENTS} from "../types/groupPostCommentTypes";

const postCommentService = new PostCommentService();

export const getPostComments = (postId: string, page: number) => (dispatch) => {
    dispatch({
        type: GET_GROUP_POST_COMMENTS
    })


}

export const addPostComment = () => (dispatch) => {
    dispatch({
        type: CREATE_GROUP_POST_COMMENT
    })


}