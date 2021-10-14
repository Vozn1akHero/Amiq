interface ISimpleRoute {
    link: string;
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

    public static getSimpleLink(route: ISimpleRoute):string{
        let output: string = "/";
        output += route.link;
        return output;
    }

    public static getNavRoute = (route: ISimpleRoute) => route.link;

    public static get myProfilePageRoutes() : ISimpleRoute{
        return {
            link: "profile",
        };
    }

    public static get profilePageRoutes() : ISimpleRoute{
        return {
            link: "profile/:userId?",
        };
    }

    public static get friendListPageRoutes() : ISimpleRoute{
        return {
            link: "friends/:userId?"
        };
    }

    public static get chatPageRoutes() : ISimpleRoute{
        return {
            link: "chat"
        };
    }

    public static get groupsPageRoutes() : ISimpleRoute{
        return {
            link: "groups"
        };
    }

    public static get groupPageRoutes() : ISimpleRoute{
        return {
            link: "group/:groupId"
        };
    }

    public static get groupSettingsPageRoutes() : ISimpleRoute{
        return {
            link: "group/:groupId/settings",
        };
    }

    public static get authPageRoutes(): ISimpleRoute{
        return {
            link: "login"
        };
    }

    public static get registrationPageRoutes(): ISimpleRoute{
        return {
            link: "joinup"
        };
    }

    public static get logoutPageRoutes() : ISimpleRoute {
        return {
            link: "logout"
        }
    }

}
