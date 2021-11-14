import {UserPostService} from "../../../features/post/user-post-service";
import {GroupPostService} from "../../../features/post/group-post-service";
import {IGroupPost} from "../../../features/post/models/group-post";
import {GET_GROUP_POSTS, SET_GROUP_POSTS} from "../types/groupPostTypes";
import {
    GET_USER_POSTS,
    REMOVE_USER_POST_COMMENT,
    SET_CREATED_USER_POST_COMMENT, SET_USER_POST_COMMENTS,
    SET_USER_POSTS
} from "../types/userPostTypes";
import {IUserPost} from "../../../features/post/models/user-post";
import {StatusCodes} from "http-status-codes";
import {PostService} from "../../../features/post/post-service";
import {CLEAR_POSTS, CREATE_POST, DELETE_POST, SET_CREATED_POST} from "../types/postTypes";
import {AxiosResponse} from "axios";
import {IResponseListOf} from "../../../core/http-client/response-list-of";
import {PostCommentService} from "../../../features/post/post-comment-service";
import {
    IGroupPostComment,
    IGroupPostCommentCreation,
    IPostComment,
    IPostCommentCreation
} from "../../../features/post/models/post-comment";
import {
    REMOVE_GROUP_POST_COMMENT,
    SET_CREATED_GROUP_POST_COMMENT,
    SET_GROUP_POST_COMMENTS
} from "../types/groupPostCommentTypes";

const userPostService = new UserPostService();
const groupPostService = new GroupPostService();
const postService = new PostService();
const postCommentService = new PostCommentService();

//#region comments

export const createGroupPostComment = (data: IGroupPostCommentCreation) => (dispatch) => {
    postCommentService.createGroupPostComment(data).then(res => {
        if(res.status === StatusCodes.CREATED){
            const newComment = res.data as IGroupPostComment;
            dispatch({
                type: SET_CREATED_GROUP_POST_COMMENT,
                payload: newComment
            })
        }
    })
}

export const createUserPostComment = (data: IPostCommentCreation) => dispatch => {
    postCommentService.create(data).then(res => {
        if(res.status === StatusCodes.CREATED){
            const newComment = res.data as IPostComment;
            dispatch({
                type: SET_CREATED_USER_POST_COMMENT,
                payload: newComment
            })
        }
    })
}

export const removeGroupPostComment = (postCommentId: string) => (dispatch) => {
    postCommentService.delete(postCommentId).then(res => {
        if (res.status === StatusCodes.OK) {
            const removeComment = res.data as IPostComment;
            dispatch({
                type: REMOVE_GROUP_POST_COMMENT,
                payload: removeComment
            })
        }
    })
}

export const removeUserPostComment = (postCommentId: string) => (dispatch) => {
    postCommentService.delete(postCommentId).then(res => {
        if (res.status === StatusCodes.OK) {
            const removedComment = res.data as IPostComment;
            dispatch({
                type: REMOVE_USER_POST_COMMENT ,
                payload: removedComment
            })
        }
    })
}

export const getUserPostComments = (postId: string, page: number) => dispatch => {
    postCommentService.getPostComments(postId, page).then(res => {
        if (res.status === StatusCodes.OK) {
            const comments = res.data as IPostComment[];
            console.log(comments)
            dispatch({
                type: SET_USER_POST_COMMENTS,
                payload: {
                    postId,
                    comments
                }
            })
        }
    })
}

export const getGroupPostComments = (postId: string, page: number) => dispatch => {
    postCommentService.getPostComments(postId, page).then(res => {
        if (res.status === StatusCodes.OK) {
            const comments = res.data as IGroupPostComment[];
            dispatch({
                type: SET_GROUP_POST_COMMENTS,
                payload: {
                    postId,
                    comments
                }
            })
        }
    })
}

//#endregion

export const getGroupPosts = (groupId: number, page: number) => (dispatch) => {
    dispatch({
        type: GET_GROUP_POSTS
    });

    groupPostService.getPostsByGroupId(groupId, page).then(res => {
        const groupPosts = res.data as IResponseListOf<IGroupPost>;
        dispatch({
            type: SET_GROUP_POSTS,
            payload: groupPosts
        })
    })
}

export const createGroupPost = (data: Partial<IGroupPost>) => dispatch => {
    dispatch({
        type: CREATE_POST
    })

    groupPostService.create(data).then(res => {
        if (res.status === StatusCodes.CREATED) {
            const createdPost = res.data as IGroupPost;
            dispatch({
                type: SET_CREATED_POST,
                payload: createdPost
            })
        }
    })
}

export const getUserPosts = (userId: number, page: number, length: number) => dispatch => {
    if (page === 1) {
        dispatch({
            type: CLEAR_POSTS
        })
    }

    dispatch({
        type: GET_USER_POSTS
    })

    userPostService.getUserPosts(userId, page, length).then(({data, status}) => {
        if (status === StatusCodes.OK) {
            dispatch({
                type: SET_USER_POSTS,
                payload: data as IResponseListOf<IUserPost>
            })
        }
    })
}

export const createUserPost = (post: Partial<IUserPost>) => async dispatch => {
    dispatch({
        type: CREATE_POST
    })

    const result: AxiosResponse<IUserPost> = await userPostService.create(post)
    const {data}: { data: IUserPost } = result;

    dispatch({
        type: SET_CREATED_POST,
        payload: data
    })
}

export const deletePost = (postId: string) => dispatch => {
    /* dispatch({
         type:
     })*/

    postService.removePostById(postId).then(res => {
        if (res.status === StatusCodes.OK) {
            dispatch({
                type: DELETE_POST,
                payload: postId
            })
        }
    })
}