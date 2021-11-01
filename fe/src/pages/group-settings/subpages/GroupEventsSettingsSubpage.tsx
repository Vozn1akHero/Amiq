import React, {Component} from 'react';
import {connect} from 'react-redux';
import {
    cancelEventById,
    getAllGroupEvents,
    reopenEventById,
    setEventVisibility
} from "store/redux/actions/groupEventActions";
import {IGroupEvent} from "../../../features/group/models/group-event";
import GroupEventCard from "../../../features/group/components/GroupEventCard/GroupEventCard";
import {IPaginatedStoreData} from "../../../store/redux/base/paginated-store-data";
import {IIdBasedPersistentData} from "../../../store/redux/base/id-based-persistent-data";
import "./group-events-settings-subpage.scss"
import SearchInput from "../../../common/components/SearchInput/SearchInput";

type Props = {
    groupId: number;
    getGroupEvents(groupId: number, page: number, count: number): void;
    cancelEventById(groupId: number, eventId: string): void;
    reopenEventById(groupId: number, eventId: string): void;
    setEventVisibility(groupId: number, eventId: string, isVisible: boolean): void;
    groupEvents: IIdBasedPersistentData<IPaginatedStoreData<IGroupEvent>>;
}

type State = {
    showHidden: boolean;
}

class GroupEventsSettingsSubpage extends Component<Props, State> {
    constructor(props) {
        super(props);
        this.state = {
            showHidden: false
        }
    }

    getCurrentGroupEvents = () => {
        const index: number = this.props.groupEvents.entries.findIndex(e => e.id === this.props.groupId);
        return this.props.groupEvents.entries[index].data;
    }

    componentDidMount() {
        this.props.getGroupEvents(this.props.groupId, 1, 10);
    }

    onCancelClick = (groupEventId: string) => {
        this.props.cancelEventById(this.props.groupId, groupEventId);
    }

    onReopenClick = (groupEventId: string) => {
        this.props.reopenEventById(this.props.groupId, groupEventId);
    }

    onHideClick = (groupEventId: string) => {
        this.props.setEventVisibility(this.props.groupId, groupEventId, false);
    }

    onMakeVisibleClick = (groupEventId: string) => {
        this.props.setEventVisibility(this.props.groupId, groupEventId, true);
    }

    search = (text: string) => {

    }

    render() {
        return (
            <div className="group-events-settings">
                <div className="uk-margin-medium-top">
                    <SearchInput onDebounceInputChange={this.search} showSpinner={false}/>
                </div>
                <div className="uk-margin-top uk-margin-bottom">
                    <label><input className="uk-checkbox"
                                  type="checkbox"
                                  onChange={() => {
                                      this.setState({
                                          showHidden: !this.state.showHidden
                                      })
                                  }}
                                  checked={this.state.showHidden}/> pokaż ukryte</label>
                </div>
                <div className="group-events-settings__entities">
                    {
                        this.props.groupEvents.entries.find(e => e.id === this.props.groupId)?.data.loaded
                        && this.props.groupEvents.entries.find(e => e.id === this.props.groupId).data.entities.filter(value => {
                            if(value.isHidden && this.state.showHidden){
                                return value;
                            } else if(value.isHidden && !this.state.showHidden){
                                return null;
                            } else return value;
                        }).map((value, key) => {
                            return <GroupEventCard key={key}
                                                   groupEventData={value}
                                                   onReopenEventClick={this.onReopenClick}
                                                   onHideClick={this.onHideClick}
                                                   onMakeVisibleClick={this.onMakeVisibleClick}
                                                   onCancelClick={this.onCancelClick}/>
                        })
                    }
                </div>
            </div>
        );
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        getGroupEvents: (groupId: number, page: number, count: number) => dispatch(getAllGroupEvents(groupId, page, count)),
        cancelEventById: (groupId: number, eventId: string) => dispatch(cancelEventById(groupId, eventId)),
        reopenEventById: (groupId: number, eventId: string) => dispatch(reopenEventById(groupId, eventId)),
        setEventVisibility: (groupId: number, eventId: string, isVisible: boolean) => dispatch(setEventVisibility(groupId, eventId, isVisible)),
    }
}

function mapStateToProps(state) {
    return {
        groupEvents: state.groupEvent.groupEvents
    };
}

export default connect(
    mapStateToProps, mapDispatchToProps
)(GroupEventsSettingsSubpage);
