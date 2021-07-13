import {Link} from "react-router-dom";
import {INavigationLink} from "./INavigationLink";
import {memo} from "react";
import "./logo.scss"
import {Routes} from "core/routing";
type Props = {

};
export function Navigation (props: Props) {
    const navigationLinks : Array<INavigationLink> = [
        {
            title: "Profil",
            anchor: Routes.getNavRoute(Routes.profilePageRoutes)
        },
        {
            title: "Znajomi",
            anchor: Routes.getNavRoute(Routes.friendListPageRoutes)
        },
        {
            title: "Czat",
            anchor: Routes.getNavRoute(Routes.profilePageRoutes)
        },
        {
            title: "Grupy",
            anchor: Routes.getNavRoute(Routes.profilePageRoutes)
        }
    ];

    return (
        <nav className="navigation uk-navbar-container uk-padding-small">
            <div className="logo uk-margin-large-left "></div>
            <div className="uk-margin-large-left uk-navbar-left">
                <ul className="uk-navbar-nav">
                    {
                        navigationLinks.map(((value,i) => <li key={i}><Link to={value.anchor} style={{textTransform: "initial"}}>{value.title}</Link></li>))
                    }
                </ul>
            </div>
        </nav>
    );
};

export const MemoizedNavigation = memo(Navigation)
