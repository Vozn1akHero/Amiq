import React, {Component} from 'react';
import {FriendService} from "features/friend/friend-service";
import FriendListPage from "./FriendListPage";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {IFriendship} from "../../features/friend/friendship-models";
import {IFoundUser} from "../../features/user/models/found-user";
import {getUserFriends, searchForFriends} from "store/redux/actions/userFriendActions";
import {connect} from "react-redux";

type State = {
}

type Props = {
    getUserFriends(userId: number, page: number, count: number):void;
    searchForFriends(text: string);
    userFriends: Array<IFriendship>;
    userFriendsLoaded: boolean;
    searching: boolean;
    foundUsers: {foundFriends: Array<IFoundUser>, foundUsers: Array<IFoundUser>};
}

class FriendsPageContainer extends Component<Props, State> {
    friendService = new FriendService();

    componentDidMount() {
        this.props.getUserFriends(AuthStore.identity.userId, 1, 20);
    }

    onSearchInputChange = (text: string) => {
        this.props.searchForFriends(text);
    }


    render() {
        return (
            <>
                <FriendListPage friendList={this.props.userFriends}
                                foundUsers={this.props.foundUsers}
                                friendsLoaded={this.props.userFriendsLoaded}
                                searchInputLoading={this.props.searching}
                                onSearchInputChange={this.onSearchInputChange} />
            </>
        );
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        getUserFriends: (userId: number, page: number, count: number) => dispatch(getUserFriends(userId, page, count)),
        searchForFriends: (text: string) => dispatch(searchForFriends(text))
    }
}

const mapStateToProps = (state) => {
    return {
        userFriends: state.userFriend.userFriends,
        userFriendsLoaded: state.userFriend.userFriendsLoaded,
        searching: state.userFriend.searching,
        foundUsers: {
            foundFriends: state.userFriend.foundFriends,
            foundUsers: state.userFriend.foundUsers
        }
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(FriendsPageContainer);
