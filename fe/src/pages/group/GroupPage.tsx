import React, {Component} from 'react';
import {ItemsFrameL} from "common/components/ItemsFrameL/ItemsFrameL";
import PostCreationForm from "features/post/PostCreationForm";
import Post from "features/post/Post";
import {EnGroupViewerRole, IGroupData, IGroupParticipant} from "../../features/group/group-models";
import PageAvatar from "../../common/components/PageAvatar/PageAvatar";
import {IGroupPost} from "../../features/post/models/group-post";
import {Utils} from "../../core/utils";
import {IPostComment} from "../../features/post/models/post-comment";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {IUserInFrame} from "../../common/components/ItemsFrameL/IUserInFrame";

type Props = {
    groupData: IGroupData;
    groupDataLoaded: boolean;
    groupPosts: Array<IGroupPost>;
    basicAdminPermissionsAvailable: boolean;
    groupParticipants: Array<IGroupParticipant>;
    onCommentCreated(data: Partial<IPostComment>);
    onPostCreated(data: Partial<IGroupPost>);
    onDeletePost(postId: string);
    //groupViewerRole: EnGroupViewerRole;
}

class GroupPage extends Component<Props, any>  {
    onPostCreated = (text: string) => {
        const {groupData} = this.props;
        const post : Partial<IGroupPost> = {
            textContent: text,
            groupId: groupData.groupId,
            author: {
                userId: AuthStore.identity.userId
            }
        };
        this.props.onPostCreated(post);
    }

    onDeletePost = (postId: string) => {
        this.props.onDeletePost(postId);
    }

    getConvertedParticipantsToFrameItem = () => {
        if(this.props.groupParticipants){
            let arr : Array<IUserInFrame> = [];
            this.props.groupParticipants.map(e=>{
                arr.push({
                    userId: e.userId,
                    viewName: e.name + " " + e.surname,
                    imagePath: e.avatarPath,
                    link: "/profile/" + e.userId
                })
            })
            return arr;
        }
    }

    render() {
        return (
            <div className="group-page uk-flex-center uk-grid uk-child-width-1-2">

                    <div className="uk-grid-item-match uk-first-column uk-width-1-3">
                        { this.props.groupDataLoaded && <PageAvatar avatarSrc={this.props.groupData.avatarSrc}
                                                                    viewTitle={this.props.groupData.name}/> }
                    </div>
                    <div className="uk-preserve-width uk-margin-left">
                        <h3>O nas</h3>
                        <p>
                            {this.props.groupData?.description}
                        </p>
                        {this.props.groupData?.descriptionBlocks && <ul uk-accordion="collapsible: false">
                            {
                                this.props.groupData.descriptionBlocks.map(((value, index) => {
                                    return <li key={index}>
                                        <a className="uk-accordion-title" href="#">{value.header}</a>
                                        <div className="uk-accordion-content">
                                            <p>{value.content}</p>
                                        </div>
                                    </li>
                                }))
                            }
                        </ul>}
                    </div>
                    <div className="uk-first-column uk-margin-medium-top uk-width-1-3">
                        {
                            this.props.groupParticipants && <ItemsFrameL title="Uczestnicy"
                                                                         items={this.getConvertedParticipantsToFrameItem()}
                                                                         callbackText="Brak uczestników" />
                        }
                    </div>
                    <div className="uk-margin-left uk-margin-large-top">
                        <div className="uk-margin-medium-bottom">
                            <PostCreationForm handleSubmit={this.onPostCreated}
                                              publishAsAdminOptionVisible={this.props.basicAdminPermissionsAvailable} />
                        </div>
                        {
                            this.props.groupPosts != null && this.props.groupPosts.map((post, index) => {
                                return <Post postId={post.postId}
                                             onDeletePost={this.onDeletePost}
                                             onCommentCreated={this.props.onCommentCreated}
                                             hasMoreCommentsThanPassed={post.hasMoreCommentsThanRecent}
                                             comments={post.recentComments}
                                             avatarPath={Utils.getImageSrc(post.avatarPath)}
                                             text={post.textContent}
                                             authorLink={"/group/"+post.groupId}
                                             createdAt={post.createdAt}
                                             viewName={post.groupName}
                                             publishCommentAsAdminOptionVisible={this.props.basicAdminPermissionsAvailable}
                                             deleteBtnVisible={this.props.basicAdminPermissionsAvailable}
                                             key={index} />
                            })
                        }
                    </div>

            </div>
        );
    }
}

export default GroupPage;
