import './App.scss';
import {MemoizedNavigation} from "layout/navigation/Navigation";
import {BrowserRouter, Redirect, Route, Switch} from "react-router-dom";
import ProfilePage from "./pages/profile/ProfilePage";
import Breadcrumb from "./common/components/Breadcrumb/Breadcrumb";
import {GuardedRoute, Routes} from "./core/routing"
import FriendsPageContainer from "./pages/friends/FriendsPageContainer";
import GroupsPageContainer from "./pages/groups/GroupsPageContainer";
import GroupPageContainer from 'pages/group/GroupPageContainer';
import ChatPageContainer from "./pages/chat/ChatPageContainer";
import IdentityWrapperPageContainer from "./pages/identity-wrapper/IdentityWrapperPageContainer";
import React, {Component} from 'react';

class App extends Component {
    render() {
        return (
            <BrowserRouter>
                <div className="App">
                    <IdentityWrapperPageContainer />
                </div>
            </BrowserRouter>
        );
    }
}

export default App;
