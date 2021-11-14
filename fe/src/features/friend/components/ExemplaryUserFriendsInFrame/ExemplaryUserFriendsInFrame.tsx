import React, {Component} from 'react';
import {ItemsFrameL} from "common/components/ItemsFrameL/ItemsFrameL";
import {Link} from "react-router-dom";
import {Utils} from "core/utils";
import {IUserInFrame} from "common/components/ItemsFrameL/IUserInFrame";
import {IFriendship} from "../../friendship-models";
import "./exemplary-user-friends-in-frame.scss"

type Props = {
    userId: number;
    userFriends: Array<IFriendship>;
    userFriendsLoaded: boolean;
}

class ExemplaryUserFriendsInFrame extends Component<Props> {
    getConvertedExemplaryFriendsToFrameItems = () => {
        if(this.props.userFriendsLoaded){
            let arr : Array<IUserInFrame> = [];
            for(const e of this.props.userFriends){
                arr.push({
                    userId: e.userId,
                    viewName: e.name + " " + e.surname,
                    imagePath: e.avatarPath,
                    link: "/profile/" + e.userId
                })
            }
            return arr;
        }
    }

    render() {
        return (
            <div className="exemplary-user-friends-in-frame">
                <ItemsFrameL title="Znajomi"
                             displayHeaderAsLink={true}
                             link={`/profile/${this.props.userId}/friends`}
                             icon="users">
                    {
                        this.props.userFriendsLoaded && (this.props.userFriends.length > 0 ? <div className="exemplary-user-friends-in-frame__items">
                            {
                                this.getConvertedExemplaryFriendsToFrameItems().map((value, index) => <Link key={index} to={value.link}>
                                    <div className="exemplary-user-friends-in-frame__item">
                                        <img className="exemplary-user-friends-in-frame__item__avatar border-radius-50"
                                             src={Utils.getImageSrc(value.imagePath)} />
                                    </div>
                                </Link>)
                            }
                        </div> : <span>Brak znajomych</span>)
                    }
                </ItemsFrameL>
            </div>
        );
    }
}

export default ExemplaryUserFriendsInFrame;