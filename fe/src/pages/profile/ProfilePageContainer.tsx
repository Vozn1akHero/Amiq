import React, {Component} from 'react';
import ProfilePage from "./ProfilePage";
import {IUserPost} from "../../features/post/models/user-post";
import {UserPostService} from "../../features/post/user-post-service";
import {AuthStore} from "../../store/auth/auth-store";
import {withRouter} from "react-router-dom";

type State = {
    posts: Array<IUserPost>;
    profileId?: number;
    isViewerProfile: boolean;
}

class ProfilePageContainer extends Component<any, State> {
    userService = new UserPostService();
    isViewerProfile: boolean;

    constructor(props) {
        super(props);

        this.initProfileId();

        this.userService.getUserPosts(AuthStore.identity.userId, 1).then(value => {
            this.state = {
                ...this.state,
                posts: value.data as Array<IUserPost>
            }
        });
    }

    initProfileId(){
        const profileId = this.props.match.params.profileId;
        if(profileId !== null){
            this.setState({
                profileId,
                isViewerProfile: AuthStore.identity.userId === profileId
            })
        } else {
            this.setState({
                isViewerProfile: true
            })
        }
    }

    render() {
        return (
            <ProfilePage posts={this.state.posts} />
        );
    }
}

export default withRouter(ProfilePageContainer);