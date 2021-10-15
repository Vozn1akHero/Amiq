import React, {Component} from 'react';
import {connect} from 'react-redux';

type Props = {
    groupId: number
}

class GroupEventsSettings extends Component<Props> {
    render() {
        return (
            <div className="group-events-settings">

            </div>
        );
    }
}

const mapDispatchToProps = (dispatch) => {
    return {}
}

function mapStateToProps(state) {
    return {};
}

export default connect(
    mapStateToProps, mapDispatchToProps
)(GroupEventsSettings);
