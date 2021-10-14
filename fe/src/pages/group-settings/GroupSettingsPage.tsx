import React, {Component} from 'react';
import PageAvatar from "common/components/PageAvatar/PageAvatar";
import {Link, withRouter} from "react-router-dom";
import GroupBasicSettings from "../../features/group/components/GroupSettings/GroupBasicSettings";
import GroupParticipantsSettings from "../../features/group/components/GroupSettings/GroupParticipantsSettings";

type Props = {
    match: any;
    location: any;
    history: any;
}

type State = {
    chosenSectionId: number;
}

class GroupSettingsPage extends Component<Props, State> {
    constructor(props) {
        super(props);

        this.state = {
            chosenSectionId: 1
        }
    }


    componentDidMount() {
        console.log(this.props.location)
    }

    /*changeTab = (e, chosenSectionId: number) => {
        e.preventDefault();
        this.setState({
            chosenSectionId
        })
    }*/

    navigate = (e, link:string) => {
        e.preventDefault();
        this.props.history.push(link);

        let index : number;
        switch(this.props.location.hash){
            case "#basic":
                index = 1;
                break;
            case "#participants":
                index = 2;
                break;
            case "#events":
                index = 3;
                break;
            default:
                index = 1;
                break;
        }

        this.setState({
            chosenSectionId: index
        })
    }

    render() {
        return (
            <div className="group-settings-page">
                <PageAvatar viewTitle="Test" avatarSrc="user.jpg" />

                <div className="uk-margin-top">
                    <ul className="uk-child-width-expand" uk-tab="true">
                        <li className="uk-active">
                            <a href="#" onClick={e=>{this.navigate(e, this.props.location.pathname+"#basic")}}>
                                <span className="uk-margin-small-right" uk-icon="icon:nut"></span> Podstawowe
                            </a>
                        </li>
                        <li>
                            <a href="#" onClick={e=>{this.navigate(e, this.props.location.pathname+"#participants")}} >
                                <span className="uk-margin-small-right" uk-icon="icon:users"></span> Uczestnicy
                            </a>
                        </li>
                        <li>
                            <a href="#" onClick={e=>{this.navigate(e, this.props.location.pathname+"#events")}} >
                                <span className="uk-margin-small-right" uk-icon="icon:calendar"></span> Wydarzenia
                            </a>
                        </li>
                    </ul>
                </div>

                <div className="chosen-section">
                    {  this.state.chosenSectionId === 1 && <GroupBasicSettings /> }
                    {  this.state.chosenSectionId === 2 && <GroupParticipantsSettings /> }
                </div>
            </div>
        );
    }
}

export default withRouter(GroupSettingsPage);