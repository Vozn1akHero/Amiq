import {UserPostService} from "../../../features/post/user-post-service";
import {GroupPostService} from "../../../features/post/group-post-service";
import {IGroupPost} from "../../../features/post/models/group-post";
import {GET_GROUP_POSTS} from "../types/groupPostTypes";
import {GET_USER_POSTS, SET_USER_POSTS} from "../types/userPostTypes";
import {IUserPost} from "../../../features/post/models/user-post";
import {StatusCodes} from "http-status-codes";
import {PostService} from "../../../features/post/post-service";
import {CREATE_POST, DELETE_POST, SET_CREATED_POST} from "../types/postTypes";
import {AuthStore} from "../../custom/auth/auth-store";
import {AxiosResponse} from "axios";

const userPostService = new UserPostService();
const groupPostService = new GroupPostService();
const postService = new PostService();

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

export const getUserPosts = (userId: number, page: number) => dispatch => {
    dispatch({
        type: GET_USER_POSTS
    })

    userPostService.getUserPosts(userId, page).then(({data, status}) => {
        if(status === StatusCodes.OK){
            dispatch({
                type: SET_USER_POSTS,
                payload: data as Array<IUserPost>
            })
        }
    })
}

export const createUserPost = (post: Partial<IUserPost>) => async dispatch => {
    dispatch({
        type: CREATE_POST
    })

    const result : AxiosResponse<IUserPost> = await userPostService.create(post)
    const { data } : { data: IUserPost } = result;

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
        if(res.status === StatusCodes.OK){
            dispatch({
                type: DELETE_POST,
                payload: postId
            })
        }
    })
}