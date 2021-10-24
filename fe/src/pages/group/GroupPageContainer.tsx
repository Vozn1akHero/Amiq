import React, {Component} from 'react';
import GroupPage from "./GroupPage";
import {EnGroupViewerRole, IGroupData, IGroupParticipant, IGroupViewer} from "../../features/group/models/group-models";
import {GroupService} from "../../features/group/services/group-service";
import {StatusCodes} from "http-status-codes";
import {GroupPostService} from "../../features/post/group-post-service";
import {IGroupPost} from "../../features/post/models/group-post";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {GroupParticipantService} from "../../features/group/services/group-participant-service";
import {IPostComment, IPostCommentCreation} from "../../features/post/models/post-comment";
import {PostService} from "../../features/post/post-service";
import {withRouter} from "react-router-dom";
import {AxiosResponse} from "axios";
import {PostCommentService} from "../../features/post/post-comment-service";

type Props = {
    match: any;
    location: any;
    history: any;
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
    postCommentService = new PostCommentService();

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

    async componentDidMount() {
        await this.getGroupData()

        this.getViewerRole()
            .then(() => {
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
        return this.groupService.getGroupById(this.props.match.params.groupId).then(res => {
            if(res.status === StatusCodes.OK){
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

    getGroupPosts = (page: number) => {
        this.groupPostService.getPostsByGroupId(this.props.match.params.groupId, page).then(res => {
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

    onCommentCreated = (data: IPostCommentCreation) => {
        data.groupId = this.state.groupData.groupId;
        this.postCommentService.create(data).then(res => {
            if(res.status === StatusCodes.CREATED){
                const newComment = res.data as IPostComment;
                this.setState({
                    groupPosts: [
                        ...this.state.groupPosts.map((value, index) => {
                            if(value.postId === data.postId){
                                if(newComment.parentId){
                                    value.comments = value.comments.map(comment => {
                                        if(comment.commentId === newComment.parentId){
                                            comment.children = [...comment.children, newComment]
                                        }
                                        return comment;
                                    })
                                } else {
                                    const comments = value.comments == null ? [] : value.comments;
                                    value.comments = [newComment, ...comments]
                                }
                            }
                            return value;
                        })
                    ]
                })
            }
        })

    }

    onRemoveComment = (postCommentId: string) => {
        this.postCommentService.delete(postCommentId).then(res => {
            if(res.status === StatusCodes.OK){
                const removeComment = res.data as IPostComment;
                for(const groupPost of this.state.groupPosts){
                    if(groupPost.postId === removeComment.postId){
                        if(removeComment.parentId){
                            groupPost.comments = groupPost.comments.map(comment => {
                                if(comment.commentId === removeComment.parentId){
                                    comment.children = [...comment.children.filter(value => value.commentId !== removeComment.commentId)]
                                }
                                return comment;
                            })
                        } else {
                            groupPost.comments = groupPost.comments.filter(value => value.commentId !== removeComment.commentId)
                        }
                    }
                }
            }
        })
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
                       onRemoveComment={this.onRemoveComment}
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
