import {Link, withRouter} from "react-router-dom";
import {INavigationLink} from "./INavigationLink";
import React, {Component, memo, RefObject, useState} from "react";
import "./navigation.scss"
import {Routes} from "core/routing";
import {Observable, take} from "rxjs";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {NotificationList} from "../../features/notification/components/NotificationList";

type State = {
    isAuthenticated: boolean;
    isNotificationListVisible: boolean;
}

type Props = {
    navRef: RefObject<HTMLElement>;
    match: any;
    location: any;
    history: any;
}

class Navigation extends Component<Props, State> {
    constructor(props) {
        super(props);
        this.state = {
            isAuthenticated: true,
            isNotificationListVisible: false
        }
    }

    loggedInUserNavigationLinks: Array<INavigationLink> = [
        {
            title: "Profil",
            anchor: Routes.getSimpleLink(Routes.myProfilePageRoutes),
            uiKitIcon: "user"
        },
        {
            title: "Znajomi",
            anchor: Routes.getSimpleLink(Routes.friendListPageRoutes),
            uiKitIcon: "users",

        },
        {
            title: "Czat",
            anchor: Routes.getSimpleLink(Routes.chatPageRoutes),
            uiKitIcon: "mail"
        },
        {
            title: "Grupy",
            anchor: Routes.getSimpleLink(Routes.groupsPageRoutes),
            uiKitIcon: "social"
        }
    ];

    loggedInUserNavigationRightSideLinks: Array<INavigationLink> = [
        /*{
            title: "Wyloguj",
            anchor: Routes.getLink(Routes.logoutPageRoutes)
        }*/
    ]

    notLoggedInUserLinks: Array<INavigationLink> = [
        {
            title: "Zaloguj",
            anchor: Routes.getSimpleLink(Routes.authPageRoutes)
        },
        {
            title: "Dołącz",
            anchor: Routes.getSimpleLink(Routes.registrationPageRoutes)
        }
    ]

    componentDidMount() {
        AuthStore.isAuthenticated$.subscribe(value => {
            this.setState({
                isAuthenticated: value
            })
        });
    }

    onBellClick = e => {
        e.preventDefault();
        this.setState({
            isNotificationListVisible: !this.state.isNotificationListVisible
        })
    }

    onLogoutClick = e => {
        e.preventDefault();
        AuthStore.logout().then(() => {
            this.props.history.push("/login");
        });
    }

    render() {
        return (
            <nav className="navigation uk-padding-small" ref={this.props.navRef}>
                <div className="logo uk-margin-large-left "></div>

                {
                    this.state.isAuthenticated ? <>
                            <div className="uk-margin-large-left uk-navbar-left">
                                <ul className="uk-navbar-nav">
                                    {
                                        this.loggedInUserNavigationLinks.map(((value, i) =>
                                            <li key={i}>
                                                <Link uk-tooltip={value.title} to={value.anchor} style={{textTransform: "initial"}}>
                                                    <span uk-icon={`icon: ${value.uiKitIcon}`}></span>
                                                   {/* {value.title}*/}
                                                </Link>
                                            </li>))
                                    }
                                </ul>
                            </div>
                            <div className="uk-margin-medium-right uk-navbar-right">
                                <ul className="uk-navbar-nav">
                                    <li>
                                        <a href="" onClick={this.onBellClick}
                                           className="uk-icon-link"
                                           uk-tooltip="Powiadomienia"
                                           uk-icon="bell"/>
                                        {
                                            this.state.isNotificationListVisible && <NotificationList/>
                                        }
                                    </li>
                                    {
                                        this.loggedInUserNavigationRightSideLinks.map(((value, i) =>
                                            <li key={i}>
                                                <Link to={value.anchor}
                                                      style={{textTransform: "initial"}}>{value.title}</Link>
                                            </li>))
                                    }
                                    <li>
                                        <a href="" onClick={this.onLogoutClick}
                                           uk-tooltip="Wyloguj"
                                           className="uk-icon-link"
                                           uk-icon="sign-out"/>
                                    </li>
                                </ul>
                            </div>
                        </>
                        :
                        <div className="uk-margin-large-right uk-navbar-right">
                            <ul className="uk-navbar-nav">
                                {
                                    this.notLoggedInUserLinks.map(((value, i) =>
                                            <li key={i}>
                                                <Link to={value.anchor}
                                                      style={{textTransform: "initial"}}>{value.title}</Link>
                                            </li>
                                    ))
                                }
                            </ul>
                        </div>
                }

            </nav>
        );
    }
};

const NavigationWithRouter = withRouter(Navigation);

export default NavigationWithRouter;
