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
import {PostCommentService} from "../../features/post/post-comment-service";
import {connect} from "react-redux";
import {
    createGroupPost,
    createGroupPostComment,
    deletePost,
    getGroupPosts,
    removeComment
} from "../../store/redux/actions/postActions";

type Props = {
    getGroupPosts(groupId: number, page: number);
    deleteGroupPost(postId: string);
    createPost(post: Partial<IGroupPost>);
    createGroupPostComment(data: IGroupPostCommentCreation);
    removeComment(postCommentId: string);
    groupPosts: Array<IGroupPost>;
    groupPostsLoaded: boolean;
    match: any;
    location: any;
    history: any;
}

type State = {
    groupData: IGroupData;
    groupDataLoaded: boolean;
    //groupPosts: Array<IGroupPost>;
    groupViewerRole: EnGroupViewerRole;
    basicAdminPermissionsAvailable: boolean;
    groupParticipants: Array<IGroupParticipant>;
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
            //groupPosts: null,
            groupViewerRole: null,
            basicAdminPermissionsAvailable: false,
            groupParticipants: null
        }
    }

    async componentDidMount() {
        await this.getGroupData()

        this.getViewerRole()
            .then(() => {
                //this.getGroupPosts(1);
                this.props.getGroupPosts(this.props.match.params.groupId, 1);
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

    getParticipants = () => {
        this.groupParticipantService.getGroupParticipantsByGroupId(this.props.match.params.groupId, 1).then(res => {
            if (res.status === StatusCodes.OK) {
                this.setState({
                    groupParticipants: res.data as Array<IGroupParticipant>
                })
            }
        })
    }


    render() {
        return (
            <GroupPage groupParticipants={this.state.groupParticipants}
                       onRemoveComment={this.props.removeComment}
                       onDeletePost={this.props.deleteGroupPost}
                       onPostCreated={this.props.createPost}
                       onCommentCreated={this.props.createGroupPostComment}
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
        removeComment: (postCommentId: string) => dispatch(removeComment(postCommentId))
    }
}

const mapStateToProps = (state) => {
    return {
        groupPosts: state.post.posts,
        groupPostsLoaded: state.post.postsLoaded
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(GroupPageContainer);
