import React, {Component} from 'react';
import FriendCard from "features/friend/components/FriendCard";

type Props = {
    friendList: Array<any>;
    onSearchInputChange: (e:InputEvent) => void;
};

type State = {

};

class FriendListPage extends Component<Props, State> {
    render() {
        return (
            <div className="friend-list-page">
                <legend className="uk-legend uk-margin-medium-top">Moi znajomi</legend>
                <div className="input-search">
                    <div className="uk-margin-medium-top uk-margin-medium-bottom">
                        <input className="uk-input" type="text" placeholder="Szukaj znajomych"/>
                    </div>
                </div>
                <div className="uk-grid uk-child-width-1-3">
                    {
                        this.props.friendList.map((value, i) =>
                            {
                                return <div key={i} className="uk-margin-top">
                                    <FriendCard avatarSrc={value.avatarSrc} viewName={value.viewName} />
                                </div>
                            }
                        )
                    }
                </div>
            </div>
        );
    }
}

export default FriendListPage;
