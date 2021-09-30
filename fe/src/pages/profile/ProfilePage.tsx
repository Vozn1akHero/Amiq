import React, {Component} from 'react';
import BasePageComponent from "core/BasePageComponent";
import {ItemsFrameL} from "common/components/ItemsFrameL/ItemsFrameL";
import Post from "features/post/Post";
import PostCreationForm from "features/post/PostCreationForm";
import PageAvatar from "common/components/PageAvatar/PageAvatar";
import {IUserPost} from "features/post/models/user-post";
import {IUser} from "../../features/user/models/user";

type Props = {
    posts: Array<IUserPost>;
    postsLoaded: boolean;
    userData: IUser;
    userDataLoaded: boolean;
    createPost(text: string);
}

type State = {

}

class ProfilePage extends Component<Props, State> {
    friends = []

    render() {
        return (
            <div className='profile-page'>
                <div className="uk-padding uk-grid uk-child-width-1-2">
                {this.props.userDataLoaded &&
                        <>
                            <div className="uk-grid-item-match uk-first-column uk-width-1-3">
                                <PageAvatar viewTitle={this.props.userData.name + " " + this.props.userData.surname}
                                            />
                            </div>
                            <div className="uk-preserve-width uk-margin-left">
                                <h3>O mnie</h3>
                                <p>
                                    {this.props.userData.shortDescription}
                                </p>
                                {this.props.userData.userDescriptionBlocks && <ul uk-accordion="collapsible: false">
                                    {
                                        this.props.userData.userDescriptionBlocks.map(((value, index) => {
                                            return <li>
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
                        <PostCreationForm />
                        {
                            this.props.posts.map((post, index) => {
                                return <Post key={index} />
                            })
                        }
                    </div>
                </div>
            </div>
        );
    }
}

export default ProfilePage;
