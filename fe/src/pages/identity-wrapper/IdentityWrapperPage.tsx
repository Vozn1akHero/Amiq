import React, {
    Component,
    ComponentClass,
    createRef, LegacyRef,
    ReactComponentElement,
    RefObject, useCallback,
    useEffect,
    useState
} from 'react';
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
import JoinupPage from "../joinup/JoinupPage";
import JoinupPageContainer from "../joinup/JoinupPageContainer";
import Footer from "layout/footer/Footer";

type Props = {
    isAuthenticated$: Observable<boolean>;
}

const IdentityWrapperPage = (props: Props) => {
    const navigationRef = createRef<HTMLElement>();
    const footerAvailableHeight : number = 200;
    const [footerShouldHaveAbsolutePosition, setFooterShouldHaveAbsolutePosition] = useState(false);

    let mainContentRef = useCallback(node=> {
        if(node !== null && navigationRef !== null)
        {
            let navAndMainContentHeight = node.clientHeight + navigationRef.current.clientHeight;
            let nextFooterAvailableHeight = window.innerHeight - navAndMainContentHeight;
            if(nextFooterAvailableHeight >= footerAvailableHeight){
                setFooterShouldHaveAbsolutePosition(true);
            }
        }
    }, []);

    return (
            <Switch>
                <Redirect from="/" to={Routes.getRouteAsString(Routes.profilePageRoutes)} exact={true} />
                <Route path="/" render={() => <>
                    <Navigation navRef={navigationRef} />
                    <div className="main-content-wrap-1  uk-margin-medium-top">
                        <main className="main-content" ref={mainContentRef} >
                            <Breadcrumb />
                            <Route component={AuthPageContainer} path={Routes.getRouteAsString(Routes.authPageRoutes)} />
                            <Route component={JoinupPageContainer} path={Routes.getRouteAsString(Routes.registrationPageRoutes)} />
                            <Route component={ProfilePageContainer} path={Routes.getRouteAsString(Routes.profilePageRoutes)} />
                            <Route component={FriendsPageContainer} path={Routes.getRouteAsString(Routes.friendListPageRoutes)} />
                            <Route component={GroupsPageContainer} path={Routes.getRouteAsString(Routes.groupsPageRoutes)} />
                            <Route component={GroupPageContainer} path={Routes.getRouteAsString(Routes.groupPageRoutes)} />
                            <Route component={ChatPageContainer} path={Routes.getRouteAsString(Routes.chatPageRoutes)} />
                        </main>
                        <Footer footerHeight={footerAvailableHeight}
                                footerShouldHaveAbsolutePosition={footerShouldHaveAbsolutePosition} />
                    </div>
                </>} />
                {/*<Route path="*" component={NotFoundPage} />*/}
            </Switch>
    );
}

export default IdentityWrapperPage;