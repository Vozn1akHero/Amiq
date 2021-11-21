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
    deletePost, getGroupPostComments,
    getGroupPosts,
    removeGroupPostComment
} from "../../store/redux/actions/postActions";
import {IPaginatedStoreData} from "../../store/redux/base/paginated-store-data";
import {getGroupParticipants} from "../../store/redux/actions/groupParticipantActions";
import {getGroupEvents} from "../../store/redux/actions/groupEventActions";
import {IIdBasedPersistentData} from "../../store/redux/base/id-based-persistent-data";
import {IGroupEvent} from "../../features/group/models/group-event";
import {IPageVisitationActivity} from "../../features/activity-tracking/models";
import moment from "moment";

type Props = {
    getGroupEvents(groupId: number, page: number, count: number):void;
    getGroupPosts(groupId: number, page: number):void;
    deleteGroupPost(postId: string):void;
    createPost(post: Partial<IGroupPost>):void;
    createGroupPostComment(data: IGroupPostCommentCreation):void;
    removeComment(postCommentId: string):void;
    getGroupParticipants(groupId: number, page: number):void;
    getGroupPostComments(postId: string, page: number): void;
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
    visitationTimeInMinutes: number;
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
            visitationTimeInMinutes: 0
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

        this.startTrackingActivity();
    }

    componentWillUnmount() {
        this.storeTrackingActivity();
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

    startTrackingActivity = () => {
        setTimeout(() => {
            this.setState({
                visitationTimeInMinutes: this.state.visitationTimeInMinutes + 1
            })
        }, 60000)
    }

    storeTrackingActivity = () => {
        if(!sessionStorage.getItem("act"))
            sessionStorage.setItem("act", JSON.stringify(""));

        let visitationState = JSON.parse(sessionStorage.getItem("act")) as IPageVisitationActivity;
        const{groupId} = this.props.match.params;

        if(!visitationState.groupVisitations){
            visitationState.groupVisitations = [];
        }

        let groupVisitationIndex = visitationState.groupVisitations.findIndex(e=>e.groupId === groupId);
        if(groupVisitationIndex!==-1){
            visitationState.groupVisitations = visitationState.groupVisitations.map((value, index) => {
                if(index === groupVisitationIndex){
                    value.visitationTotalTime += this.state.visitationTimeInMinutes;
                    value.lastVisited =  moment().toDate()
                }
                return value;
            });
        } else {
            visitationState.groupVisitations.push({
                groupId,
                lastVisited: moment().toDate(),
                visitationTotalTime: this.state.visitationTimeInMinutes
            })
        }

    }

    render() {
        return (
            <GroupPage groupParticipants={this.props.groupParticipants}
                       getComments={this.props.getGroupPostComments}
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
        removeComment: (postCommentId: string) => dispatch(removeGroupPostComment(postCommentId)),
        getGroupParticipants: (groupId: number, page: number) => dispatch(getGroupParticipants(groupId, page)),
        getGroupEvents: (groupId: number, page: number, count: number) => dispatch(getGroupEvents(groupId, page, count)),
        getGroupPostComments: (postId: string, page: number) => dispatch(getGroupPostComments(postId, page))
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
