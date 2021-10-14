import React, {Component} from 'react';
import UserCard from "common/components/UserCard/UserCard";
import {IUserCardControl} from "common/components/UserCard/IUserCardControl";

class GroupParticipantsSettings extends Component {
    cardControls: Array<IUserCardControl>;

    onBlockClick = (userId: number) => {
        console.log(userId)
    }

    constructor(props) {
        super(props);

        this.cardControls = [
            {
                icon: "trash",
                event: this.onBlockClick
            }
        ];
    }

    render() {
        return (
            <div className="group-participants-settings">
                <UserCard userId={1} surname="d1" name="d1" avatarPath="user.jpg" controls={this.cardControls} />
                <UserCard userId={2} surname="d2" name="d2" avatarPath="user.jpg" controls={this.cardControls} />
            </div>
        );
    }
}

export default GroupParticipantsSettings;