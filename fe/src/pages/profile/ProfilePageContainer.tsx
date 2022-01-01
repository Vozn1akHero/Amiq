import React, {Dispatch, useEffect, useLayoutEffect, useState} from 'react';
import ProfilePage from "./ProfilePage";
import {IUserPost} from "../../features/post/models/user-post";
import {AuthStore} from "../../store/custom/auth/auth-store";
import UserService from "features/user/user-service";
import {IUser} from "features/user/models/user";
import {IPostCommentCreation} from "../../features/post/models/post-comment";
import {getUserFriends} from "../../store/redux/actions/userFriendActions";
import {useDispatch, useSelector} from "react-redux";
import {IFriendship} from "../../features/friend/friendship-models";
import {
    createUserPost,
    createUserPostComment,
    deletePost,
    getUserPostComments,
    getUserPosts,
    removeUserPostComment
} from "../../store/redux/actions/postActions";
import {FriendRequestService} from "../../features/friend/friend-request-service";
import {AxiosResponse} from "axios";
import {BlockedUserService} from "../../features/user/blocked-user-service";
import {StatusCodes} from "http-status-codes";
import {FriendService} from "../../features/friend/friend-service";
import {first} from "rxjs";
import {IdentityModel} from "../../store/custom/auth/identity-model";
import {IPageVisitationActivity} from "../../features/activity-tracking/models";
import moment from "moment";


