import React, {Component} from 'react';
import FoundUserCard from "features/friend/components/FoundUserCard";
import SearchInput from "../../common/components/SearchInput/SearchInput";
import {IFriendship} from "../../features/friend/friendship-models";
import {IFoundUser} from "../../features/user/models/found-user";

type Props = {
    friendList: Array<IFriendship>;
    friendsLoaded: boolean;
    foundUsers: {foundFriends: Array<IFoundUser>, foundUsers: Array<IFoundUser>};
    searchInputLoading: boolean;
    onSearchInputChange: (text: string) => void;
};

type State = {
    searchInputValue: string
};

class FriendListPage extends Component<Props, State> {
    constructor(props) {
        super(props);
        this.state = {
            searchInputValue: ""
        }
    }

    onSearchForUsers = (searchInputValue: string) => {
        this.setState({
            searchInputValue
        })
        this.props.onSearchInputChange(searchInputValue);
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
                                         onDebounceInputChange={this.onSearchForUsers} />
                        </div>
                    </div>
                    <div className="uk-grid uk-child-width-1-3">
                        {
                            this.state.searchInputValue.length === 0 ?
                                (this.props.friendsLoaded && this.props.friendList.map((value, i) =>
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
                                )) :  (
                                    !this.props.searchInputLoading && this.props.foundUsers.foundFriends.map((value, i) =>
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
                                )
                        }
                    </div>
                </div>
                {
                    !this.props.searchInputLoading && this.props.foundUsers.foundUsers.length > 0 && <div className="other-users uk-margin-large-top">
                        <legend className="uk-legend uk-margin-medium-top">Inni użytkownicy</legend>
                        <div className="uk-grid uk-child-width-1-3">
                            {
                                this.props.foundUsers.foundUsers.map((value, i) =>
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
