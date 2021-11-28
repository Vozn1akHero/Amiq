import {IGroupPost} from "../../../features/post/models/group-post";
import {SET_GROUP_POSTS} from "../types/groupPostTypes";
import {
    REMOVE_USER_POST_COMMENT,
    SET_CREATED_USER_POST_COMMENT,
    SET_USER_POST_COMMENTS,
    SET_USER_POSTS
} from "../types/userPostTypes";
import {CLEAR_POSTS, DELETE_POST, GET_POSTS, SET_CREATED_POST} from "../types/postTypes";
import {IUserPost} from "../../../features/post/models/user-post";
import {IResponseListOf} from "../../../core/http-client/response-list-of";
import {
    REMOVE_GROUP_POST_COMMENT,
    SET_CREATED_GROUP_POST_COMMENT,
    SET_GROUP_POST_COMMENTS
} from "../types/groupPostCommentTypes";
import {IPost} from "../../../features/post/models/post";
import {IGroupPostComment, IPostComment} from "../../../features/post/models/post-comment";

type PostReducer = {
    //groupPosts: Array<IGroupPost>,
    //groupPostsLoaded: boolean
    postsLoaded: boolean,
    posts: Array<IGroupPost> | Array<IUserPost>,
    postsLength: number,
    nextPage: number
}

const initialState : PostReducer = {
    postsLoaded: false,
    posts: [],
    postsLength: 0,
    nextPage: 1
}

export default function(state:PostReducer = initialState, action) {
    switch (action.type) {
        case GET_POSTS:
            return {
                ...state,
                //postsLoaded: true
            }
        case SET_GROUP_POSTS: {
            const data = action.payload as IResponseListOf<IGroupPost>;
            return {
                ...state,
                postsLoaded: true,
                posts: data.entities,
                postsLength: data.length,
                nextPage: state.nextPage + 1
            }
        }
        case SET_USER_POSTS:
        {
            const data = action.payload as IResponseListOf<IUserPost>;
            return {
                ...state,
                postsLoaded: true,
                posts: [...state.posts, ...data.entities],
                postsLength: data.length,
                nextPage: state.nextPage + 1
            }
        }
        case DELETE_POST:
            return {
                ...state,
                posts: [...state.posts.filter(e=>e.postId !== action.payload)],
                postsLength: state.postsLength - 1
            }
        case SET_CREATED_POST:
            return {
                ...state,
                posts: [action.payload, ...state.posts],
                postsLength: state.postsLength + 1
            }
        case CLEAR_POSTS: {
            return {
                ...state,
                postsLoaded: false,
                posts: [],
                postsLength: 0,
                nextPage: 1
            }
        }
        case REMOVE_GROUP_POST_COMMENT: {
            return {
                ...state,
                posts: [...state.posts.map((groupPost) => {
                    if (groupPost.postId === action.payload.postId) {
                        if (action.payload.parentCommentId) {
                            groupPost.comments = groupPost.comments.map(comment => {
                                if (comment.commentId === action.payload.parentCommentId) {
                                    //comment.children = [...comment.children.filter(value => value.commentId !== action.payload.commentId)]
                                    comment.children = [...comment.children.map(value => {
                                        if(value.commentId === action.payload.commentId){
                                            value.isRemoved = true;
                                        }
                                        return value;
                                    })]
                                }
                                return comment;
                            })
                        } else {
                            groupPost.comments = groupPost.comments.map(value => {
                                if(value.commentId === action.payload.commentId){
                                    value.isRemoved = true;
                                }
                                return value;
                            })
                        }
                    }
                    return groupPost;
                })]
            }
        }
        case SET_CREATED_GROUP_POST_COMMENT:{
            return {
                ...state,
                posts: [...state.posts.map((value) => {
                    if(value.postId === action.payload.postId){
                        if(action.payload.parentCommentId){
                            value.comments = value.comments.map(comment => {
                                if(comment.commentId === action.payload.parentCommentId){
                                    comment.children = [...comment.children, action.payload]
                                    console.log(action.payload, comment.children)
                                }
                                return comment;
                            })
                        } else {
                            const comments = value.comments == null ? [] : value.comments;
                            value.comments = [...comments, action.payload]
                        }
                    }
                    return value;
                })]
            }
        }
        case SET_CREATED_USER_POST_COMMENT:{
            return {
                ...state,
                posts: [...state.posts.map((value) => {
                    if(value.postId === action.payload.postId){
                        if(action.payload.parentCommentId){
                            value.comments = value.comments.map(comment => {
                                if(comment.commentId === action.payload.parentCommentId){
                                    comment.children = [...comment.children, action.payload]
                                }
                                return comment;
                            })
                        } else {
                            const comments = value.comments == null ? [] : value.comments;
                            value.comments = [...comments, action.payload]
                        }
                    }
                    return value;
                })]
            }
        }
        case REMOVE_USER_POST_COMMENT: {
            return {
                ...state,
                posts: [...state.posts.map((userPost) => {
                    if (userPost.postId === action.payload.postId) {
                        if (action.payload.parentCommentId) {
                            userPost.comments = userPost.comments.map(comment => {
                                if (comment.commentId === action.payload.parentCommentId) {
                                    //comment.children = [...comment.children.filter(value => value.commentId !== action.payload.commentId)]
                                    comment.children = [...comment.children.map(value => {
                                        if(value.commentId === action.payload.commentId){
                                            value.isRemoved = true;
                                        }
                                        return value;
                                    })]
                                }
                                return comment;
                            })
                        } else {
                            userPost.comments = userPost.comments.map(value => {
                                if(value.commentId === action.payload.commentId){
                                    value.isRemoved = true;
                                }
                                return value;
                            })
                        }
                    }
                    return userPost;
                })]
            }
        }
        case SET_USER_POST_COMMENTS: {
            const comments = action.payload.comments as IResponseListOf<IPostComment>;
            return {
                ...state,
                posts: state.posts.map((userPost) => {
                    if (userPost.postId === action.payload.postId) {
                        userPost.comments = userPost.comments ? [...userPost.comments, ...comments.entities] : comments.entities
                    }
                    return userPost;
                })
            }
        }
        case SET_GROUP_POST_COMMENTS: {
            const comments = action.payload.comments as IResponseListOf<IGroupPostComment>;
            return  {
                ...state,
                posts: state.posts.map((groupPost) => {
                    if (groupPost.postId === action.payload.postId) {
                        groupPost.comments = groupPost.comments ? [...groupPost.comments, ...comments.entities] : comments.entities
                    }
                    return groupPost;
                })
            }
        }
        default:
            return state;
    }
}