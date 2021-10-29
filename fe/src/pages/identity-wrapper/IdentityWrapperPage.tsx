import React, {createRef, useCallback, useState} from 'react';
import {Redirect, Route, Switch} from "react-router-dom";
import {Routes} from "core/routing";
import GroupsPageContainer from "pages/groups/GroupsPageContainer";
import GroupPageContainer from "pages/group/GroupPageContainer";
import ChatPageContainer from "pages/chat/ChatPageContainer";
import AuthPageContainer from "pages/auth/AuthPageContainer";
import ProfilePageContainer from "pages/profile/ProfilePageContainer";
import NavigationWithRouter from "../../layout/navigation/Navigation";
import {Observable} from "rxjs";
import JoinupPageContainer from "../joinup/JoinupPageContainer";
import Footer from "layout/footer/Footer";
import "./identity-wrapper-page.scss"
import GuardedRoute from "../../core/routing/GuardedRoute";
import NotFoundPage from "../not-found/NotFoundPage";
import GroupSettingsPageContainer from "../group-settings/GroupSettingsPageContainer";
import FriendListPage from "../friends/FriendListPage";

type Props = {
    isAuthenticated$: Observable<boolean>;
}

const IdentityWrapperPage = (props: Props) => {
    const navigationRef = createRef<HTMLElement>();
    const footerAvailableHeight : number = 200;
    const [footerShouldHaveAbsolutePosition, setFooterShouldHaveAbsolutePosition] = useState(false);

    let mainContentRef = useCallback(node=> {
        /*if(node !== null && navigationRef !== null)
        {
            let navAndMainContentHeight = node.clientHeight + navigationRef.current.clientHeight;
            let nextFooterAvailableHeight = window.innerHeight - navAndMainContentHeight;
            if(nextFooterAvailableHeight >= footerAvailableHeight){
                setFooterShouldHaveAbsolutePosition(true);
            }
        }*/
    }, []);

    return (
        <div className="identity-wrapper">
            <Switch>
                <Redirect from="/" to={Routes.getSimpleLink(Routes.profilePageRoutes)} exact={true} />
                <Route path="/" render={() => <>
                    <NavigationWithRouter navRef={navigationRef} />

                    <div className="main-content" ref={mainContentRef}>
                        <div className="uk-margin-medium-top">
                            {/*<Breadcrumb />*/}
                            <div className="page-wrapper uk-margin-medium-top">
                                <Route component={AuthPageContainer} path={Routes.getSimpleLink(Routes.authPageRoutes)} />
                                <Route component={JoinupPageContainer} path={Routes.getSimpleLink(Routes.registrationPageRoutes)} />
                                <GuardedRoute component={ProfilePageContainer} path={Routes.getSimpleLink(Routes.profilePageRoutes)} />
                                <Route component={FriendListPage} path={Routes.getSimpleLink(Routes.friendListPageRoutes)} />
                                <Route component={GroupsPageContainer} path={Routes.getSimpleLink(Routes.groupsPageRoutes)} />
                                <Route exact={true} component={GroupSettingsPageContainer} path={Routes.getSimpleLink(Routes.groupSettingsPageRoutes)} />
                                <Route component={GroupPageContainer} path={Routes.getSimpleLink(Routes.groupPageRoutes)} />
                                <Route component={ChatPageContainer} path={Routes.getSimpleLink(Routes.chatPageRoutes)} />
                                <Route path="/not-found" component={NotFoundPage} />
                            </div>
                        </div>
                    </div>

                    <div className="uk-margin-medium-top">
                        <Footer footerHeight={footerAvailableHeight}
                                footerShouldHaveAbsolutePosition={footerShouldHaveAbsolutePosition} />
                    </div>
                </>} />
                {/*<Route path="*" component={NotFoundPage} />*/}
            </Switch>
        </div>
    );
}

export default IdentityWrapperPage;