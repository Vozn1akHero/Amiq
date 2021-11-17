import React, {Dispatch, Fragment, useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import {AuthStore} from "../../../store/custom/auth/auth-store";
import {IFriendship} from "../../../features/friend/friendship-models";
import {useDispatch, useSelector} from "react-redux";
import {getUserFriends, removeFriend} from "../../../store/redux/actions/userFriendActions";
import FoundUserCard from "../../../features/friend/components/FoundUserCard/FoundUserCard";
import InfiniteScroll from 'react-infinite-scroll-component';

const UserFriendsSubpage = () => {
    const {userId} = useParams<any>();

    const [isViewerProfile, setIsViewerProfile] = useState(null);
    const [actualProfileId, setActualProfileId] = useState(null);

    const dispatch: Dispatch<any> = useDispatch();

    const USER_FRIENDS_PER_PAGE = 10;

    useEffect(() => {
        initProfileId()
    }, [])

    useEffect(() => {
        if(actualProfileId)
            dispatch(getUserFriends(actualProfileId, userFriendsCurrentPage, USER_FRIENDS_PER_PAGE));
    }, [actualProfileId])

    const userFriends: Array<IFriendship> = useSelector(
        (state:any) => {
            return state.userFriend.userFriends
        }
    )
    const userFriendsCurrentPage: number = useSelector(
        (state:any) => {
            return state.userFriend.userFriendsCurrentPage
        }
    )
    const userFriendsLoaded: boolean = useSelector(
        (state:any) => {
            return state.userFriend.userFriendsLoaded
        }
    )
    const userFriendsLength: number = useSelector(
        (state:any) => {
            return state.userFriend.userFriendsLength
        }
    )

    const initProfileId = () => {
        if(userId){
            const isUserProfile : boolean = AuthStore.identity.userId === +userId;
            setIsViewerProfile(isUserProfile);
            setActualProfileId(userId);
        } else {
            setIsViewerProfile(true);
            AuthStore.identity$.subscribe(value => {
                if(value)
                    setActualProfileId(value.userId);
            })
        }
    }

    return (
        <div className="user-friends-subpage">
            <InfiniteScroll
                dataLength={userFriendsLength}
                next={() => {dispatch(getUserFriends(actualProfileId, userFriendsCurrentPage, USER_FRIENDS_PER_PAGE))}}
                hasMore={userFriendsLength >= userFriends.length}
                loader={<Fragment></Fragment>}
            >
            {
                (userFriendsLoaded && userFriends.map((value, i) =>
                    {
                        return <div key={i} className="uk-margin-top">
                            <FoundUserCard userId={value.userId}
                                           name={value.name}
                                           surname={value.surname}
                                           avatarPath={value.avatarPath}
                                           isIssuerFriend={true}
                                           onRemoveFriendById={(friendId: number) => dispatch(removeFriend(friendId))}
                                           key={i} />
                        </div>
                    }
                ))
            }
            </InfiniteScroll>
        </div>
    );
};

export default UserFriendsSubpage;
