import React, {Dispatch, useEffect, useState} from 'react';
import ProfilePage from "./ProfilePage";
import {IUserPost} from "../../features/post/models/user-post";
import {UserPostService} from "../../features/post/user-post-service";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {useParams} from "react-router-dom";
import UserService from "features/user/user-service";
import {IUser} from "features/user/models/user";
import {AxiosResponse} from "axios";
import {IPostCommentCreation} from "../../features/post/models/post-comment";
import {PostService} from "../../features/post/post-service";
import {StatusCodes} from "http-status-codes";
import {PostCommentService} from "../../features/post/post-comment-service";
import {getUserFriends} from "../../store/redux/actions/userFriendActions";
import {useDispatch, useSelector} from "react-redux";
import {IFriendship} from "../../features/friend/friendship-models";
import {createUserPost, deletePost, getUserPosts} from "../../store/redux/actions/postActions";


const ProfilePageContainer : React.FC = () => {
    const userPostService = new UserPostService();
    const userService = new UserService();
    const postService = new PostService();
    const postCommentService = new PostCommentService();

    const [isViewerProfile, setIsViewerProfile] = useState(null);
    //const [userPosts, setUserPosts] = useState<Array<IUserPost>>([]);
    const [userData, setUserData] = useState(null);
    const [userDataLoaded, setUserDataLoaded] = useState(false);
    //const [postsLoaded, setPostsLoaded] = useState(false);
    const [actualProfileId, setActualProfileId] = useState(null);

    const EXEMPLARY_ENTITIES_LENGTH: number = 6;
    const {userId} = useParams<any>();

    const dispatch: Dispatch<any> = useDispatch();

    useEffect(() => {
        initProfileId();
    }, []);

    useEffect(() => {
        if(actualProfileId){
            getUserData();
            //getUserPosts();
            dispatch(getUserFriends(actualProfileId, 1, EXEMPLARY_ENTITIES_LENGTH));
            dispatch(getUserPosts(actualProfileId, 1));
        }
    }, [actualProfileId])

    //TODO
    const getViewerStatus = () => {

    }

    //#region user friends

    const userFriends: Array<IFriendship> = useSelector(
        (state:any) => {
            return state.userFriend.userFriends
        }
    )
    const userFriendsLoaded: boolean = useSelector(
        (state:any) => {
            return state.userFriend.userFriendsLoaded
        }
    )

    //#endregion

    const postsLoaded: boolean = useSelector(
        (state:any) => {
            return state.post.loaded
        }
    )
    const userPosts: Array<IUserPost> = useSelector(
        (state:any) => {
            return state.post.posts
        }
    )

    /*const getUserPosts = () => {
        userPostService.getUserPosts(actualProfileId.toString(), 1).then(value => {
            const posts = value.data as Array<IUserPost>;
            console.log(posts)
            setUserPosts(posts);
            setPostsLoaded(true);
        });
    }*/

    const initProfileId = () => {
        if(userId){
            setIsViewerProfile(AuthStore.identity.userId === userId);
            setActualProfileId(userId);
        } else {
            setIsViewerProfile(true);
            AuthStore.identity$.subscribe(value => {
                if(value)
                    setActualProfileId(value.userId);
            })
        }
    }

    const getUserData = () => {
        userService.getById(actualProfileId.toString()).then(res => {
            const userData = res.data as IUser;
            setUserData(userData);
            setUserDataLoaded(true);
        })
    }

    /*const createPost = async (text: string) => {
        const post : Partial<IUserPost> = {
            textContent: text,
            author: {
                userId: AuthStore.identity.userId
            }
        }
        const result : AxiosResponse<IUserPost> = await userPostService.create(post)
        const {data} = result;
        const arr : Array<IUserPost> = [data, ...userPosts];
        //setUserPosts(arr);
    }*/

    const onCommentCreated = (data: IPostCommentCreation) => {
        postCommentService.create(data).then(res => {
            console.log(res.data)
        })
    }

    const onRemoveComment = (commentId: string) => {

    }

    return (
        <ProfilePage posts={userPosts}
                     profileId={actualProfileId}
                     userFriendsLoaded={userFriendsLoaded}
                     userFriends={userFriends}
                     onRemoveComment={onRemoveComment}
                     onCommentCreated={onCommentCreated}
                     postsLoaded={postsLoaded}
                     deletePost={(postId: string) => {
                         dispatch(deletePost(postId))
                     }}
                     isViewerProfile={isViewerProfile}
                     userData={userData}
                     userDataLoaded={userDataLoaded}
                     createPost={(text: string) => {
                         const newPost : Partial<IUserPost> = {
                             textContent: text,
                             author: {
                                 userId: AuthStore.identity.userId
                             }
                         }
                         dispatch(createUserPost(newPost))
                     }}
        />
    );

}

export default ProfilePageContainer;