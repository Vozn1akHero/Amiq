import React, {Component} from 'react';
import {FriendService} from "features/friend/friend-service";
import {container} from "tsyringe";
import FriendListPage from "./FriendListPage";
import BasePageComponent from "core/BasePageComponent";

class FriendsPageContainer extends BasePageComponent {
    readonly friendService : FriendService = container.resolve(FriendService);
    friendsMock: Array<any> = [];

    constructor(props:any) {
        super(props);

        this.friendsMock = this.friendService.getFriendsByUserId("");
    }

    onSearchInputChange = (e : InputEvent) => {
        console.log(e)
    }

    render() {
        return (
            <>
                <FriendListPage friendList={this.friendsMock}
                                onSearchInputChange={this.onSearchInputChange} />
            </>
        );
    }
}

export default FriendsPageContainer;
