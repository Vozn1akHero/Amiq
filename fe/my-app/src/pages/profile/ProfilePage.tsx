import React from 'react';
import BasePageComponent from "core/BasePageComponent";
import {ItemsFrameL} from "../../common/components/ItemsFrameL";
import Post from "../../features/post/Post";
import PostCreationForm from "../../features/post/PostCreationForm";


class ProfilePage extends BasePageComponent {
    friends = []

    render() {
        return (
            <div className='profile-page'>
                <div className="uk-padding uk-grid uk-child-width-1-2" uk-grid>
                    <div className="uk-grid-item-match uk-first-column uk-width-1-3">
                        <div className="uk-card uk-card-default uk-card-body">
                            <h3 className="uk-card-title">Dima Vozniachuk</h3>
                            <img style={{borderRadius: '10%'}}
                                    src="https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"
                                 sizes="(min-width: 150px) 150px, 100vw" width="150" height="150" alt="" uk-img />
                        </div>
                    </div>
                    <div className="uk-preserve-width uk-margin-left">
                        <h3>O mnie</h3>
                        <p>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt
                            ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco
                            laboris nisi ut aliquip ex ea commodo consequat.
                        </p>
                        <ul uk-accordion="collapsible: false">
                            <li>
                                <a className="uk-accordion-title" href="#">Item 1</a>
                                <div className="uk-accordion-content">
                                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor
                                        incididunt ut labore et dolore magna aliqua.</p>
                                </div>
                            </li>
                            <li>
                                <a className="uk-accordion-title" href="#">Item 2</a>
                                <div className="uk-accordion-content">
                                    <p>Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut
                                        aliquip ex ea commodo consequat. Duis aute irure dolor reprehenderit.</p>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div className="uk-first-column uk-margin-medium-top uk-width-1-3">
                        <ItemsFrameL title="Znajomi" items={this.friends} />
                    </div>
                    <div className="uk-margin-left uk-margin-large-top">
                        <PostCreationForm />
                        <Post />
                    </div>
                </div>
            </div>
        );
    }
}

export default ProfilePage;
