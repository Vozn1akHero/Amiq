import React, {Component} from 'react';
import BasePageComponent from "core/BasePageComponent";
import {ItemsFrameL} from "common/components/ItemsFrameL/ItemsFrameL";
import Post from "features/post/Post";
import PostCreationForm from "features/post/PostCreationForm";
import PageAvatar from "common/components/PageAvatar/PageAvatar";
import {IUserPost} from "features/post/models/user-post";
import {IUser} from "../../features/user/models/user";
import {Utils} from "../../core/utils";
import {IPostComment} from "../../features/post/models/post-comment";

type Props = {
    posts: Array<IUserPost>;
    postsLoaded: boolean;
    userData: IUser;
    userDataLoaded: boolean;
    createPost(text: string);
    deletePost(postId: string);
    isViewerProfile: boolean;
    onCommentCreated(data: Partial<IPostComment>);
}

type State = {

}

class ProfilePage extends Component<Props> {
    friends = []

    buildPostProfileLink = (userId: number) => {
        return this.props.isViewerProfile ? "/profile/" : `/profile/${userId}`
    }

    onDeletePost = (postId: string) => {

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
                        <ItemsFrameL title="Znajomi" items={this.friends} callbackText="Nothing to show" />
                    </div>
                    <div className="uk-margin-left uk-margin-large-top">
                        <div className="uk-margin-medium-bottom">
                            <PostCreationForm handleSubmit={this.props.createPost}
                                              publishAsAdminOptionVisible={false} />
                        </div>
                        {
                            this.props.posts.map((post, index) => {
                                return <Post postId={post.postId}
                                             onDeletePost={this.onDeletePost}
                                             publishCommentAsAdminOptionVisible={false}
                                             onCommentCreated={this.props.onCommentCreated}
                                             avatarPath={Utils.getImageSrc(post.avatarPath)}
                                             text={post.textContent}
                                             authorLink={this.buildPostProfileLink(post.author.userId)}
                                             createdAt={post.createdAt}
                                             viewName={post.author.name + " " + post.author.surname}
                                             deleteBtnVisible={this.props.isViewerProfile}
                                             comments={post.recentComments}
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
