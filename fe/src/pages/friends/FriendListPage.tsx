import React, {Component} from 'react';
import FoundUserCard from "features/friend/components/FoundUserCard";
import DebounceInput from "../../common/components/DebounceInput/DebounceInput";
import {IFriendship} from "../../features/friend/friendship-models";
import {IFoundUser} from "../../features/user/models/found-user";

type Props = {
    friendList: Array<IFriendship>;
    friendsLoaded: boolean;
    foundUsers: Array<IFoundUser>;
    onSearchInputChange: (text: string) => void;
};

type State = {

};

class FriendListPage extends Component<Props, State> {
    render() {
        return (
            <div className="friend-list-page">
                <div className="friends">
                    <legend className="uk-legend uk-margin-medium-top">Moi znajomi</legend>
                    <div className="input-search">
                        <div className="uk-margin-medium-top uk-margin-medium-bottom">
                            <DebounceInput debounceTime={600}
                                           onDebounceInputChange={(e) => this.props.onSearchInputChange(e)} />
                        </div>
                    </div>
                    <div className="uk-grid uk-child-width-1-3">
                        {
                            this.props.friendsLoaded && this.props.friendList.map((value, i) =>
                                {
                                    return <div key={i} className="uk-margin-top">
                                        <FoundUserCard userId={value.userId}
                                                       name={value.name}
                                                       surname={value.surname}
                                                       avatarPath={value.avatarPath}
                                                       isIssuerFriend={true}
                                                       key={i} />
                                    </div>
                                }
                            )
                        }
                    </div>
                </div>
                {
                    this.props.foundUsers.length > 0 && <div className="other-users uk-margin-large-top">
                        <legend className="uk-legend uk-margin-medium-top">Inni użytkownicy</legend>
                        <div className="uk-grid uk-child-width-1-3">
                            {
                                this.props.foundUsers.map((value, i) =>
                                    {
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
                                                key={i} />
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

export default FriendListPage;
