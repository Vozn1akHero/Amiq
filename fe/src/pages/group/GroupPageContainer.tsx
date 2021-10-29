import React, {Component} from 'react';
import GroupPage from "./GroupPage";
import {EnGroupViewerRole, IGroupData, IGroupParticipant, IGroupViewer} from "../../features/group/models/group-models";
import {GroupService} from "../../features/group/services/group-service";
import {StatusCodes} from "http-status-codes";
import {GroupPostService} from "../../features/post/group-post-service";
import {IGroupPost} from "../../features/post/models/group-post";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {GroupParticipantService} from "../../features/group/services/group-participant-service";
import {
    IGroupPostComment,
    IGroupPostCommentCreation,
    IPostComment,
    IPostCommentCreation
} from "../../features/post/models/post-comment";
import {PostService} from "../../features/post/post-service";
import {PostCommentService} from "../../features/post/post-comment-service";
import {connect} from "react-redux";
import {createGroupPost, deletePost, getGroupPosts} from "../../store/redux/actions/postActions";

type Props = {
    getGroupPosts(groupId: number, page: number);
    deleteGroupPost(postId: string);
    createPost(post: Partial<IGroupPost>);
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

    getParticipants = () => {
        this.groupParticipantService.getGroupParticipantsByGroupId(this.props.match.params.groupId, 1).then(res => {
            if(res.status === StatusCodes.OK){
                this.setState({
                    groupParticipants: res.data as Array<IGroupParticipant>
                })
            }
        })
    }

    //#region comments

    onCommentCreated = (data: IGroupPostCommentCreation) => {
        /*data.groupId = this.state.groupData.groupId;
        this.postCommentService.createGroupPostComment(data).then(res => {
            if(res.status === StatusCodes.CREATED){
                const newComment = res.data as IGroupPostComment;
                const groupPosts : Array<IGroupPost> = this.state.groupPosts.map((value, index) => {
                    if(value.postId === data.postId){
                        if(newComment.parentCommentId){
                            value.comments = value.comments.map(comment => {
                                if(comment.commentId === newComment.parentCommentId){
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
                });
                this.setState({
                    groupPosts
                }, () => {
                    console.log(this.state.groupPosts)
                })
            }
        })
*/
    }

    onRemoveComment = (postCommentId: string) => {
        /*this.postCommentService.delete(postCommentId).then(res => {
            if(res.status === StatusCodes.OK){
                const removeComment = res.data as IPostComment;
                for(const groupPost of this.state.groupPosts){
                    if(groupPost.postId === removeComment.postId){
                        if(removeComment.parentCommentId){
                            groupPost.comments = groupPost.comments.map(comment => {
                                if(comment.commentId === removeComment.parentCommentId){
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
        })*/
    }

    //#endregion

    //#region posts



    /*getGroupPosts = (page: number) => {
        this.groupPostService.getPostsByGroupId(this.props.match.params.groupId, page).then(res => {
            this.setState({
                groupPosts: res.data as Array<IGroupPost>
            })
        })
    }*/

    /*onPostCreated = (data: Partial<IGroupPost>) => {
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
    }*/

    //#endregion

    render() {
        return (
            <GroupPage groupParticipants={this.state.groupParticipants}
                       onRemoveComment={this.onRemoveComment}
                       onDeletePost={this.props.deleteGroupPost}
                       onPostCreated={this.props.createPost}
                       onCommentCreated={this.onCommentCreated}
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
        createPost: (post: Partial<IGroupPost>) => dispatch(createGroupPost(post))
    }
}

const mapStateToProps = (state) => {
    return {
        groupPosts: state.post.posts,
        groupPostsLoaded: state.post.postsLoaded
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(GroupPageContainer);
