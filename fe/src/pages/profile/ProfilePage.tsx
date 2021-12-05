import React, {Component, Fragment} from 'react';
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
import ExemplaryUserFriendsInFrame
    from "../../features/friend/components/ExemplaryUserFriendsInFrame/ExemplaryUserFriendsInFrame";
import ChangeAvatarPopup from "../../features/user/components/ChangeAvatarPopup/ChangeAvatarPopup";
import {matchPath, Route, Switch, withRouter} from "react-router-dom";
import UserFriendsSubpage from "./subpages/UserFriendsSubpage";
import InfiniteScroll from 'react-infinite-scroll-component';
import UserAvatar from "../../features/user/components/UserAvatar/UserAvatar";
import {ModalService} from "../../core/modal-service";

type Props = {
    posts: Array<IUserPost>;
    postsLength: number;
    postsLoaded: boolean;
    userData: IUser;
    userDataLoaded: boolean;
    isViewerProfile: boolean;
    userFriends: Array<IFriendship>;
    userFriendsLoaded: boolean;
    profileId: number;
    postsPerPage: number;
    getComments(postId: string, page: number);
    removeFriend(friendId: number): void;
    acceptFriendRequest(destUserId: number): void;
    sendFriendRequest(destUserId: number): void;
    rejectFriendRequest(destUserId: number): void;
    cancelFriendRequest(destUserId: number): void;
    blockUser(destUserId: number): void;
    createPost(value: {text: string});
    deletePost(postId: string);
    commentCreated(data: IPostCommentCreation);
    removeComment(postCommentId: string);
    getMorePosts(): void;
    onAvatarChangeSubmit(file: File):void;
    match: any;
    location: any;
    history: any;
}

type State = {
    showSubpageSwitch: boolean;
}

class ProfilePage extends Component<Props, State> {
    readonly friendsSubroute = "/profile/:userId/friends";
    readonly subroutes: string[] = [this.friendsSubroute]

    constructor(props) {
        super(props);

        this.state = {
            showSubpageSwitch: this.shouldSubpageBeVisible()
        }
    }

    componentDidUpdate(prevProps: Readonly<Props>, prevState: Readonly<State>, snapshot?: any) {
        if (this.props.location.pathname !== prevProps.location.pathname) {
            this.setState({
                showSubpageSwitch: this.shouldSubpageBeVisible()
            })
        }
    }

    buildPostProfileLink = (userId: number) => {
        return this.props.isViewerProfile ? "/profile/" : `/profile/${userId}`
    }

    onDeletePost = (postId: string) => {
        this.props.deletePost(postId);
    }

    shouldSubpageBeVisible = () => {
        const {pathname} = this.props.location;
        const match = matchPath(pathname, {
            path: this.subroutes,
            exact: true,
        });
        return match != null && match !== undefined
    }

    openAvatarPopup = () => {
        ModalService.open(<ChangeAvatarPopup avatarSrc={this.props.userData.avatarPath}
                                             pageTitle={this.props.userData.name + " " + this.props.userData.surname}
                                             onSubmit={(file:File) => {
                                                 this.props.onAvatarChangeSubmit(file);
                                                 ModalService.close();
                                             }}
        />);
    }

    render() {
        return (
            <div className='profile-page uk-flex-center uk-grid uk-child-width-1-2'>
                {this.props.userDataLoaded &&
                <>
                    <div className="uk-first-column uk-width-1-3">
                        <UserAvatar userData={this.props.userData}
                                    changeAvatarPopup={this.openAvatarPopup}
                                    isViewerProfile={this.props.isViewerProfile}
                                    removeFriend={this.props.removeFriend}
                                    acceptFriendRequest={this.props.acceptFriendRequest}
                                    sendFriendRequest={this.props.sendFriendRequest}
                                    rejectFriendRequest={this.props.rejectFriendRequest}
                                    cancelFriendRequest={this.props.cancelFriendRequest}
                                    blockUser={this.props.blockUser}
                        />

                        {
                            this.props.userDataLoaded && !this.props.userData.issuerBlocked &&
                            <div className="uk-margin-medium-top">
                                <ExemplaryUserFriendsInFrame userId={this.props.profileId}
                                                             userFriendsLoaded={this.props.userFriendsLoaded}
                                                             userFriends={this.props.userFriends.slice(0, 6)}/>

                                {/*<div className="uk-margin-medium-top">
                                    <ItemsFrameL title="Linki"
                                                 icon="world"
                                                 items={[]}/>
                                </div>*/}
                            </div>
                        }
                    </div>

                    {
                        !this.state.showSubpageSwitch && <div className="uk-margin-left">
                            {
                                this.props.userDataLoaded && this.props.userData.issuerBlocked && <>
                                    <p className="uk-text-danger">Nie masz dostępu do tego profilu</p>
                                </>
                            }
                            {
                                this.props.userDataLoaded && !this.props.userData.issuerBlocked &&
                                <>
                                    <h3>O mnie</h3>
                                    <p>
                                        {this.props.userData.shortDescription}
                                    </p>
                                    {this.props.userData.userDescriptionBlocks &&
                                    <ul uk-accordion="collapsible: false">
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
                                </>
                            }
                            {
                                this.props.userDataLoaded && !this.props.userData.issuerBlocked &&
                                <div className="uk-margin-large-top">
                                    {
                                        this.props.isViewerProfile && <div className="uk-margin-medium-bottom">
                                            <PostCreationForm handleSubmit={this.props.createPost}
                                                              publishAsAdminOptionVisible={false}/>
                                        </div>
                                    }
                                    {
                                        this.props.postsLoaded &&
                                        <InfiniteScroll
                                            dataLength={this.props.postsLength}
                                            next={this.props.getMorePosts}
                                            hasMore={this.props.postsLength >= this.props.posts.length}
                                            loader={<Fragment></Fragment>}
                                            endMessage={
                                                <p style={{textAlign: 'center'}}>
                                                    <b>Yay! You have seen it all</b>
                                                </p>
                                            }
                                        >
                                            {
                                                this.props.posts.map((post, index) => {
                                                    return <Post postId={post.postId}
                                                                 commentsCount={post.commentsCount}
                                                                 postType={EnPostType.User}
                                                                 onRemoveComment={this.props.removeComment}
                                                                 onDeletePost={this.onDeletePost}
                                                                 publishCommentAsAdminOptionVisible={false}
                                                                 onCommentCreated={this.props.commentCreated}
                                                                 getComments={this.props.getComments}
                                                                 avatarPath={Utils.getImageSrc(post.avatarPath)}
                                                                 text={post.textContent}
                                                                 authorLink={this.buildPostProfileLink(post.author.userId)}
                                                                 createdAt={post.createdAt}
                                                                 viewName={post.author.name + " " + post.author.surname}
                                                                 deleteBtnVisible={this.props.isViewerProfile}
                                                                 comments={post.comments}
                                                                 hasMoreCommentsThanPassed={post.hasMoreCommentsThanRecent}
                                                                 key={index}/>
                                                })
                                            }
                                        </InfiniteScroll>
                                    }
                                </div>
                            }
                        </div>
                    }
                </>
                }

                {
                    this.state.showSubpageSwitch && <div className="uk-margin-left">
                        <Switch>
                            <Route path={this.friendsSubroute} component={UserFriendsSubpage}/>
                        </Switch>
                    </div>
                }

            </div>
        );
    }
}

export default withRouter(ProfilePage);
