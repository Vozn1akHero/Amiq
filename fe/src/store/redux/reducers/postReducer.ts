import {IGroupPost} from "../../../features/post/models/group-post";
import {SET_GROUP_POSTS} from "../types/groupPostTypes";
import {SET_USER_POSTS} from "../types/userPostTypes";
import {CLEAR_POSTS, DELETE_POST, GET_POSTS, SET_CREATED_POST} from "../types/postTypes";
import {IUserPost} from "../../../features/post/models/user-post";
import {IResponseListOf} from "../../../core/http-client/response-list-of";
import {REMOVE_GROUP_POST_COMMENT, SET_CREATED_GROUP_POST_COMMENT} from "../types/groupPostCommentTypes";

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
                                    comment.children = [...comment.children.filter(value => value.commentId !== action.payload.commentId)]
                                }
                                return comment;
                            })
                        } else {
                            groupPost.comments = groupPost.comments.filter(value => value.commentId !== action.payload.commentId)
                        }
                    }
                })]
            }
        }
        case SET_CREATED_GROUP_POST_COMMENT:{
            return {
                ...state,
                posts: [...state.posts.map((value, index) => {
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
                            value.comments = [action.payload, ...comments]
                        }
                    }
                    return value;
                })]
            }
        }
        default:
            return state;
    }
}