import React, {Component} from 'react';
import {FriendService} from "features/friend/friend-service";
import FriendListPage from "./FriendListPage";
import {AuthStore} from "../../store/auth/auth-store";
import {StatusCodes} from "http-status-codes";
import {IFriendship} from "../../features/friend/friendship-models";
import {IFoundUser} from "../../features/user/models/found-user";

type State = {
    friends: Array<IFriendship>;
    friendsLoaded: boolean;
    allFriends: Array<IFriendship>;
    foundUsers: Array<IFoundUser>;
}

class FriendsPageContainer extends Component<any, State> {
    //readonly friendService : FriendService = container.resolve(FriendService);
    friendService = new FriendService();

    constructor(props:any) {
        super(props);

        this.state = {
            friends: [],
            friendsLoaded: false,
            allFriends: [],
            foundUsers: []
        }
    }

    componentDidMount() {
        this.getUserFriends();
    }

    onSearchInputChange = (text: string) => {
        console.log(text)
        this.friendService.search(text).then(res => {
            console.log(res.data)
            const data : any = res.data;
            if(res.status === StatusCodes.OK){
                this.setState({
                    friends: data.foundFriends as Array<IFriendship>,
                    foundUsers: data.foundUsers as Array<IFoundUser>
                })
            }
        })
    }

    getUserFriends = () => {
        this.friendService.getFriendsByUserId(AuthStore.identity.userId, 1, 20).then(res => {
            console.log(res.data)
            if(res.status === StatusCodes.OK){
                this.setState({
                    friendsLoaded: true,
                    friends: res.data as Array<IFriendship>,
                    allFriends: [...this.state.friends, ...this.state.allFriends]
                })
            }
        })
    }

    render() {
        return (
            <>
                <FriendListPage friendList={this.state.friends}
                                foundUsers={this.state.foundUsers}
                                friendsLoaded={this.state.friendsLoaded}
                                onSearchInputChange={this.onSearchInputChange} />
            </>
        );
    }
}

export default FriendsPageContainer;
