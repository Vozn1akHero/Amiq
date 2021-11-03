import React, {Component} from 'react';
import UserCard from "features/user/components/UserCard/UserCard";
import {IUserCardControl} from "features/user/components/UserCard/IUserCardControl";
import {IGroupParticipant} from "../../../features/group/models/group-models";
import {connect} from "react-redux";
import {
    blockUserInGroup,
    deleteGroupParticipant,
    getGroupParticipants
} from 'store/redux/actions/groupParticipantActions';
import {IPaginatedStoreData} from "../../../store/redux/base/paginated-store-data";
import SearchInput from "../../../common/components/SearchInput/SearchInput";
import "./group-participants-settings-subpage.scss"
import InfiniteScroll from "react-infinite-scroll-component";
import {GroupParticipantService} from "../../../features/group/services/group-participant-service";
import {withRouter} from "react-router-dom";

type Props = {
    groupId: number;
    groupParticipants: IPaginatedStoreData<IGroupParticipant>;
    getParticipants(groupId, page): void;
    blockUserInGroup(groupId: number, userId: number):void;
    deleteGroupParticipant(groupId: number, userId: number):void;
    match: any;
    location: any;
    history: any;
}

type State = {
    chosenSection: number;
}

class GroupParticipantsSettingsSubpage extends Component<Props, State> {
    cardControls: Array<IUserCardControl>;
    groupParticipantService = new GroupParticipantService();

    onBlockClick = (userId: number) => {
        this.props.blockUserInGroup(userId, this.props.groupId);
    }

    onRemoveParticipantClick = (userId: number) => {
        this.props.deleteGroupParticipant(userId, this.props.groupId);
    }

    constructor(props) {
        super(props);

        this.state = {
            chosenSection: 1
        }

        this.cardControls = [
            {
                icon: "trash",
                tooltip: "Usuń",
                event: this.onRemoveParticipantClick
            },
            {
                icon: "ban",
                tooltip: "Zablokuj",
                event: this.onBlockClick
            }
        ];
    }

    componentDidMount() {
        this.props.getParticipants(this.props.groupId, 1);
    }


    search = (text: string) => {

    }


    navigate =  (e, id: number) => {
        e.preventDefault();
        this.setState({
            chosenSection: id
        })
    }

    render() {
        return (
            <div className="group-participants-settings">
                <div className="uk-margin-medium-top">
                    <SearchInput onDebounceInputChange={this.search} showSpinner={false}/>
                </div>
                <div className="uk-margin-medium-top">
                    <ul className="uk-child-width-expand" uk-tab="true">
                        <li className={this.state.chosenSection === 1 ? `uk-active` : ""}>
                            <a onClick={e=>{this.navigate(e, 1)}}>
                                <span className="uk-margin-small-right" uk-icon="icon:nut"></span> Aktywni
                            </a>
                        </li>
                        <li className={this.state.chosenSection === 2 ? `uk-active` : ""}>
                            <a onClick={e=>{this.navigate(e, 2)}} >
                                <span className="uk-margin-small-right" uk-icon="icon:users"></span> Zablokowani
                            </a>
                        </li>
                    </ul>
                </div>
                {
                    this.state.chosenSection === 1 && <>
                        {
                            this.props.groupParticipants.loaded &&
                            <InfiniteScroll
                                dataLength={this.props.groupParticipants.entities.length}
                                next={() => {
                                    this.props.getParticipants(this.props.groupId, this.props.groupParticipants.currentPage);
                                }}
                                hasMore={this.props.groupParticipants.length > this.props.groupParticipants.entities.length}
                                /*loader={<UiKitDefaultSpinner/>}*/
                                loader={null}
                            >
                                <div className="group-participants-settings__entries uk-margin-medium-top">
                                    {this.props.groupParticipants.entities.map((value, index) => {
                                        return <UserCard key={index}
                                                         userId={value.userId}
                                                         surname={value.surname}
                                                         name={value.name}
                                                         avatarPath={value.avatarPath}
                                                         controls={this.cardControls}/>
                                    })}
                                </div>
                            </InfiniteScroll>
                        }
                    </>
                }
                {
                    this.state.chosenSection === 2 && <>

                    </>
                }
            </div>
        );
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        getParticipants: (groupId: number, page: number) => dispatch(getGroupParticipants(groupId, page)),
        blockUserInGroup: (groupId: number, userId: number) => dispatch(blockUserInGroup(groupId, userId)),
        deleteGroupParticipant: (groupId: number, userId: number) => dispatch(deleteGroupParticipant(groupId, userId)),
    }
};

const mapStateToProps = (state) => {
    return {
        groupParticipants: state.groupParticipant.groupParticipants
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(withRouter(GroupParticipantsSettingsSubpage));