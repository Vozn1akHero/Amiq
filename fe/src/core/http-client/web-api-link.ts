import {ModulesDictionary} from "../dictionaries/modules-dictionary";

export interface IWebApiLink {
    module: ModulesDictionary;
    monolithLink: string;
    microservicesLink: string;
}