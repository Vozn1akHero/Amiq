import React, {Component} from 'react';
import GroupPage from "./GroupPage";
import {EnGroupViewerRole, IGroupData, IGroupParticipant, IGroupViewer} from "../../features/group/models/group-models";
import {GroupService} from "../../features/group/services/group-service";
import {StatusCodes} from "http-status-codes";
import {GroupPostService} from "../../features/post/group-post-service";
import {IGroupPost} from "../../features/post/models/group-post";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {GroupParticipantService} from "../../features/group/services/group-participant-service";
import {IGroupPostCommentCreation} from "../../features/post/models/post-comment";
import {PostService} from "../../features/post/post-service";
import {connect} from "react-redux";
import {
    createGroupPost,
    createGroupPostComment,
    deletePost,
    getGroupPosts,
    removeComment
} from "../../store/redux/actions/postActions";
import {IPaginatedStoreData} from "../../store/redux/base/paginated-store-data";
import {getGroupParticipants} from "../../store/redux/actions/groupParticipantActions";
import {getGroupEvents} from "../../store/redux/actions/groupEventActions";
import {IIdBasedPersistentData} from "../../store/redux/base/id-based-persistent-data";
import {IGroupEvent} from "../../features/group/models/group-event";

type Props = {
    getGroupEvents(groupId: number, page: number, count: number):void;
    getGroupPosts(groupId: number, page: number):void;
    deleteGroupPost(postId: string):void;
    createPost(post: Partial<IGroupPost>):void;
    createGroupPostComment(data: IGroupPostCommentCreation):void;
    removeComment(postCommentId: string):void;
    getGroupParticipants(groupId: number, page: number):void;
    groupEvents: IIdBasedPersistentData<IPaginatedStoreData<IGroupEvent>>;
    groupPosts: Array<IGroupPost>;
    groupPostsLoaded: boolean;
    groupParticipants: IPaginatedStoreData<IGroupParticipant>,
    match: any;
    location: any;
    history: any;
}

type State = {
    groupData: IGroupData;
    groupDataLoaded: boolean;
    groupViewerRole: EnGroupViewerRole;
    basicAdminPermissionsAvailable: boolean;
}

class GroupPageContainer extends Component<Props, State> {
    groupService: GroupService = new GroupService();
    groupPostService: GroupPostService = new GroupPostService();
    groupParticipantService: GroupParticipantService = new GroupParticipantService();
    postService = new PostService();

    constructor(props) {
        super(props);
        this.state = {
            groupDataLoaded: false,
            groupData: null,
            groupViewerRole: null,
            basicAdminPermissionsAvailable: false,
        }
    }

    async componentDidMount() {
        await this.getGroupData()

        this.getViewerRole()
            .then(() => {
                const{groupId} = this.props.match.params;
                this.props.getGroupParticipants(groupId, 1);
                this.props.getGroupPosts(groupId, 1);
                this.props.getGroupEvents(groupId, 1, 10);
            })
    }

    getViewerRole = () => {
        return this.groupParticipantService.getViewerRole(AuthStore.identity.userId, this.props.match.params.groupId)
            .then(res => {
                const groupViewerRole = (res.data as IGroupViewer).groupViewerRole;
                this.setState({
                    groupViewerRole,
                    basicAdminPermissionsAvailable: groupViewerRole === EnGroupViewerRole.Creator
                        || groupViewerRole === EnGroupViewerRole.Admin
                })
            })
    }

    getGroupData = () => {
        return this.groupService.getGroupById(this.props.match.params.groupId).then(res => {
            if (res.status === StatusCodes.OK) {
                this.setState({
                    groupData: res.data as IGroupData,
                    groupDataLoaded: true
                })
            }
        }).catch((err) => {
            if (err.response.status === StatusCodes.NOT_FOUND) {
                this.props.history.push("/not-found")
            } else {
                this.props.history.push("/profile")
            }
        })
    }

    createComment = (data: IGroupPostCommentCreation) => {
        data.groupId = this.state.groupData.groupId;
        this.props.createGroupPostComment(data);
    }

    getExemplaryGroupEvents = () => {

    }

    render() {
        return (
            <GroupPage groupParticipants={this.props.groupParticipants}
                       /*groupEvents={this.props.groupEvents?.entries?.find(e=>e.id===this.props.match.params.groupId).data.entities}
                       groupEventsLoaded={this.props.groupEvents?.entries?.find(e=>e.id===this.props.match.params.groupId).data.loaded}*/
                       groupEvents={this.props.groupEvents}
                       onRemoveComment={this.props.removeComment}
                       onDeletePost={this.props.deleteGroupPost}
                       onPostCreated={this.props.createPost}
                       onCommentCreated={this.createComment}
                       basicAdminPermissionsAvailable={this.state.basicAdminPermissionsAvailable}
                       groupData={this.state.groupData}
                       groupPosts={this.props.groupPosts}
                       groupPostsLoaded={this.props.groupPostsLoaded}
                       groupDataLoaded={this.state.groupDataLoaded}
            />
        );
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        getGroupPosts: (groupId: number, page: number) => dispatch(getGroupPosts(groupId, page)),
        deleteGroupPost: (postId: string) => dispatch(deletePost(postId)),
        createPost: (post: Partial<IGroupPost>) => dispatch(createGroupPost(post)),
        createGroupPostComment: (data: IGroupPostCommentCreation) => dispatch(createGroupPostComment(data)),
        removeComment: (postCommentId: string) => dispatch(removeComment(postCommentId)),
        getGroupParticipants: (groupId: number, page: number) => dispatch(getGroupParticipants(groupId, page)),
        getGroupEvents: (groupId: number, page: number, count: number) => dispatch(getGroupEvents(groupId, page, count)),
    }
}

const mapStateToProps = (state) => {
    return {
        groupPosts: state.post.posts,
        groupPostsLoaded: state.post.postsLoaded,
        groupParticipants: state.groupParticipant.groupParticipants,
        groupEvents: state.groupEvent.groupEvents
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(GroupPageContainer);
