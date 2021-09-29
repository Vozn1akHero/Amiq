import React from 'react';
import BasePageComponent from "core/BasePageComponent";
import {container} from "tsyringe";
import {GroupService} from "features/group/group-service";
import GroupsPage from "./GroupsPage";
import {AuthStore} from "../../store/auth/auth-store";
import {IGroupCard} from "../../features/group/group-models";
import {GroupParticipantService} from "../../features/group/group-participant-service";

class GroupsPageContainer extends BasePageComponent {
    //readonly groupService : GroupService = container.resolve(GroupService);
    readonly groupParticipant = new GroupParticipantService();

    groupsMock: Array<any> = [];

    constructor(props:any) {
        super(props);
        this.setGroups();
        //this.groupsMock = this.groupService.getGroupsByUserId("");
    }

    setGroups = () => {
        this.groupParticipant.getUserGroups(AuthStore.identity.userId).then((res) => {
            const data  = res.data as Array<IGroupCard>;
            this.setState({
                groups: data
            })
        })
    }

    leaveGroup = (groupId: number) => {
        //this.groupParticipant
    }

    onSearchInputChange = (e : InputEvent) => {
        console.log(e)
    }

    render() {
        return (
            <>
                <GroupsPage groupList={this.groupsMock}
                            leaveGroup={this.leaveGroup}
                            onSearchInputChange={this.onSearchInputChange} />
            </>
        );
    }
}

export default GroupsPageContainer;
