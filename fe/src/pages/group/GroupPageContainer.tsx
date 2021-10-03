import React, {Component} from 'react';
import GroupPage from "./GroupPage";
import {EnGroupViewerRole, IGroupData, IGroupParticipant, IGroupViewer} from "../../features/group/group-models";
import {GroupService} from "../../features/group/group-service";
import {StatusCodes} from "http-status-codes";
import {GroupPostService} from "../../features/post/group-post-service";
import {IGroupPost} from "../../features/post/models/group-post";
import {AuthStore} from "../../store/auth/auth-store";
import {GroupParticipantService} from "../../features/group/group-participant-service";
import {IPostComment} from "../../features/post/models/post-comment";
import {PostService} from "../../features/post/post-service";

type Props = {
    match: any,
    //onCommentCreated(data: Partial<IPostComment>);
}

type State = {
    groupData: IGroupData;
    groupDataLoaded: boolean;
    groupPosts: Array<IGroupPost>;
    //groupViewer: IGroupViewer;
    groupViewerRole: EnGroupViewerRole;
    //deletePostBtnVisible: boolean;
    basicAdminPermissionsAvailable: boolean;
    //groupPostsLoaded: boolean;
    groupParticipants: Array<IGroupParticipant>;
}

class GroupPageContainer extends Component<Props, State>{
    groupService: GroupService = new GroupService();
    groupPostService: GroupPostService = new GroupPostService();
    groupParticipantService: GroupParticipantService = new GroupParticipantService();
    postService = new PostService();

    constructor(props) {
        super(props);
        this.state = {
            groupDataLoaded: false,
            groupData: null,
            groupPosts: null,
            groupViewerRole: null,
            basicAdminPermissionsAvailable: false,
            groupParticipants: null
            //groupPostsLoaded: false
        }
    }

    componentDidMount() {
        this.getViewerRole()
            .then(() => {
                this.getGroupData();
                this.getGroupPosts(1);
                this.getParticipants();
            })
    }

    getViewerRole = () => {
        return this.groupParticipantService.getViewerRole(AuthStore.identity.userId, this.props.match.params.groupId)
            .then(res => {
                const groupViewerRole = (res.data as IGroupViewer).groupViewerRole;
                this.setState({
                    groupViewerRole,
                    basicAdminPermissionsAvailable: groupViewerRole === EnGroupViewerRole.Creator
                        ||  groupViewerRole === EnGroupViewerRole.Admin
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

    getParticipants = () => {
        this.groupParticipantService.getGroupParticipantsByGroupId(this.props.match.params.groupId, 1).then(res => {
            if(res.status === StatusCodes.OK){
                this.setState({
                    groupParticipants: res.data as Array<IGroupParticipant>
                })
            }
        })
    }

    onCommentCreated = (data: Partial<IPostComment>) => {
        console.log(data)
    }

    onPostCreated = (data: Partial<IGroupPost>) => {
        console.log(data)
        this.groupPostService.create(data).then(res => {
            if(res.status === StatusCodes.CREATED){
                const createdPost = res.data as IGroupPost;
                this.setState({
                    groupPosts: [createdPost, ...this.state.groupPosts]
                })
            }
        })
    }

    onDeletePost = (postId: string) => {
        console.log(postId)
        this.postService.removePostById(postId).then(res => {
            if(res.status === StatusCodes.OK){
                this.setState({
                    groupPosts: [...this.state.groupPosts.filter(e=>e.postId !== postId)]
                })
            }
        })
    }

    render() {
        return (
            <GroupPage groupParticipants={this.state.groupParticipants}
                       onDeletePost={this.onDeletePost}
                       onPostCreated={this.onPostCreated}
                       onCommentCreated={this.onCommentCreated}
                       basicAdminPermissionsAvailable={this.state.basicAdminPermissionsAvailable}
                       groupData={this.state.groupData}
                       groupPosts={this.state.groupPosts}
                       groupDataLoaded={this.state.groupDataLoaded}
            />
        );
    }
}

export default GroupPageContainer;
