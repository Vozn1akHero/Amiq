import React from 'react';
import './App.scss';
import {MemoizedNavigation} from "layout/navigation/Navigation";
import {BrowserRouter, Redirect, Route, Switch} from "react-router-dom";
import ProfilePage from "./pages/profile/ProfilePage";
import Breadcrumb from "./common/components/Breadcrumb/Breadcrumb";
import {Routes} from "./core/routing"
import FriendListPage from "./pages/friends/FriendListPage";
import FriendsPageContainer from "./pages/friends/FriendsPageContainer";

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
              </Switch>
          </main>
      </BrowserRouter>
    </div>
  );
}

export default App;
