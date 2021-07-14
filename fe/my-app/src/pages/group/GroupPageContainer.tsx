import React, {Component} from 'react';
import GroupPage from "./GroupPage";

type Props = {
    match: any
}

type State = {}

class GroupPageContainer extends Component<Props, State>{
    participants = []

    componentDidMount() {
        console.log(this.props.match.params.groupId)
    }

    render() {
        return (
            <GroupPage participants={this.participants} />
        );
    }
}

export default GroupPageContainer;
