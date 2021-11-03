import React, {Component} from 'react';
import PageAvatar from "common/components/PageAvatar/PageAvatar";
import {Link, withRouter} from "react-router-dom";
import GroupBasicSettingsSubpage from "./subpages/GroupBasicSettingsSubpage";
import GroupParticipantsSettings from "./subpages/GroupParticipantsSettings";
import {IGroupData} from "../../features/group/models/group-models";
import UiKitDefaultSpinner from "../../common/components/UIKitDefaultSpinner/UIKitDefaultSpinner";
import GroupEventsSettings from "./subpages/GroupEventsSettingsSubpage";
import "./group-settings-page.scss"
import {Routes} from "../../core/routing";

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

        this.setState({
            chosenSectionId: index
        })
    }

    getTabIndex = () => {
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

        return index;
    }

    navigate = (e, link:string) => {
        e.preventDefault();
        this.props.history.push(link);
    }

    render() {
        return (
            <div className="group-settings-page">
                <div className="group-settings-page__group-avatar-wrap uk-flex">
                    <Link to={`/${Routes.groupPageRoutes.baseLink}/${this.props.match.params.groupId}`}
                          className="group-settings-page__get-back-btn uk-background-muted uk-text-center uk-margin-remove-bottom"
                          style={{width: "4%"}}>
                        <span className="uk-icon"
                              uk-icon="chevron-double-left"></span>
                    </Link>
                    <div className="max-width">
                        {
                            this.props.basicGroupDataLoaded ? <PageAvatar viewTitle={this.props.basicGroupData.name}
                                                                          avatarSrc={this.props.basicGroupData.avatarSrc} /> : <UiKitDefaultSpinner />
                        }
                    </div>
                </div>

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
                        <GroupBasicSettingsSubpage groupData={this.props.basicGroupData} /> : <UiKitDefaultSpinner />) }
                    {  this.state.chosenSectionId === 2 && <GroupParticipantsSettings groupId={this.props.match.params.groupId} /> }
                    { this.state.chosenSectionId === 3 && <GroupEventsSettings groupId={this.props.match.params.groupId} /> }
                </div>
            </div>
        );
    }
}

export default withRouter(GroupSettingsPage);