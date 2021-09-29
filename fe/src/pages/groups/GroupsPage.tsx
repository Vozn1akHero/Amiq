import React, {Component} from 'react';
import MemoizedGroupCard from "features/group/components/GroupCard";
import {IGroupCard} from "../../features/group/group-models";
import {GroupParticipantService} from "../../features/group/group-participant-service";
import {AuthStore} from "../../store/auth/auth-store";

type Props = {
    groupList: Array<IGroupCard>;
    onSearchInputChange: (e:InputEvent) => void;
    leaveGroup(groupId: number):void;
};

type State = {

};

class GroupsPage extends Component<Props, State> {
    render() {
        return (
            <div className="friend-list-page">
                <legend className="uk-legend uk-margin-medium-top">Moje grupy</legend>
                <div className="input-search">
                    <div className="uk-margin-medium-top uk-margin-medium-bottom">
                        <input className="uk-input" type="text" placeholder="Szukaj grup"/>
                    </div>
                </div>
                <div className="uk-grid uk-child-width-1-3">
                    {
                        this.props.groupList.map((value, i) =>
                            {
                                return <div key={i} className="uk-margin-top">
                                    <MemoizedGroupCard leaveGroup={this.props.leaveGroup}
                                                       groupCard={value} />
                                </div>
                            }
                        )
                    }
                </div>
            </div>
        );
    }
}

export default GroupsPage;
