import React, {Component, useEffect, useState} from 'react';
import {BrowserRouter, Redirect, Route, Switch, useLocation} from "react-router-dom";
import ProfilePage from "pages/profile/ProfilePage";
import {Routes} from "core/routing";
import FriendsPageContainer from "pages/friends/FriendsPageContainer";
import GroupsPageContainer from "pages/groups/GroupsPageContainer";
import GroupPageContainer from "pages/group/GroupPageContainer";
import ChatPageContainer from "pages/chat/ChatPageContainer";
import AuthPageContainer from "pages/auth/AuthPageContainer";
import ProfilePageContainer from "pages/profile/ProfilePageContainer";
import {MemoizedNavigation, Navigation} from "../../layout/navigation/Navigation";
import Breadcrumb from "../../common/components/Breadcrumb/Breadcrumb";
import {Observable} from "rxjs";
import NotFoundPage from "pages/not-found/NotFoundPage";
import {AuthStore} from "../../store/auth/auth-store";

type Props = {
    isAuthenticated$: Observable<boolean>;
}

const IdentityWrapperPage = (props: Props) => {
    return (
            <Switch>
                {/*<Redirect from="/" to={Routes.getRouteAsString(Routes.profilePageRoutes)} exact={true} />*/}
                <Route path="/" render={() => <>
                    <Navigation />
                    <main className="main-content uk-margin-medium-top">
                        <Breadcrumb />
                        <Route component={AuthPageContainer} path={Routes.getRouteAsString(Routes.authPageRoutes)} />
                        <Route component={ProfilePageContainer} path={Routes.getRouteAsString(Routes.profilePageRoutes)} />
                        <Route component={FriendsPageContainer} path={Routes.getRouteAsString(Routes.friendListPageRoutes)} />
                        <Route component={GroupsPageContainer} path={Routes.getRouteAsString(Routes.groupsPageRoutes)} />
                        <Route component={GroupPageContainer} path={Routes.getRouteAsString(Routes.groupPageRoutes)} />
                        <Route component={ChatPageContainer} path={Routes.getRouteAsString(Routes.chatPageRoutes)} />
                    </main>
                </>} />
                {/*<Route path="*" component={NotFoundPage} />*/}
            </Switch>
    );
}

export default IdentityWrapperPage;