import {IGroupPost} from "../../../features/post/models/group-post";
import {SET_GROUP_POSTS} from "../types/groupPostTypes";
import {SET_USER_POSTS} from "../types/userPostTypes";
import {DELETE_POST, GET_POSTS, SET_CREATED_POST} from "../types/postTypes";
import {IUserPost} from "../../../features/post/models/user-post";

type PostReducer = {
    //groupPosts: Array<IGroupPost>,
    //groupPostsLoaded: boolean
    postsLoaded: boolean,
    posts: Array<IGroupPost> | Array<IUserPost>
}

const initialState : PostReducer = {
    //groupPosts: [],
    //groupPostsLoaded: false
    postsLoaded: false,
    posts: []
}

export default function(state:PostReducer = initialState, action) {
    switch (action.type) {
        case GET_POSTS:
            return {
                ...state,
                postsLoaded: true
            }
        case SET_GROUP_POSTS:
            return {
                ...state,
                //groupPostsLoaded: true,
                postsLoaded: true,
                posts: action.payload as Array<IGroupPost>
            }
        case SET_USER_POSTS:
            return {
                ...state,
                postsLoaded: true,
                posts: action.payload as Array<IUserPost>
            }
        case DELETE_POST:
            return {
                ...state,
                posts: [...state.posts.filter(e=>e.postId !== action.payload)]
            }
        case SET_CREATED_POST:
            return {
                ...state,
                posts: [action.payload, ...state.posts]
            }
        default:
            return state;
    }
}