import React, {Component, Fragment} from 'react';
import FoundUserCard from "features/friend/components/FoundUserCard/FoundUserCard";
import SearchInput from "common/components/SearchInput/SearchInput";
import {IFriendRequest, IFriendship} from "features/friend/friendship-models";
import {IFoundUser} from "features/user/models/found-user";
import {FriendRequestType} from "features/friend/friend-request-type";
import {getUserFriends, removeFriend, searchForFriends} from "store/redux/actions/userFriendActions";
import {
    acceptFriendRequest,
    cancelFriendRequest,
    getReceivedFriendRequests, getSentFriendRequests,
    rejectFriendRequest
} from "store/redux/actions/friendRequestActions";
import {connect} from "react-redux";
import {AuthStore} from "../../store/custom/auth/auth-store";
import InfiniteScroll from "react-infinite-scroll-component";

type Props = {
    userFriendsLoaded: boolean;
    userFriends: Array<IFriendship>;
    userFriendsLength: number;
    userFriendsCurrentPage: number;
    foundUsers: { foundFriends: Array<IFoundUser>, foundUsers: Array<IFoundUser> };
    searchInputLoading: boolean;
    onSearchInputChange: (text: string) => void;
    sentFriendRequestsLoaded: boolean;
    sentFriendRequests: Array<IFriendRequest>;
    receivedFriendRequestsLoaded: boolean;
    receivedFriendRequests: Array<IFriendRequest>;
    getUserFriends(userId: number, page: number, count: number): void;
    searchForFriends(text: string): void;
    getReceivedFriendRequests(): void;
    getSentFriendRequests(): void;
    acceptFriendRequest(friendRequestId: string): void;
    cancelFriendRequest(friendRequestId: string): void;
    rejectFriendRequest(friendRequestId: string): void;
    removeFriend(friendId: number): void;
};

type State = {
    searchInputValue: string,
    selectedFriendRequestType: FriendRequestType,
};

class FriendListPage extends Component<Props, State> {
    readonly USER_FRIENDS_LENGTH: number = 10;

    constructor(props) {
        super(props);
        this.state = {
            searchInputValue: "",
            selectedFriendRequestType: FriendRequestType.Receiver,
        }
    }

    componentDidMount() {
        this.props.getReceivedFriendRequests();
        this.props.getUserFriends(AuthStore.identity.userId, 1, this.USER_FRIENDS_LENGTH);
    }

    onSearchForUsers = (searchInputValue: string) => {
        this.setState({
            searchInputValue
        })
        this.props.searchForFriends(searchInputValue);
    }

    navigateToFriendRequestType = (e, friendRequestType: FriendRequestType) => {
        e.preventDefault();
        this.setState({
            selectedFriendRequestType: friendRequestType
        })
        if (friendRequestType === FriendRequestType.Creator && !this.props.sentFriendRequestsLoaded) {
            this.props.getSentFriendRequests();
        }
    }

