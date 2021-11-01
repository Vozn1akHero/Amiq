import React, {Component} from 'react';
import UserCard from "features/user/components/UserCard/UserCard";
import {IUserCardControl} from "features/user/components/UserCard/IUserCardControl";
import {IGroupParticipant} from "../../../features/group/models/group-models";
import {connect} from "react-redux";
import UiKitDefaultSpinner from "../../../common/components/UIKitDefaultSpinner/UIKitDefaultSpinner";
import {getGroupParticipants} from 'store/redux/actions/groupParticipantActions';
import {IPaginatedStoreData} from "../../../store/redux/base/paginated-store-data";
import SearchInput from "../../../common/components/SearchInput/SearchInput";
import "./group-participants-settings-subpage.scss"
import InfiniteScroll from "react-infinite-scroll-component";

type Props = {
    groupId: number;
    groupParticipants: IPaginatedStoreData<IGroupParticipant>;
    getParticipants(groupId, page): void;
}

class GroupParticipantsSettingsSubpage extends Component<Props> {
    cardControls: Array<IUserCardControl>;

    onBlockClick = (userId: number) => {
        console.log(userId)
    }

    constructor(props) {
        super(props);

        this.cardControls = [
            {
                icon: "ban",
                event: this.onBlockClick
            }
        ];
    }

    componentDidMount() {
        this.props.getParticipants(this.props.groupId, 1);
    }

    search = (text: string) => {

    }

    render() {
        return (
            <div className="group-participants-settings">
                <div className="uk-margin-medium-top">
                    <SearchInput onDebounceInputChange={this.search} showSpinner={false}/>
                </div>
                {
                    this.props.groupParticipants.loaded &&
                        <InfiniteScroll
                            dataLength={this.props.groupParticipants.entities.length}
                            next={() => {
                                this.props.getParticipants(this.props.groupId, this.props.groupParticipants.currentPage);
                            }}
                            hasMore={this.props.groupParticipants.length > this.props.groupParticipants.entities.length}
                            loader={<UiKitDefaultSpinner/>}
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
            </div>
        );
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        getParticipants: (groupId: number, page: number) => dispatch(getGroupParticipants(groupId, page))
    }
};

const mapStateToProps = (state) => {
    console.log(state)
    return {
        groupParticipants: state.groupParticipant.groupParticipants
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(GroupParticipantsSettingsSubpage);