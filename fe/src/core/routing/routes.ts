interface IRoute {
    link: string;
    subroutes: Array<string>,
    params: Array<string>,
    queryParams: string;
}

export default class Routes {
    public static getRouteAsString(route: IRoute) : string{
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
    }

    public static getLink(route: IRoute):string{
        let output: string = "/";
        output += route.link;
        return output;
    }

    public static getNavRoute = (route: IRoute) => route.link;

    public static get myProfilePageRoutes() : IRoute{
        return {
            link: "profile",
            subroutes: [],
            params: [],
            queryParams: ""
        };
    }

    public static get profilePageRoutes() : IRoute{
        return {
            link: "profile",
            subroutes: [],
            params: ["/:userId?"],
            queryParams: ""
        };
    }

    public static get friendListPageRoutes() : IRoute{
        return {
            link: "friends",
            subroutes: [],
            params: ["/:userId?"],
            queryParams: ""
        };
    }

    public static get chatPageRoutes() : IRoute{
        return {
            link: "chat",
            subroutes: [],
            params: [],
            queryParams: ""
        };
    }

    public static get groupsPageRoutes() : IRoute{
        return {
            link: "groups",
            subroutes: [],
            params: ["/:userId?"],
            queryParams: ""
        };
    }

    public static get groupPageRoutes() : IRoute{
        return {
            link: "group",
            subroutes: [],
            params: ["/:groupId"],
            queryParams: ""
        };
    }

    public static get authPageRoutes(): IRoute{
        return {
            link: "login",
            subroutes: [],
            params: [],
            queryParams: ""
        };
    }

    public static get registrationPageRoutes(): IRoute{
        return {
            link: "joinup",
            subroutes: [],
            params: [],
            queryParams: ""
        };
    }

    public static get logoutPageRoutes() : IRoute {
        return {
            link: "logout",
            subroutes: [],
            params: [],
            queryParams: ""
        }
    }
}
