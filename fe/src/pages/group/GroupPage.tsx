import React, {Component} from 'react';
import {ItemsFrameL} from "common/components/ItemsFrameL/ItemsFrameL";
import PostCreationForm from "features/post/PostCreationForm";
import Post from "features/post/Post";
import {IGroupData} from "../../features/group/group-models";
import PageAvatar from "../../common/components/PageAvatar/PageAvatar";
import {IGroupPost} from "../../features/post/models/group-post";

type Props = {
    participants: Array<any>;
    groupData: IGroupData;
    groupDataLoaded: boolean;
    groupPosts: Array<IGroupPost>;
}

class GroupPage extends Component<Props, any>  {
    render() {
        return (
            <div className='profile-page'>
                <div className="uk-padding uk-grid uk-child-width-1-2">
                    <div className="uk-grid-item-match uk-first-column uk-width-1-3">
                        { this.props.groupDataLoaded && <PageAvatar viewTitle={this.props.groupData.name}/> }
                    </div>
                    <div className="uk-preserve-width uk-margin-left">
                        <h3>O nas</h3>
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
                        <ItemsFrameL title="Uczestnicy" items={this.props.participants} callbackText="Nothing to show" />
                    </div>
                    <div className="uk-margin-left uk-margin-large-top">
                        <PostCreationForm />
                        {
                            this.props.groupPosts != null && this.props.groupPosts.map((value, index) => {
                                return <Post key={index} />
                            })
                        }
                    </div>
                </div>
            </div>
        );
    }
}

export default GroupPage;
