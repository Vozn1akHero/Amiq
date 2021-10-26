import {IPostComment} from "../../../features/post/models/post-comment";
import {SET_GROUP_POST_COMMENTS} from "../types/groupPostCommentTypes";

type GroupPostCommentReducer = {
    postCommentsLoaded: boolean;
    postComments: Array<IPostComment>
}

const initialState : GroupPostCommentReducer =  {
    postCommentsLoaded: false,
    postComments: []
}

export default function (state: GroupPostCommentReducer = initialState, action){
    switch (action.type) {
        case SET_GROUP_POST_COMMENTS:
            return {
                ...state,
                postCommentsLoaded: false,
                postComments: action.payload
            }
        default:
            return state;
    }
}