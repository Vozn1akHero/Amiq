import React from 'react';
import BasePageComponent from "core/BasePageComponent";
import {container} from "tsyringe";
import {GroupService} from "features/group/group-service";
import GroupsPage from "./GroupsPage";

class GroupsPageContainer extends BasePageComponent {
    readonly groupService : GroupService = container.resolve(GroupService);
    groupsMock: Array<any> = [];

    constructor(props:any) {
        super(props);

        this.groupsMock = this.groupService.getGroupsByUserId("");
    }

    onSearchInputChange = (e : InputEvent) => {
        console.log(e)
    }

    render() {
        return (
            <>
                <GroupsPage groupList={this.groupsMock}
                            onSearchInputChange={this.onSearchInputChange} />
            </>
        );
    }
}

export default GroupsPageContainer;
