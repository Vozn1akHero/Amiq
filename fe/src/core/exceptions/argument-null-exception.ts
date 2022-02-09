import {Exception} from "./exception";

export default class ArgumentNullException extends Exception {
    constructor(variable: string, message:string = "") {
        super(message);

        console.log(variable + " cannot be null");
    }
}