import {Component} from "react";
import "reflect-metadata";

export default class BasePageComponent extends Component {
    get userId(): string {
        return this._userId;
    }

    set userId(value: string) {
        this._userId = value;
    }
    private _userId: string;
}
