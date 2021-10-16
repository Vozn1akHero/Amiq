import React, {Component} from 'react';
import PageAvatar from "common/components/PageAvatar/PageAvatar";
import {Link, withRouter} from "react-router-dom";
import GroupBasicSettings from "../../features/group/components/GroupSettings/GroupBasicSettings";
import GroupParticipantsSettings from "../../features/group/components/GroupSettings/GroupParticipantsSettings";
import {IGroupData} from "../../features/group/models/group-models";
import UiKitDefaultSpinner from "../../common/components/UIKitDefaultSpinner/UIKitDefaultSpinner";
import GroupEventsSettings from "../../features/group/components/GroupSettings/GroupEventsSettings";

type Props = {
    match: any;
    location: any;
    history: any;
    raiseGetBasicData():void;
    basicGroupData: IGroupData;
    basicGroupDataLoaded: boolean;
}

type State = {
    chosenSectionId: number;
}

class GroupSettingsPage extends Component<Props, State> {
    constructor(props) {
        super(props);

        this.state = {
            chosenSectionId: this.getTabIndex()
        }
    }


    componentDidMount() {
        this.raiseDataLoad();
    }

    componentWillUnmount() {
        //window.removeEventListener("hashchange", this.rerenderSubpageAfterHashChange, false);
    }

    componentDidUpdate(prevProps: Readonly<Props>, prevState: Readonly<State>, snapshot?: any) {
        if(this.getTabIndex() !== this.state.chosenSectionId){
            this.rerenderSubpageAfterHashChange();
        }
    }

    raiseDataLoad = () => {
        switch (this.state.chosenSectionId){
            case 1:
                this.props.raiseGetBasicData();
                break;
        }
    }

    rerenderSubpageAfterHashChange = () => {
        let index = this.getTabIndex();
        console.log(index)
        this.setState({
            chosenSectionId: index
        })
    }

    getTabIndex = () => {
        let index : number;
        console.log(this.props.location.hash)
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

        return index;
    }

    navigate = (e, link:string) => {
        e.preventDefault();
        this.props.history.push(link);
    }

    render() {
        return (
            <div className="group-settings-page">
                {
                    this.props.basicGroupDataLoaded ? <PageAvatar viewTitle={this.props.basicGroupData.name}
                                                                  avatarSrc={this.props.basicGroupData.avatarSrc} /> : <UiKitDefaultSpinner />
                }

                <div className="uk-margin-medium-top">
                    <ul className="uk-child-width-expand" uk-tab="true">
                        <li className={this.state.chosenSectionId === 1 ? `uk-active` : ""}>
                            <a href="#" onClick={e=>{this.navigate(e, "#basic")}}>
                                <span className="uk-margin-small-right" uk-icon="icon:nut"></span> Podstawowe
                            </a>
                        </li>
                        <li className={this.state.chosenSectionId === 2 ? `uk-active` : ""}>
                            <a href="#" onClick={e=>{this.navigate(e, "#participants")}} >
                                <span className="uk-margin-small-right" uk-icon="icon:users"></span> Uczestnicy
                            </a>
                        </li>
                        <li className={this.state.chosenSectionId === 3 ? `uk-active` : ""}>
                            <a href="#" onClick={e=>{this.navigate(e, "#events")}} >
                                <span className="uk-margin-small-right" uk-icon="icon:calendar"></span> Wydarzenia
                            </a>
                        </li>
                    </ul>
                </div>

                <div className="chosen-section">
                    {  this.state.chosenSectionId === 1 && (this.props.basicGroupDataLoaded ?
                        <GroupBasicSettings groupData={this.props.basicGroupData} /> : <UiKitDefaultSpinner />) }
                    {  this.state.chosenSectionId === 2 && <GroupParticipantsSettings groupId={this.props.match.params.groupId} /> }
                    { this.state.chosenSectionId === 3 && <GroupEventsSettings groupId={this.props.match.params.groupId} /> }
                </div>
            </div>
        );
    }
}

export default withRouter(GroupSettingsPage);