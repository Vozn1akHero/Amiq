import React, {Component} from 'react';
import PostCreationForm from "features/post/PostCreationForm";
import Post from "features/post/Post";
import {IGroupData, IGroupParticipant} from "../../features/group/models/group-models";
import PageAvatar from "../../common/components/PageAvatar/PageAvatar";
import {IGroupPost} from "../../features/post/models/group-post";
import {Utils} from "../../core/utils";
import {IGroupPostCommentCreation} from "../../features/post/models/post-comment";
import {AuthStore} from "../../store/custom/auth/auth-store";
import DescriptionBlocks from "../../common/components/DescriptionBlock/DescriptionBlocks";
import {Link, matchPath, Route, Switch, withRouter} from 'react-router-dom';
import GroupParticipantsSubpage from "./subpages/group-participants-subpage/GroupParticipantsSubpage";
import GroupEventsSubpage from "./subpages/group-events-subpage/GroupEventsSubpage";
import GroupEventSubpage from "./subpages/group-event/GroupEventSubpage";
import GroupParticipantsInFrame
    from "../../features/group/components/GroupParticipantsInFrame/GroupParticipantsInFrame";
import {EnPostType} from "../../features/post/en-post-type";
import {IPaginatedStoreData} from "../../store/redux/base/paginated-store-data";
import {IGroupEvent} from "../../features/group/models/group-event";
import {IIdBasedPersistentData} from "../../store/redux/base/id-based-persistent-data";
import GroupEventsInFrame from "../../features/group/components/GroupEventsInFrame/GroupEventsInFrame";
import "./group-page.scss"

type Props = {
    groupData: IGroupData;
    groupDataLoaded: boolean;
    groupPosts: Array<IGroupPost>;
    groupPostsLoaded: boolean;
    basicAdminPermissionsAvailable: boolean;
    groupEvents: IIdBasedPersistentData<IPaginatedStoreData<IGroupEvent>>;
    groupParticipants: IPaginatedStoreData<IGroupParticipant>;
    onCommentCreated(data: IGroupPostCommentCreation);
    onPostCreated(data: Partial<IGroupPost>);
    onDeletePost(postId: string);
    onRemoveComment(postCommentId: string);
    match: any;
    location: any;
    history: any;
    //groupViewerRole: EnGroupViewerRole;
}

type State = {
    showSubpageSwitch: boolean;
}

class GroupPage extends Component<Props, State> {
    readonly participantsSubroute = "/group/:groupId/participants";
    readonly eventsSubroute = "/group/:groupId/events";
    readonly groupEventSubroute = "/group/:groupId/event/:groupEventId";
    readonly subrotes: string[] = [this.participantsSubroute, this.eventsSubroute, this.groupEventSubroute];

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

    shouldSubpageBeVisible = () => {
        const {pathname} = this.props.location;
        const match = matchPath(pathname, {
            path: this.subrotes,
            exact: true,
        });
        return match != null && match !== undefined
    }

    setSubroute = () => {
        const {pathname} = this.props.location;
        const match = matchPath(pathname, {
            path: this.subrotes,
            exact: true,
        });
        if (match) {
            const subroute = this.subrotes.filter(e => e === match.path);

        }
        console.log(match)
    }

    onPostCreated = (text: string) => {
        const {groupData} = this.props;
        const post: Partial<IGroupPost> = {
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

    getSettingsLink = (part?: string): string => {
        const link: string = `/group-settings/${this.props.groupData.groupId}${part ? "#" + part : ""}`;
        return link;
    }

    render() {
        return (
            <div className="group-page uk-flex-center uk-flex">
                <div className="group-page__first-column">
                    {this.props.groupDataLoaded && <PageAvatar avatarSrc={this.props.groupData.avatarSrc}
                                                               viewTitle={this.props.groupData.name}/>}

                    <div className="uk-margin-medium-top">
                        <div className="uk-card uk-card-default uk-card-body uk-background-default">
                            <span className="uk-card-title">Zarządzanie grupą</span>
                            {
                                this.props.basicAdminPermissionsAvailable && <div className="uk-margin-small-top">
                                    {
                                        this.props.groupDataLoaded && <div className="uk-flex uk-flex-column">
                                            <Link className="uk-margin-small-top" to={this.getSettingsLink("basic")}>
                                                <span className="uk-margin-small-right"
                                                      uk-icon="icon:nut"></span> Podstawowe dane
                                            </Link>
                                            <Link className="uk-margin-small-top"
                                                  to={this.getSettingsLink("participants")}>
                                                <span className="uk-margin-small-right"
                                                      uk-icon="icon:users"></span> Uczestnicy
                                            </Link>
                                            <Link className="uk-margin-small-top" to={this.getSettingsLink("events")}>
                                                <span className="uk-margin-small-right"
                                                      uk-icon="icon:calendar"></span> Wydarzenia
                                            </Link>
                                        </div>
                                    }
                                </div>
                            }
                        </div>

                        {
                            this.props.groupParticipants.loaded && <div className="uk-margin-medium-top">
                                <GroupParticipantsInFrame groupParticipants={this.props.groupParticipants}/>
                            </div>
                        }

                        <div className="uk-margin-medium-top">
                            <GroupEventsInFrame groupId={this.props.match.params.groupId}
                                                groupEvents={this.props.groupEvents}/>
                        </div>
                    </div>
                </div>

                <div className="group-page__second-column uk-margin-medium-left">
                    {
                        !this.state.showSubpageSwitch && this.props.groupDataLoaded && <>
                            <div className="uk-preserve-width">
                                <h3>O nas</h3>
                                <p>
                                    {this.props.groupData.description}
                                </p>
                                {
                                    <DescriptionBlocks descriptionBlocks={this.props.groupData.descriptionBlocks}/>
                                }
                            </div>

                            <div className="uk-margin-large-top">
                                <div className="uk-margin-medium-bottom">
                                    <PostCreationForm handleSubmit={this.onPostCreated}
                                                      publishAsAdminOptionVisible={this.props.basicAdminPermissionsAvailable}/>
                                </div>
                                {
                                    this.props.groupPostsLoaded && this.props.groupPosts.map((post, index) => {
                                        return <Post postId={post.postId}
                                                     postType={EnPostType.Group}
                                                     onDeletePost={this.onDeletePost}
                                                     onRemoveComment={this.props.onRemoveComment}
                                                     onCommentCreated={this.props.onCommentCreated}
                                                     hasMoreCommentsThanPassed={post.hasMoreCommentsThanRecent}
                                                     comments={post.comments}
                                                     avatarPath={Utils.getImageSrc(post.avatarPath)}
                                                     text={post.textContent}
                                                     authorLink={"/group/" + post.groupId}
                                                     createdAt={post.createdAt}
                                                     viewName={post.groupName}
                                                     publishCommentAsAdminOptionVisible={this.props.basicAdminPermissionsAvailable}
                                                     deleteBtnVisible={this.props.basicAdminPermissionsAvailable}
                                                     key={index}/>
                                    })
                                }
                            </div>
                        </>
                    }

                    {
                        this.state.showSubpageSwitch && <Switch>
                            <Route path={this.participantsSubroute} component={GroupParticipantsSubpage}/>
                            <Route path={this.eventsSubroute} exact={true} component={GroupEventsSubpage}/>
                            <Route path={this.groupEventSubroute} exact={true} component={GroupEventSubpage}/>
                        </Switch>
                    }
                </div>
            </div>
        );
    }
}

export default withRouter(GroupPage);
