interface ISimpleRoute {
    reactRouterLink: string;
    baseLink?: string;
    navLink?: string;
    //subroutes: Array<string>,
    //params: Array<string>,
    //queryParams: string;
}

/*interface IRouteWithChildren {
    parts: Array<string>;
    params: Array<string>;
    queryParams: string;
}*/

export default class Routes {
    /*public static getRouteAsString(route: ISimpleRoute) : string{
        let output: string = "/";
        output += route.link;
        route.subroutes.forEach(subroute => {
            output += subroute
        })
        route.params.forEach(param => {
            output += param
        })

        //todo
        //query params
        return output;
    }*/

    public static getRouterLink(route: ISimpleRoute):string{
        let output: string = "/";
        output += route.reactRouterLink;
        return output;
    }

    public static getBaseLink(route: ISimpleRoute):string{
        return route.baseLink;
    }

    public static getNavRoute = (route: ISimpleRoute) => route.reactRouterLink;

    public static get myProfilePageRoutes() : ISimpleRoute{
        return {
            reactRouterLink: "profile",
        };
    }

    public static get profilePageRoutes() : ISimpleRoute{
        return {
            reactRouterLink: "profile/:userId?",
            baseLink: "profile"
        };
    }

    public static get userSettingsRoutes() : ISimpleRoute {
        return {
            reactRouterLink: "settings"
        }
    }

    public static get friendListPageRoutes() : ISimpleRoute{
        return {
            reactRouterLink: "friends/:userId?",
            baseLink: "friends"
        };
    }

    public static get chatPageRoutes() : ISimpleRoute{
        return {
            reactRouterLink: "chat"
        };
    }

    public static get groupsPageRoutes() : ISimpleRoute{
        return {
            reactRouterLink: "groups"
        };
    }

    public static get groupPageRoutes() : ISimpleRoute{
        return {
            reactRouterLink: "group/:groupId",
            baseLink: "group"
        };
    }

    public static get groupEventRoutes(): ISimpleRoute {
        return {
            reactRouterLink: "group/:groupId/event/:eventId"
        }
    }

    public static get groupSettingsPageRoutes() : ISimpleRoute{
        return {
            reactRouterLink: "group-settings/:groupId",
        };
    }

    public static get authPageRoutes(): ISimpleRoute{
        return {
            reactRouterLink: "login"
        };
    }

    public static get registrationPageRoutes(): ISimpleRoute{
        return {
            reactRouterLink: "joinup"
        };
    }

    public static get logoutPageRoutes() : ISimpleRoute {
        return {
            reactRouterLink: "logout"
        }
    }

}
