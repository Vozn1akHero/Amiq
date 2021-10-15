import React, {Component, useEffect, useState} from 'react';
import GroupSettingsPage from "./GroupSettingsPage";
import {StatusCodes} from "http-status-codes";
import {IGroupData, IGroupParticipant} from "../../features/group/group-models";
import {GroupService} from "../../features/group/group-service";
import {useHistory, useLocation, useParams} from "react-router-dom";

const GroupSettingsPageContainer = () => {
    const groupService: GroupService = new GroupService();

    const [basicGroupData, setBasicGroupData] = useState(null);
    const [basicGroupDataLoaded, setBasicGroupDataLoaded] = useState(false);

    const params = useParams();

    useEffect( ()=>{
        getBasicGroupData();
    }, [])

    const getBasicGroupData = () => {
        if(!basicGroupDataLoaded)
            return groupService.getGroupById(params["groupId"]).then(res => {
                if(res.status === StatusCodes.OK){
                    setBasicGroupData(res.data as IGroupData);
                    setBasicGroupDataLoaded(true);
                }
            })
    }



    return (
        <GroupSettingsPage raiseGetBasicData={getBasicGroupData}
                           basicGroupDataLoaded={basicGroupDataLoaded}
                           basicGroupData={basicGroupData} />
    );
}

export default GroupSettingsPageContainer;