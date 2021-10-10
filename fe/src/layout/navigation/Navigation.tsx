import {Link} from "react-router-dom";
import {INavigationLink} from "./INavigationLink";
import {Component, memo, RefObject, useState} from "react";
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
    navRef: RefObject<HTMLElement>
}

export class Navigation extends Component<Props, State> {
    constructor(props) {
        super(props);
        this.state = {
            isAuthenticated: true,
            isNotificationListVisible: false
        }
    }

    loggedInUserNavigationLinks : Array<INavigationLink> = [
        {
            title: "Profil",
            anchor: Routes.getLink(Routes.profilePageRoutes)
        },
        {
            title: "Znajomi",
            anchor: Routes.getLink(Routes.friendListPageRoutes)
        },
        {
            title: "Czat",
            anchor: Routes.getLink(Routes.chatPageRoutes)
        },
        {
            title: "Grupy",
            anchor: Routes.getLink(Routes.groupsPageRoutes)
        }
    ];

    loggedInUserNavigationRightSideLinks : Array<INavigationLink> = [
        {
            title: "Wyloguj",
            anchor: Routes.getLink(Routes.logoutPageRoutes)
        }
    ]

    notLoggedInUserLinks : Array<INavigationLink> = [
        {
            title: "Zaloguj",
            anchor: Routes.getLink(Routes.authPageRoutes)
        },
        {
            title: "Dołącz",
            anchor: Routes.getLink(Routes.registrationPageRoutes)
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

    render() {
        return (
            <nav className="navigation uk-padding-small" ref={this.props.navRef}>
                <div className="logo uk-margin-large-left "></div>

                {
                    this.state.isAuthenticated ? <>
                            <div className="uk-margin-large-left uk-navbar-left">
                                <ul className="uk-navbar-nav">
                                    {
                                        this.loggedInUserNavigationLinks.map(((value,i) =>
                                            <li key={i}>
                                                <Link to={value.anchor} style={{textTransform: "initial"}}>{value.title}</Link>
                                            </li>))
                                    }
                                </ul>
                            </div>
                            <div className="uk-margin-medium-right uk-navbar-right">
                                <ul className="uk-navbar-nav">
                                    <li>
                                        <a href="" onClick={this.onBellClick}
                                           className="uk-icon-link"
                                           uk-icon="bell" />
                                        {
                                            this.state.isNotificationListVisible && <NotificationList />
                                        }
                                    </li>
                                    {
                                        this.loggedInUserNavigationRightSideLinks.map(((value,i) =>
                                            <li key={i}>
                                                <Link to={value.anchor} style={{textTransform: "initial"}}>{value.title}</Link>
                                            </li>))
                                    }
                                </ul>
                            </div>
                        </>
                        :
                    <div className="uk-margin-large-right uk-navbar-right">
                        <ul className="uk-navbar-nav">
                            {
                                this.notLoggedInUserLinks.map(((value,i) =>
                                    <li key={i}>
                                        <Link to={value.anchor} style={{textTransform: "initial"}}>{value.title}</Link>
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

export const MemoizedNavigation = memo(Navigation)
