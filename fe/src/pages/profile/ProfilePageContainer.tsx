import React, {Dispatch, useEffect, useState} from 'react';
import ProfilePage from "./ProfilePage";
import {IUserPost} from "../../features/post/models/user-post";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {useHistory, useParams} from "react-router-dom";
import UserService from "features/user/user-service";
import {IUser} from "features/user/models/user";
import {IPostCommentCreation} from "../../features/post/models/post-comment";
import {PostCommentService} from "../../features/post/post-comment-service";
import {getUserFriends} from "../../store/redux/actions/userFriendActions";
import {useDispatch, useSelector} from "react-redux";
import {IFriendship} from "../../features/friend/friendship-models";
import {createUserPost, deletePost, getUserPosts} from "../../store/redux/actions/postActions";
import {FriendRequestService} from "../../features/friend/friend-request-service";
import {AxiosResponse} from "axios";
import {BlockedUserService} from "../../features/user/blocked-user-service";
import {StatusCodes} from "http-status-codes";
import {FriendService} from "../../features/friend/friend-service";


const ProfilePageContainer : React.FC = (props:any) => {
    const userService = new UserService();
    const postCommentService = new PostCommentService();
    const friendRequestService = new FriendRequestService();
    const blockedUserService = new BlockedUserService();
    const friendService = new FriendService();

    const [isViewerProfile, setIsViewerProfile] = useState(null);
    const [userData, setUserData] = useState<IUser>(null);
    const [userDataLoaded, setUserDataLoaded] = useState(false);
    const [actualProfileId, setActualProfileId] = useState(null);

    const EXEMPLARY_ENTITIES_LENGTH: number = 6;
    const POSTS_PER_PAGE: number = 10;

    const dispatch: Dispatch<any> = useDispatch();

    /*
    useEffect(() => {
        initProfileId();
    }, []);*/

    useEffect(() => {
        initProfileId();
    }, [props.match.params.userId]);

    useEffect(() => {
        if(actualProfileId){
            getUserData();
            dispatch(getUserFriends(actualProfileId, 1, EXEMPLARY_ENTITIES_LENGTH));
            dispatch(getUserPosts(actualProfileId, 1, POSTS_PER_PAGE));
        }
    }, [actualProfileId])

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
            return state.post.postsLoaded
        }
    )
    const userPosts: Array<IUserPost> = useSelector(
        (state:any) => {
            return state.post.posts
        }
    )
    const postsLength : number = useSelector(
        (state:any) => {
            return state.post.postsLength
        }
    )
    const nextPage : number = useSelector(
        (state:any) => {
            return state.post.nextPage
        }
    )

    const initProfileId = () => {
        const userId = props.match.params.userId;
        if(userId){
            const isUserProfile : boolean = AuthStore.identity.userId === +userId;
            console.log(isUserProfile)
            setIsViewerProfile(isUserProfile);
            setActualProfileId(userId);
        } else {
            setIsViewerProfile(true);
            setActualProfileId(AuthStore.identity.userId)
        }
    }

    const getUserData = () => {
        userService.getById(actualProfileId).then(res => {
            const userData = res.data as IUser;
            setUserData(userData);
            setUserDataLoaded(true);
        })
    }

    const onCommentCreated = (data: IPostCommentCreation) => {
        postCommentService.create(data).then(res => {
            console.log(res.data)
        })
    }

    const onRemoveComment = (commentId: string) => {

    }

    //#region avatar component events handling
    const sendFriendRequest = (destUserId: number) => {
        return friendRequestService.sendFriendRequest(destUserId).then((res: AxiosResponse) => {
            if(res.status === StatusCodes.CREATED){
                userData.issuerSentFriendRequest = true;
            }
        })
    }
    const rejectFriendRequest = (destUserId: number) => {
        return friendRequestService.rejectFriendRequestByDestUserId(destUserId).then((res: AxiosResponse) => {
            if(res.status === StatusCodes.OK){
                userData.issuerReceivedFriendRequest = false;
            }
        })
    }
    const cancelFriendRequest = (destUserId: number) => {
        return friendRequestService.cancelFriendRequestByDestUserId(destUserId).then((res: AxiosResponse) => {
            if(res.status === StatusCodes.OK){
                userData.issuerSentFriendRequest = false;
            }
        })
    }
    const acceptFriendRequest = (destUserId:number) => {
        return friendRequestService.acceptFriendRequestByDestUserId(destUserId).then((res: AxiosResponse) => {
            if(res.status === StatusCodes.OK){
                userData.issuerSentFriendRequest = false;
            }
        })
    }
    const blockUser = (destUserId: number) => {
        return blockedUserService.blockUser(destUserId).then((res: AxiosResponse) => {
            if(res.status === StatusCodes.OK){
                userData.issuerBlocked = true;
            }
        })
    }
    const removeFriend = (friendId: number) => {
        return friendService.removeFriend(friendId).then((res: AxiosResponse) => {
            if(res.status === StatusCodes.OK){
                userData.isIssuerFriend = false;
            }
        })
    }
    //#endregion

    return (
        <ProfilePage posts={userPosts}
                     postsLength={postsLength}
                     getMorePosts={()=>{dispatch(getUserPosts(actualProfileId, nextPage, POSTS_PER_PAGE))}}
                     postsPerPage={POSTS_PER_PAGE}
                     profileId={actualProfileId}
                     userFriendsLoaded={userFriendsLoaded}
                     userFriends={userFriends}
                     removeFriend={removeFriend}
                     acceptFriendRequest={acceptFriendRequest}
                     sendFriendRequest={sendFriendRequest}
                     rejectFriendRequest={rejectFriendRequest}
                     cancelFriendRequest={cancelFriendRequest}
                     blockUser={blockUser}
                     removeComment={onRemoveComment}
                     commentCreated={onCommentCreated}
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