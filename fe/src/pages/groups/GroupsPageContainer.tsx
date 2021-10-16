import React, {Component} from 'react';
import BasePageComponent from "core/BasePageComponent";
import {container} from "tsyringe";
import {GroupService} from "features/group/services/group-service";
import GroupsPage from "./GroupsPage";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {IGroupCard} from "../../features/group/models/group-models";
import {GroupParticipantService} from "../../features/group/services/group-participant-service";
import {StatusCodes} from "http-status-codes";
import IDropdownOption from "../../common/components/SimpleDropdown/IDropdownOption";

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
        this.setGroups(0);
    }

    setGroups = (filterType: number) => {
        this.groupParticipant.getUserGroups(1, filterType).then((res) => {
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

    onSortDropdownOptionSelection = (option: IDropdownOption) => {
        this.setGroups(option.id);
    }

    render() {
        return (
            <>
                <GroupsPage groupList={this.state.groups}
                            groupsLoaded={this.state.groupsLoaded}
                            leaveGroup={this.leaveGroup}
                            onSortDropdownOptionSelection={this.onSortDropdownOptionSelection}
                            onSearchInputChange={this.onSearchInputChange} />
            </>
        );
    }
}

export default GroupsPageContainer;
