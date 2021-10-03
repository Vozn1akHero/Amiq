import React, {Component} from 'react';
import {FriendService} from "features/friend/friend-service";
import {container} from "tsyringe";
import FriendListPage from "./FriendListPage";
import {AuthStore} from "../../store/auth/auth-store";
import {StatusCodes} from "http-status-codes";
import {IFriendship} from "../../features/friend/friendship-models";

type State = {
    friends: Array<IFriendship>;
    friendsLoaded: boolean;
    allFriends: Array<IFriendship>;
}

class FriendsPageContainer extends Component<any, State> {
    //readonly friendService : FriendService = container.resolve(FriendService);
    friendService = new FriendService();
    friendsMock: Array<any> = [];

    constructor(props:any) {
        super(props);

        this.state = {
            friends: [],
            friendsLoaded: false,
            allFriends: []
        }

        //this.friendsMock = this.friendService.getFriendsByUserId("");
    }

    componentDidMount() {
        this.getUserFriends();
    }

    onSearchInputChange = (text: string) => {
        console.log(text)
        this.friendService.search(text).then(res => {
            console.log(res.data)
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
                                friendsLoaded={this.state.friendsLoaded}
                                onSearchInputChange={this.onSearchInputChange} />
            </>
        );
    }
}

export default FriendsPageContainer;
