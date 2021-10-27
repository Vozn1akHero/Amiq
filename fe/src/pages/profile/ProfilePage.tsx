import React, {Component} from 'react';
import {ItemsFrameL} from "common/components/ItemsFrameL/ItemsFrameL";
import Post from "features/post/Post";
import PostCreationForm from "features/post/PostCreationForm";
import PageAvatar from "common/components/PageAvatar/PageAvatar";
import {IUserPost} from "features/post/models/user-post";
import {IUser} from "../../features/user/models/user";
import {Utils} from "../../core/utils";
import {IPostCommentCreation} from "../../features/post/models/post-comment";
import {EnPostType} from "../../features/post/en-post-type";
import {IFriendship} from "../../features/friend/friendship-models";
import {IUserInFrame} from "../../common/components/ItemsFrameL/IUserInFrame";
import ExemplaryUserFriendsInFrame
    from "../../features/friend/components/ExemplaryUserFriendsInFrame/ExemplaryUserFriendsInFrame";

type Props = {
    posts: Array<IUserPost>;
    postsLoaded: boolean;
    userData: IUser;
    userDataLoaded: boolean;
    isViewerProfile: boolean;
    userFriends: Array<IFriendship>;
    userFriendsLoaded: boolean;
    profileId: number;
    createPost(text: string);
    deletePost(postId: string);
    onCommentCreated(data: IPostCommentCreation);
    onRemoveComment(postCommentId: string);
}

class ProfilePage extends Component<Props> {
    buildPostProfileLink = (userId: number) => {
        return this.props.isViewerProfile ? "/profile/" : `/profile/${userId}`
    }

    onDeletePost = (postId: string) => {
        this.props.deletePost(postId);
    }

    render() {
        return (
            <div className='profile-page uk-flex-center uk-grid uk-child-width-1-2'>
                {this.props.userDataLoaded &&
                        <>
                            <div className="uk-grid-item-match uk-first-column uk-width-1-3">
                                <PageAvatar avatarSrc={this.props.userData.avatarPath}
                                            viewTitle={this.props.userData.name + " " + this.props.userData.surname}
                                            />
                            </div>
                            <div className="uk-grid-item-match uk-preserve-width uk-margin-left">
                                <h3>O mnie</h3>
                                <p>
                                    {this.props.userData.shortDescription}
                                </p>
                                {this.props.userData.userDescriptionBlocks && <ul uk-accordion="collapsible: false">
                                    {
                                        this.props.userData.userDescriptionBlocks.map(((value, index) => {
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
                        </>
                    }
                    <div className="uk-first-column uk-margin-medium-top uk-width-1-3">
                        <ExemplaryUserFriendsInFrame userId={this.props.profileId}
                                                     userFriendsLoaded={this.props.userFriendsLoaded}
                                                     userFriends={this.props.userFriends} />

                        {/*<div className="uk-margin-medium-top">
                            <ItemsFrameL title="Najlepsi znajomi"
                                         icon="users"
                                         items={[]}
                                         callbackText="Brak najlepszych znajomych" />
                        </div>*/}

                        <div className="uk-margin-medium-top">
                            <ItemsFrameL title="Linki"
                                         icon="world"
                                         items={[]}
                                         callbackText="Brak linków" />
                        </div>
                    </div>
                    <div className="uk-margin-left uk-margin-large-top">
                        {
                            this.props.isViewerProfile && <div className="uk-margin-medium-bottom">
                                <PostCreationForm handleSubmit={this.props.createPost}
                                                  publishAsAdminOptionVisible={false} />
                            </div>
                        }
                        {
                            this.props.posts.map((post, index) => {
                                return <Post postId={post.postId}
                                             postType={EnPostType.User}
                                             onRemoveComment={this.props.onRemoveComment}
                                             onDeletePost={this.onDeletePost}
                                             publishCommentAsAdminOptionVisible={false}
                                             onCommentCreated={this.props.onCommentCreated}
                                             avatarPath={Utils.getImageSrc(post.avatarPath)}
                                             text={post.textContent}
                                             authorLink={this.buildPostProfileLink(post.author.userId)}
                                             createdAt={post.createdAt}
                                             viewName={post.author.name + " " + post.author.surname}
                                             deleteBtnVisible={this.props.isViewerProfile}
                                             comments={post.comments}
                                             hasMoreCommentsThanPassed={post.hasMoreCommentsThanRecent}
                                             key={index} />
                            })
                        }
                    </div>
            </div>
        );
    }
}

export default ProfilePage;
