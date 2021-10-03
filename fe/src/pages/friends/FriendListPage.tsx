import React, {Component} from 'react';
import FriendCard from "features/friend/components/FriendCard";
import DebounceInput from "../../common/components/DebounceInput/DebounceInput";

type Props = {
    friendList: Array<any>;
    friendsLoaded: boolean;
    onSearchInputChange: (text: string) => void;
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
                        <DebounceInput debounceTime={600}
                                       onDebounceInputChange={(e) => this.props.onSearchInputChange(e)} />
                    </div>
                </div>
                <div className="uk-grid uk-child-width-1-3">
                    {
                        this.props.friendsLoaded && this.props.friendList.map((value, i) =>
                            {
                                return <div key={i} className="uk-margin-top">
                                    <FriendCard friendship={value} key={i} />
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
