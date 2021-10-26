import {IGroupPost} from "../../../features/post/models/group-post";
import {SET_GROUP_POSTS} from "../types/groupPostTypes";

type GroupPostReducer = {
    groupPosts: Array<IGroupPost>,
    groupPostsLoaded: boolean
}

const initialState : GroupPostReducer = {
    groupPosts: [],
    groupPostsLoaded: false
}

export default function(state:GroupPostReducer = initialState, action) {
    switch (action.type) {
        case SET_GROUP_POSTS:
            return {
                ...state,
                groupPostsLoaded: true,
                participants: action.payload
            }
        default:
            return state;
    }
}