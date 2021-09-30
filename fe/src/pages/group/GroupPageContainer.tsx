import React, {Component} from 'react';
import GroupPage from "./GroupPage";
import {EnGroupViewerRole, IGroupData, IGroupViewer} from "../../features/group/group-models";
import {GroupService} from "../../features/group/group-service";
import {StatusCodes} from "http-status-codes";
import {GroupPostService} from "../../features/post/group-post-service";
import {IGroupPost} from "../../features/post/models/group-post";
import {AuthStore} from "../../store/auth/auth-store";
import {GroupParticipantService} from "../../features/group/group-participant-service";

type Props = {
    match: any
}

type State = {
    groupData: IGroupData;
    groupDataLoaded: boolean;
    groupPosts: Array<IGroupPost>;
    //groupViewer: IGroupViewer;
    groupViewerRole: EnGroupViewerRole
    //groupPostsLoaded: boolean;
}

class GroupPageContainer extends Component<Props, State>{
    participants = []
    groupService: GroupService = new GroupService();
    groupPostService: GroupPostService = new GroupPostService();
    groupParticipantService: GroupParticipantService = new GroupParticipantService();

    constructor(props) {
        super(props);
        this.state = {
            groupDataLoaded: false,
            groupData: null,
            groupPosts: null,
            groupViewerRole: null
            //groupPostsLoaded: false
        }
    }

    componentDidMount() {
        this.getViewerRole()
            .then(() => {
                this.getGroupData();
                this.getGroupPosts(1);
            })
    }

    getViewerRole = () => {
        return this.groupParticipantService.getViewerRole(AuthStore.identity.userId, this.props.match.params.groupId)
            .then(res => {
                this.setState({
                    groupViewerRole: (res.data as IGroupViewer).groupViewerRole
                })
        })
    }

    getGroupData = () => {
        this.groupService.getGroupById(this.props.match.params.groupId).then(res => {
            if(res.status === StatusCodes.OK){
                this.setState({
                    groupData: res.data as IGroupData,
                    groupDataLoaded: true
                })
            }
        })
    }

    getGroupPosts = (page: number) => {
        this.groupPostService.getPostsByGroupId(this.props.match.params.groupId, page).then(res => {
            console.log(res)
            this.setState({
                groupPosts: res.data as Array<IGroupPost>
            })
        })
    }

    render() {
        return (
            <GroupPage participants={this.participants}
                       groupData={this.state.groupData}
                       groupPosts={this.state.groupPosts}
                       groupDataLoaded={this.state.groupDataLoaded}
            />
        );
    }
}

export default GroupPageContainer;
