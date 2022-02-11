import {Link, useHistory} from "react-router-dom";
import {INavigationLink} from "./INavigationLink";
import React, {createRef, ReactElement, RefObject, useEffect, useState} from "react";
import "./navigation.scss"
import {Routes} from "core/routing";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {NotificationList} from "../../features/notification/components/NotificationList";
import {useClickOutside} from "../../core/hooks/useClickOutside";
import {NotificationService} from "../../features/notification/notification-service";
import {StatusCodes} from "http-status-codes";


type Props = {
    navRef: RefObject<HTMLElement>;
}

export const Navigation = (props: Props) => {
    const [isAuthenticated, setIsAuthenticated] = useState(null);
    const [isNotificationListVisible, setIsNotificationListVisible] = useState(false);
    const [isNewNotificationsIconVisible, setIsNewNotificationsIconVisible] = useState(false);

    const history = useHistory();

    const notificationService = new NotificationService();

    const loggedInUserNavigationLinks: Array<INavigationLink> = [
        {
            title: "Profil",
            anchor: Routes.getRouterLink(Routes.myProfilePageRoutes),
            uiKitIcon: "user"
        },
        {
            title: "Znajomi",
            anchor: Routes.getRouterLink(Routes.friendListPageRoutes),
            uiKitIcon: "users",

        },
        {
            title: "Czat",
            anchor: Routes.getRouterLink(Routes.chatPageRoutes),
            uiKitIcon: "mail"
        },
        {
            title: "Grupy",
            anchor: Routes.getRouterLink(Routes.groupsPageRoutes),
            uiKitIcon: "social"
        }
    ];

    const loggedInUserNavigationRightSideLinks: Array<INavigationLink> = [
        {
            title: "Ustawienia",
            uiKitIcon: "cog",
            anchor: Routes.getRouterLink(Routes.userSettingsRoutes)
        }
    ]

    const notLoggedInUserLinks: Array<INavigationLink> = [
        {
            title: "Zaloguj",
            anchor: Routes.getRouterLink(Routes.authPageRoutes),
            uiKitIcon: "sign-in"
        },
        {
            title: "Dołącz",
            anchor: Routes.getRouterLink(Routes.registrationPageRoutes),
            uiKitIcon: "social"
        }
    ]

    useEffect(() => {
        AuthStore.isAuthenticated$.subscribe(isAuthenticated => {
            setIsAuthenticated(isAuthenticated);

            if(isAuthenticated){

                // TODO
                /*notificationService.anyNotReadExist().then(res => {
                    if(res.status === StatusCodes.OK){
                        const data: any = res.data;
                        const exist: boolean = data.result;
                        setIsNewNotificationsIconVisible(exist);
                    }
                })*/
            }
        });
    }, [])

    const onBellClick = e => {
        e.preventDefault();
        setIsNotificationListVisible(!isNotificationListVisible)

        if(isNewNotificationsIconVisible){
            notificationService.setAllNotificationsAsRead().then(res => {
                if(res.status === StatusCodes.OK){
                    setIsNewNotificationsIconVisible(false);
                }
            })
        }
    }

    const onLogoutClick = e => {
        e.preventDefault();
        AuthStore.logout().then(() => {
            history.push("/login");
        });
    }

    //#region powiadomienia

    //const clickRef = React.useRef();
    const clickRef = createRef<HTMLDivElement>();
    useClickOutside(clickRef, () => {
        setIsNotificationListVisible(false);
    });

    //#endregion

    return (
        <nav className="navigation uk-padding-small" ref={props.navRef}>
            <div className="logo uk-margin-large-left "></div>

            {
                isAuthenticated ? <>
                        <div className="uk-margin-large-left uk-navbar-left">
                            <ul className="uk-navbar-nav">
                                {
                                    loggedInUserNavigationLinks.map(((value, i) =>
                                        <li key={i}>
                                            <Link uk-tooltip={value.title} to={value.anchor}
                                                  style={{textTransform: "initial"}}>
                                                <span uk-icon={`icon: ${value.uiKitIcon}`}></span>
                                                {/* {value.title}*/}
                                            </Link>
                                        </li>))
                                }
                            </ul>
                        </div>
                        <div className="uk-margin-medium-right uk-navbar-right">
                            <ul className="uk-navbar-nav">
                                <li className={isNewNotificationsIconVisible ? `uk-position-relative` : ``}>
                                    {isNewNotificationsIconVisible && <div className="navigation__new-entries-icon-overlay"></div>}
                                    <a onClick={onBellClick}
                                       className="uk-icon-link"
                                       uk-tooltip="Powiadomienia"
                                       uk-icon="bell"/>
                                    {
                                        isNotificationListVisible && <NotificationList notifRef={clickRef}/>
                                    }
                                </li>
                                {
                                    loggedInUserNavigationRightSideLinks.map(((value, i) =>
                                        <li key={i}>
                                            <Link to={value.anchor}
                                                  style={{textTransform: "initial"}}>
                                                <span uk-icon={`icon: ${value.uiKitIcon}`}></span>
                                            </Link>
                                        </li>))
                                }
                                <li>
                                    <a href="" onClick={onLogoutClick}
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
                                notLoggedInUserLinks.map(((value, i) =>
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
};
