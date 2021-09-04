import {Link} from "react-router-dom";
import {INavigationLink} from "./INavigationLink";
import {Component, memo, useState} from "react";
import "./logo.scss"
import {Routes} from "core/routing";
import {Observable, take} from "rxjs";
import {AuthStore} from "../../store/auth/auth-store";

type State = {
    isAuthenticated: boolean;
}

export class Navigation extends Component<any, State> {
    constructor(props) {
        super(props);
        this.state = {
            isAuthenticated: true
        }
    }


    navigationLinks : Array<INavigationLink> = [
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
            anchor: Routes.getNavRoute(Routes.chatPageRoutes)
        },
        {
            title: "Grupy",
            anchor: Routes.getNavRoute(Routes.groupsPageRoutes)
        }
    ];

    componentDidMount() {
        AuthStore.isAuthenticated$.subscribe(value => {
            this.setState({
                isAuthenticated: value
            })
        });
    }

    render() {
        return (
            <nav className="navigation uk-navbar-container uk-padding-small">
                <div className="logo uk-margin-large-left "></div>
                {
                    this.state.isAuthenticated && <div className="uk-margin-large-left uk-navbar-left">
                        <ul className="uk-navbar-nav">
                            {
                                this.navigationLinks.map(((value,i) => <li key={i}><Link to={value.anchor} style={{textTransform: "initial"}}>{value.title}</Link></li>))
                            }
                        </ul>
                    </div>
                }
            </nav>
        );
    }
};

export const MemoizedNavigation = memo(Navigation)
