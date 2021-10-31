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
    removeFriend(friendId: number): void;
    acceptFriendRequest(destUserId: number): void;
    sendFriendRequest(destUserId: number): void;
    rejectFriendRequest(destUserId: number): void;
    cancelFriendRequest(destUserId: number): void;
    blockUser(destUserId: number): void;
    createPost(text: string);
    deletePost(postId: string);
    commentCreated(data: IPostCommentCreation);
    removeComment(postCommentId: string);
    getMorePosts(): void;
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

    renderAvatarControls = () => {
        const {
            userId,
            blockedByIssuer,
            issuerBlocked,
            issuerReceivedFriendRequest,
            issuerSentFriendRequest,
            isIssuerFriend
        } = this.props.userData;

        if (!this.props.isViewerProfile) {
            if (!issuerBlocked && !blockedByIssuer && !issuerReceivedFriendRequest && !issuerSentFriendRequest && !isIssuerFriend) {
                return <Fragment>
                    <button onClick={() => this.props.sendFriendRequest(userId)}
                            className="uk-button uk-button-primary page-avatar__control">Dodaj
                    </button>
                    <a onClick={(e) => {
                        e.preventDefault();
                        this.props.blockUser(userId);
                    }} className="uk-button uk-button-secondary uk-margin-small-left uk-text-center" uk-icon="lock">

                    </a>
                </Fragment>
            } else if (issuerReceivedFriendRequest) {
                return <>
                    <button onClick={() => this.props.acceptFriendRequest(userId)}
                            className="uk-button uk-button-primary">Zatwierdź
                    </button>
                    <button onClick={() => this.props.acceptFriendRequest(userId)}
                            className="uk-button uk-button-danger uk-margin-small-left">Odrzuć
                    </button>
                </>
            } else if (issuerSentFriendRequest) {
                return <button onClick={() => this.props.rejectFriendRequest(userId)}
                               className="uk-button uk-button-secondary">Anuluj</button>
            } else if (isIssuerFriend) {
                return <a onClick={(e) => {
                    e.preventDefault();
                    this.props.removeFriend(userId);
                }} className="uk-button uk-button-secondary" uk-icon="trash"/>
            }
        }
    }

    shouldSubpageBeVisible = () => {
        const {pathname} = this.props.location;
        const match = matchPath(pathname, {
            path: this.subroutes,
            exact: true,
        });
        return match != null && match !== undefined
    }

    refresh = () => {
        console.log("refresh")
    }

    render() {
        return (

            <div className='profile-page uk-flex-center uk-grid uk-child-width-1-2'>
                <ChangeAvatarPopup/>

                {this.props.userDataLoaded &&
                <>
                    <div className="uk-first-column uk-width-1-3">
                        <PageAvatar avatarSrc={this.props.userData.avatarPath}
                                    isChangeAvatarBtnVisible={this.props.isViewerProfile}
                                    userSpecifics={this.props.userData}
                                    viewTitle={this.props.userData.name + " " + this.props.userData.surname}>
                            {this.renderAvatarControls()}
                        </PageAvatar>

                        {
                            this.props.userDataLoaded && !this.props.userData.issuerBlocked &&
                            <div className="uk-margin-medium-top">
                                <ExemplaryUserFriendsInFrame userId={this.props.profileId}
                                                             userFriendsLoaded={this.props.userFriendsLoaded}
                                                             userFriends={this.props.userFriends.slice(0, 6)}/>

                                <div className="uk-margin-medium-top">
                                    <ItemsFrameL title="Linki"
                                                 icon="world"
                                                 items={[]}
                                                 callbackText="Brak linków"/>
                                </div>
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
                                                refreshFunction={this.refresh}
                                                pullDownToRefresh
                                                pullDownToRefreshThreshold={50}
                                            >
                                                {
                                                    this.props.posts.map((post, index) => {
                                                        return <Post postId={post.postId}
                                                                     postType={EnPostType.User}
                                                                     onRemoveComment={this.props.removeComment}
                                                                     onDeletePost={this.onDeletePost}
                                                                     publishCommentAsAdminOptionVisible={false}
                                                                     onCommentCreated={this.props.commentCreated}
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
