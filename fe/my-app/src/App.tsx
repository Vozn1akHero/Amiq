import React from 'react';
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

function App() {
  return (
    <div className="App">
      <BrowserRouter>
          <MemoizedNavigation />

          <main className="main-content uk-margin-medium-top">
              <Breadcrumb />
              <Switch>
                  <Redirect from="/" to={Routes.getRouteAsString(Routes.profilePageRoutes)} exact={true} />
                  <Route component={ProfilePage} path={Routes.getRouteAsString(Routes.profilePageRoutes)} />
                  <Route component={FriendsPageContainer} path={Routes.getRouteAsString(Routes.friendListPageRoutes)} />
                  <Route component={GroupsPageContainer} path={Routes.getRouteAsString(Routes.groupsPageRoutes)} />
                  <Route component={GroupPageContainer} path={Routes.getRouteAsString(Routes.groupPageRoutes)} />
                  {/*<GuardedRoute component={ChatPageContainer} path={Routes.getRouteAsString(Routes.chatPageRoutes)} authenticated={true} />*/}
                  <Route component={ChatPageContainer} path={Routes.getRouteAsString(Routes.chatPageRoutes)} />
              </Switch>
          </main>
      </BrowserRouter>
    </div>
  );
}

export default App;
