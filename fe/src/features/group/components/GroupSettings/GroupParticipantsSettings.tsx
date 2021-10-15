import React, {Component} from 'react';
import UserCard from "common/components/UserCard/UserCard";
import {IUserCardControl} from "common/components/UserCard/IUserCardControl";
import {IGroupParticipant} from "../../group-models";
import {GroupParticipantService} from "../../group-participant-service";
import {connect} from "react-redux";
import {GET_PARTICIPANTS} from "../../../../store/redux/types/groupParticipantTypes";
import UiKitDefaultSpinner from "../../../../common/components/UIKitDefaultSpinner/UIKitDefaultSpinner";
import { getParticipants } from 'store/redux/actions/groupParticipantActions';

type Props = {
    groupId: number;
    groupParticipants: Array<IGroupParticipant>;
    participantsLoaded: boolean;
    getParticipants(groupId, page):void;
}

class GroupParticipantsSettings extends Component<Props> {
    cardControls: Array<IUserCardControl>;
    groupParticipantService: GroupParticipantService = new GroupParticipantService();

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

    componentDidMount() {
        this.props.getParticipants(this.props.groupId, 1);
    }

    render() {
        return (
            <div className="group-participants-settings">
                {
                    this.props.participantsLoaded ? <div className="uk-grid uk-child-width-1-3">
                        {this.props.groupParticipants.map((value, index) => {
                            return <UserCard key={index}
                                             userId={value.userId}
                                             surname={value.surname}
                                             name={value.name}
                                             avatarPath={value.avatarPath}
                                             controls={this.cardControls} />;
                        })}
                    </div> : <UiKitDefaultSpinner />
                }
            </div>
        );
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        getParticipants: (groupId: number, page: number) => dispatch(getParticipants(groupId, page))
    }
};

const mapStateToProps = (state) => {
    console.log(state)
    return {
        groupParticipants: state.groupParticipant.participants,
        participantsLoaded: state.groupParticipant.participantsLoaded
    }// as Partial<Props>
};

export default connect(mapStateToProps, mapDispatchToProps)(GroupParticipantsSettings);