    render() {
        return (
            <div className="friend-list-page">
                <div className="friends">
                    <legend className="uk-legend uk-margin-medium-top">Moi znajomi</legend>
                    <div className="input-search">
                        <div className="uk-margin-medium-top uk-margin-medium-bottom">
                            <SearchInput debounceTime={600}
                                         showSpinner={this.props.searchInputLoading}
                                         onDebounceInputChange={this.onSearchForUsers}/>
                        </div>
                    </div>

                    <div className="friend-requests">
                        <h4 className="uk-h4">Zaproszenia do znajomych</h4>
                        <div>
                            <ul className="uk-child-width-expand" uk-tab="true">
                                <li className={this.state.selectedFriendRequestType === FriendRequestType.Receiver ? `uk-active` : ""}>
                                    <a onClick={e => {
                                        this.navigateToFriendRequestType(e, FriendRequestType.Receiver)
                                    }}>
                                        Otrzymane
                                    </a>
                                </li>
                                <li className={this.state.selectedFriendRequestType === FriendRequestType.Creator ? `uk-active` : ""}>
                                    <a onClick={e => {
                                        this.navigateToFriendRequestType(e, FriendRequestType.Creator)
                                    }}>
                                        Wysłane
                                    </a>
                                </li>
                            </ul>
                        </div>
                        <div className="uk-grid uk-child-width-1-3">
                            {
                                this.state.selectedFriendRequestType === FriendRequestType.Receiver ?
                                    (
                                        this.props.receivedFriendRequestsLoaded && this.props.receivedFriendRequests.map((value, i) => {
                                                return <div key={i} className="uk-margin-top">
                                                    <FoundUserCard userId={value.creator.userId}
                                                                   name={value.creator.name}
                                                                   surname={value.creator.surname}
                                                                   avatarPath={value.creator.avatarPath}
                                                                   friendRequestId={value.friendRequestId}
                                                                   issuerReceivedFriendRequest={true}
                                                                   onRejectFriendRequest={this.props.rejectFriendRequest}
                                                                   onAcceptFriendRequest={this.props.acceptFriendRequest}
                                                                   key={i}/>
                                                </div>
                                            }
                                        )
                                    )
                                    :
                                    (
                                        this.props.sentFriendRequestsLoaded && this.props.sentFriendRequests.map((value, i) => {
                                                return <div key={i} className="uk-margin-top">
                                                    <FoundUserCard userId={value.receiver.userId}
                                                                   name={value.receiver.name}
                                                                   surname={value.receiver.surname}
                                                                   avatarPath={value.receiver.avatarPath}
                                                                   friendRequestId={value.friendRequestId}
                                                                   issuerSentFriendRequest={true}
                                                                   onCancelFriendRequest={this.props.cancelFriendRequest}
                                                                   key={i}/>
                                                </div>
                                            }
                                        )
                                    )
                            }
                        </div>
                    </div>

                    <div className="uk-margin-medium-top">
                        <h4 className="uk-h4">Znajomi</h4>
                        <div>
                            {
                                this.state.searchInputValue.length === 0 ?
                                    (this.props.userFriendsLoaded &&
                                        <InfiniteScroll dataLength={this.props.userFriendsLength}
                                                        next={() => {
                                                            this.props.getUserFriends(AuthStore.identity.userId,
                                                                this.props.userFriendsCurrentPage,
                                                                this.USER_FRIENDS_LENGTH)
                                                        }}
                                                        hasMore={this.props.userFriendsLength > this.props.userFriends.length}
                                                        loader={<h3>Loading...</h3>}>
                                            <div className="uk-grid uk-child-width-1-3">
                                                {
                                                    this.props.userFriends.map((value, i) => {
                                                            return <div key={i} className="uk-margin-top">
                                                                <FoundUserCard userId={value.userId}
                                                                               name={value.name}
                                                                               surname={value.surname}
                                                                               avatarPath={value.avatarPath}
                                                                               isIssuerFriend={true}
                                                                               onRemoveFriendById={this.props.removeFriend}
                                                                               key={i}/>
                                                            </div>
                                                        }
                                                    )
                                                }
                                            </div>
                                        </InfiniteScroll>) : (
                                        !this.props.searchInputLoading && <div className="uk-grid uk-child-width-1-3">
                                            {
                                                this.props.foundUsers.foundFriends.map((value, i) => {
                                                        return <div key={i} className="uk-margin-top">
                                                            <FoundUserCard userId={value.userId}
                                                                           name={value.name}
                                                                           surname={value.surname}
                                                                           avatarPath={value.avatarPath}
                                                                           isIssuerFriend={true}
                                                                           onRemoveFriendById={this.props.removeFriend}
                                                                           key={i}/>
                                                        </div>
                                                    }
                                                )
                                            }
                                        </div>
                                    )
                            }
                        </div>
                    </div>
                </div>
                {
                    !this.props.searchInputLoading && this.props.foundUsers.foundUsers.length > 0 &&
                    <div className="other-users uk-margin-large-top">
                        <legend className="uk-legend uk-margin-medium-top">Inni użytkownicy</legend>
                        <div className="uk-grid uk-child-width-1-3">
                            {
                                this.props.foundUsers.foundUsers.map((value, i) => {
                                        return <div key={i} className="uk-margin-top">
                                            <FoundUserCard
                                                userId={value.userId}
                                                name={value.name}
                                                surname={value.surname}
                                                avatarPath={value.avatarPath}
                                                isIssuerFriend={value.isIssuerFriend}
                                                blockedByIssuer={value.blockedByIssuer}
                                                issuerBlocked={value.issuerBlocked}
                                                issuerReceivedFriendRequest={value.issuerReceivedFriendRequest}
                                                issuerSentFriendRequest={value.issuerSentFriendRequest}
                                                onRemoveFriendById={this.props.removeFriend}
                                                onCancelFriendRequest={this.props.cancelFriendRequest}
                                                onRejectFriendRequest={this.props.rejectFriendRequest}
                                                onAcceptFriendRequest={this.props.acceptFriendRequest}
                                                key={i}/>
                                        </div>
                                    }
                                )
                            }
                        </div>
                    </div>
                }
            </div>
        );
    }
}


const mapDispatchToProps = (dispatch) => {
    return {
        getUserFriends: (userId: number, page: number, count: number) => dispatch(getUserFriends(userId, page, count)),
        searchForFriends: (text: string) => dispatch(searchForFriends(text)),
        getReceivedFriendRequests: () => dispatch(getReceivedFriendRequests()),
        getSentFriendRequests: () => dispatch(getSentFriendRequests()),
        cancelFriendRequest: (friendRequestId: string) => dispatch(cancelFriendRequest(friendRequestId)),
        acceptFriendRequest: (friendRequestId: string) => dispatch(acceptFriendRequest(friendRequestId)),
        rejectFriendRequest: (friendRequestId: string) => dispatch(rejectFriendRequest(friendRequestId)),
        removeFriend: (friendId: number) => dispatch(removeFriend(friendId))
    }
}

const mapStateToProps = (state) => {
    return {
        sentFriendRequests: state.friendRequest.sentFriendRequests,
        sentFriendRequestsLoaded: state.friendRequest.sentFriendRequestsLoaded,
        receivedFriendRequests: state.friendRequest.receivedFriendRequests,
        receivedFriendRequestsLoaded: state.friendRequest.receivedFriendRequestsLoaded,
        userFriends: state.userFriend.userFriends,
        userFriendsLoaded: state.userFriend.userFriendsLoaded,
        userFriendsLength: state.userFriend.userFriendsLength,
        userFriendsCurrentPage: state.userFriend.userFriendsCurrentPage,
        searching: state.userFriend.searching,
        foundUsers: {
            foundFriends: state.userFriend.foundFriends,
            foundUsers: state.userFriend.foundUsers
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(FriendListPage)