import React, {Component} from 'react';
import {connect} from 'react-redux';
import {
    cancelEventById,
    getAllGroupEvents,
    reopenEventById,
    setEventVisibility
} from "store/redux/actions/groupEventActions";
import {IGroupEvent} from "../../models/group-event";
import GroupEventCard from "../GroupEventCard/GroupEventCard";

type Props = {
    groupId: number;
    getAllGroupEvents(groupId: number):void;
    cancelEventById(groupId: number, eventId: string):void;
    reopenEventById(groupId: number, eventId: string):void;
    setEventVisibility(groupId: number, eventId: string, isVisible: boolean):void;
    groupEvents: Array<IGroupEvent>;
    groupEventsLoaded: boolean;
}

type State = {
    showHidden: boolean;
}

class GroupEventsSettings extends Component<Props, State> {
    constructor(props) {
        super(props);
        this.state = {
            showHidden: false
        }
    }


    componentDidMount() {
        this.props.getAllGroupEvents(this.props.groupId);
    }

    onCancelClick = (groupEventId:string) => {
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

    getFilteredEvents = () => {
        return this.props.groupEvents
            .filter(value => {
                if(!this.state.showHidden){
                    return value.isHidden === false
                } else return value;
            })
    }

    render() {
        return (
            <div className="group-events-settings">
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
                <div className="uk-grid uk-flex-middle uk-child-width-1-3 uk-grid-margin uk-margin-remove" data-uk-grid>
                    {
                        this.props.groupEventsLoaded && this.getFilteredEvents()
                            .map((value, index) =>
                            {
                                return <GroupEventCard key={index}
                                                       groupEventData={value}
                                                       onReopenEventClick={this.onReopenClick}
                                                       onHideClick={this.onHideClick}
                                                       onMakeVisibleClick={this.onMakeVisibleClick}
                                                       onCancelClick={this.onCancelClick} />
                            })
                    }
                </div>
            </div>
        );
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        getAllGroupEvents: (groupId: number) => dispatch(getAllGroupEvents(groupId)),
        cancelEventById: (groupId: number, eventId: string) => dispatch(cancelEventById(groupId, eventId)),
        reopenEventById: (groupId: number, eventId: string) => dispatch(reopenEventById(groupId, eventId)),
        setEventVisibility: (groupId: number, eventId: string, isVisible: boolean) => dispatch(setEventVisibility(groupId, eventId, isVisible)),
    }
}

function mapStateToProps(state) {
    return {
        groupEvents: state.groupEvent.groupEvents,
        groupEventsLoaded: state.groupEvent.groupEventsLoaded,
    };
}

export default connect(
    mapStateToProps, mapDispatchToProps
)(GroupEventsSettings);