const ProfilePageContainer: React.FC = (props: any) => {
    const userService = new UserService();
    const friendRequestService = new FriendRequestService();
    const blockedUserService = new BlockedUserService();
    const friendService = new FriendService();

    const [isViewerProfile, setIsViewerProfile] = useState(null);
    const [userData, setUserData] = useState<IUser>(null);
    const [userDataLoaded, setUserDataLoaded] = useState(false);
    const [actualProfileId, setActualProfileId] = useState(null);
    const [visitationTimeInMinutes, setVisitationTimeInMinutes] = useState(0);

    const EXEMPLARY_ENTITIES_LENGTH: number = 6;
    const POSTS_PER_PAGE: number = 10;

    const dispatch: Dispatch<any> = useDispatch();

    useEffect(() => {
        initProfileId();
        startTrackingActivity();
    }, []);

    useEffect(() => {
        initProfileId();
    }, [props.match.params.userId]);

    useEffect(() => {
        if (actualProfileId) {
            getUserData();
            dispatch(getUserFriends(actualProfileId, 1, EXEMPLARY_ENTITIES_LENGTH));
            dispatch(getUserPosts(actualProfileId, 1, POSTS_PER_PAGE));
        }
    }, [actualProfileId])

    useLayoutEffect(() => {
        return () => {
            storeTrackingActivity();
        }
    }, [])

    //#region user friends
    const userFriends: Array<IFriendship> = useSelector(
        (state: any) => {
            return state.userFriend.userFriends
        }
    )
    const userFriendsLoaded: boolean = useSelector(
        (state: any) => {
            return state.userFriend.userFriendsLoaded
        }
    )
    //#endregion

    const postsLoaded: boolean = useSelector(
        (state: any) => {
            return state.post.postsLoaded
        }
    )
    const userPosts: Array<IUserPost> = useSelector(
        (state: any) => {
            return state.post.posts
        }
    )
    const postsLength: number = useSelector(
        (state: any) => {
            return state.post.postsLength
        }
    )
    const nextPage: number = useSelector(
        (state: any) => {
            return state.post.nextPage
        }
    )

    const initProfileId = () => {
        const userId = props.match.params.userId;
        if (userId) {
            const isUserProfile: boolean = AuthStore.identity.userId === +userId;
            setIsViewerProfile(isUserProfile);
            setActualProfileId(userId);
        } else {
            setIsViewerProfile(true);
            AuthStore.identity$
                .pipe(first((e: IdentityModel) => e.userId != null && e.userId != undefined))
                .subscribe((value:IdentityModel) => {
                    setActualProfileId(value.userId)
                })

        }
    }

    const getUserData = () => {
        userService.getById(actualProfileId).then(res => {
            const userData = res.data as IUser;
            setUserData(userData);
            setUserDataLoaded(true);
        })
    }

    //#region avatar component events handling
    const sendFriendRequest = (destUserId: number) => {
        return friendRequestService.sendFriendRequest(destUserId).then((res: AxiosResponse) => {
            if (res.status === StatusCodes.CREATED) {
                setUserData({
                    ...userData,
                    issuerSentFriendRequest: true
                })
            }
        })
    }
    const rejectFriendRequest = (destUserId: number) => {
        return friendRequestService.rejectFriendRequestByDestUserId(destUserId).then((res: AxiosResponse) => {
            if (res.status === StatusCodes.OK) {
                setUserData({
                    ...userData,
                    issuerReceivedFriendRequest: false
                })
            }
        })
    }
    const cancelFriendRequest = (destUserId: number) => {
        return friendRequestService.cancelFriendRequestByDestUserId(destUserId).then((res: AxiosResponse) => {
            if (res.status === StatusCodes.OK) {
                setUserData({
                    ...userData,
                    issuerSentFriendRequest: false
                })
            }
        })
    }
    const acceptFriendRequest = (destUserId: number) => {
        return friendRequestService.acceptFriendRequestByDestUserId(destUserId).then((res: AxiosResponse) => {
            if (res.status === StatusCodes.OK) {
                setUserData({
                    ...userData,
                    issuerReceivedFriendRequest: false,
                    isIssuerFriend: true
                })
            }
        })
    }
    const blockUser = (destUserId: number) => {
        return blockedUserService.blockUser(destUserId).then((res: AxiosResponse) => {
            if (res.status === StatusCodes.OK) {
                setUserData({
                    ...userData,
                    blockedByIssuer: true,
                    issuerBlocked: false
                })
            }
        })
    }
    const removeFriend = (friendId: number) => {
        return friendService.removeFriend(friendId).then((res: AxiosResponse) => {
            if (res.status === StatusCodes.OK) {
                setUserData({
                    ...userData,
                    isIssuerFriend: false
                })
            }
        })
    }

    const onAvatarChangeSubmit = (file: File) => {
        userService.changeAvatar(file).then(res => {
            if(res.status === StatusCodes.OK){
                setUserData({
                    ...userData,
                    avatarPath: res.data.entity.avatarPath
                })
                window.location.reload();
            }
        })

    }
    //#endregion

    const getPostComments = (postId: string, page: number) => {
        dispatch(getUserPostComments(postId, page, 10))
    }

    const removeComment= (postCommentId: string) => {
        dispatch(removeUserPostComment(postCommentId))
    }

    const createPost = (value:{text: string}) => {
        const newPost: Partial<IUserPost> = {
            textContent: value.text,
            author: {
                userId: AuthStore.identity.userId
            }
        }
        dispatch(createUserPost(newPost))
    }

    //#region activity tracking

    const startTrackingActivity = () => {
        if(isViewerProfile) return;

        setTimeout(() => {
            setVisitationTimeInMinutes(visitationTimeInMinutes + 1);
        }, 60000)
    }

    const storeTrackingActivity = () => {
        if(isViewerProfile) return;

        if(!sessionStorage.getItem("act"))
        {
            const obj : IPageVisitationActivity = {
                groupVisitations: [],
                userProfileVisitations: [],
                //lastRequestTime: moment().toDate()
            };
            sessionStorage.setItem("act", JSON.stringify(obj));
        }

        let visitationState = JSON.parse(sessionStorage.getItem("act")) as IPageVisitationActivity;

        if(!visitationState){
            visitationState = {
                userProfileVisitations: [],
                groupVisitations: []
            }
        }

        if(!visitationState.userProfileVisitations){
            visitationState.userProfileVisitations = [];
        }

        let profileVisitationIndex = visitationState.userProfileVisitations.findIndex(e=>e.visitedUserId === actualProfileId);
        if(profileVisitationIndex!==-1){
            visitationState.userProfileVisitations = visitationState.userProfileVisitations.map((value, index) => {
                if(index === profileVisitationIndex){
                    value.visitationTotalTime += visitationTimeInMinutes;
                    value.lastVisited =  moment().toDate()
                }
                return value;
            });
        } else {
            visitationState.userProfileVisitations.push({
                visitedUserId: actualProfileId,
                lastVisited: moment().toDate(),
                visitationTotalTime: visitationTimeInMinutes
            })
        }

        sessionStorage.setItem("act", JSON.stringify(visitationState));
    }

    //endregion

    return (
        <ProfilePage posts={userPosts}
                     postsLength={postsLength}
                     getMorePosts={() => {
                         dispatch(getUserPosts(actualProfileId, nextPage, POSTS_PER_PAGE))
                     }}
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
                     removeComment={removeComment}
                     getComments={getPostComments}
                     commentCreated={(data: IPostCommentCreation) => {
                         dispatch(createUserPostComment(data));
                     }}
                     postsLoaded={postsLoaded}
                     deletePost={(postId: string) => {
                         dispatch(deletePost(postId))
                     }}
                     isViewerProfile={isViewerProfile}
                     userData={userData}
                     userDataLoaded={userDataLoaded}
                     createPost={createPost}
                     onAvatarChangeSubmit={onAvatarChangeSubmit}
        />
    );
}

export default ProfilePageContainer;