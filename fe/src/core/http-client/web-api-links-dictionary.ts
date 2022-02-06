import {IWebApiLink} from "./web-api-link";
import {ModulesDictionary} from "../dictionaries/modules-dictionary";

export default class WebApiLinksDictionary {
    private static readonly _links : Array<IWebApiLink> = [
        {
            module: ModulesDictionary.Group,
            monolithLink: "",
            microservicesLink: ""
        }
    ]

    public static get links(){
        return this._links;
    }
}