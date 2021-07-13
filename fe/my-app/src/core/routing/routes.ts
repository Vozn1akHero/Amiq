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
        console.log(output)
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
}
