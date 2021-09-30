import React, {Component} from 'react';
import BasePageComponent from "core/BasePageComponent";
import {container} from "tsyringe";
import {GroupService} from "features/group/group-service";
import GroupsPage from "./GroupsPage";
import {AuthStore} from "../../store/auth/auth-store";
import {IGroupCard} from "../../features/group/group-models";
import {GroupParticipantService} from "../../features/group/group-participant-service";
import {StatusCodes} from "http-status-codes";

type State = {
    groups: Array<IGroupCard>;
    groupsLoaded: boolean;
    cachedGroupsBeforeSearch: Array<IGroupCard>;
    initialSearch: boolean;
}

class GroupsPageContainer extends Component<any, State> {
    readonly groupService : GroupService = new GroupService();
    readonly groupParticipant = new GroupParticipantService();

    constructor(props) {
        super(props);
        this.state = {
            groups: [],
            cachedGroupsBeforeSearch: [],
            groupsLoaded: false,
            initialSearch: true
        }
    }

    componentDidMount() {
        this.setGroups();
    }

    setGroups = () => {
        this.groupParticipant.getUserGroups(1).then((res) => {
            const data  = res.data as Array<IGroupCard>;
            this.setState({
                groups: data,
                groupsLoaded: true
            })
        })
    }

    leaveGroup = (groupId: number) => {
        this.groupParticipant.leaveGroup(AuthStore.identity.userId, groupId).then(res => {
            if(res.status === StatusCodes.OK){
                this.setState({
                    groups: [...this.state.groups.filter(e=>e.groupId!==groupId)]
                })
            }
        })
    }

    onSearchInputChange = (text : string) => {
        if(!text && !this.state.initialSearch){
            this.setState({
                groups: this.state.cachedGroupsBeforeSearch
            })
        }
        if(text?.length > 0){
            this.groupService.getByName(text).then(res => {
                if(this.state.initialSearch){
                    this.setState({
                        cachedGroupsBeforeSearch: this.state.groups,
                        initialSearch: false
                    }, () => {
                        this.setState({
                            groups: res.data as Array<IGroupCard>
                        })
                    })
                } else {
                    this.setState({
                        groups: res.data as Array<IGroupCard>
                    })
                }
            })
        }
    }

    render() {
        return (
            <>
                <GroupsPage groupList={this.state.groups}
                            groupsLoaded={this.state.groupsLoaded}
                            leaveGroup={this.leaveGroup}
                            onSearchInputChange={this.onSearchInputChange} />
            </>
        );
    }
}

export default GroupsPageContainer;
