import {UserPostService} from "../../../features/post/user-post-service";
import {GroupPostService} from "../../../features/post/group-post-service";
import {IGroupPost} from "../../../features/post/models/group-post";
import {GET_GROUP_POSTS} from "../types/groupPostTypes";

const userPostService = new UserPostService();
const groupPostService = new GroupPostService();

export const getGroupPosts = (groupId: number, page: number) => (dispatch) => {
    dispatch({
        type: GET_GROUP_POSTS
    });

    groupPostService.getPostsByGroupId(groupId, page).then(res => {
        const groupPosts = res.data as Array<IGroupPost>;
        dispatch({
            type: "SET_POSTS",
            payload: groupPosts
        })
    })
}

