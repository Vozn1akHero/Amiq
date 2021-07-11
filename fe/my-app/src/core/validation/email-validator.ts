import { ValidatorBase } from "./validator-base";

export class EmailValidator extends ValidatorBase {
    private readonly _emailRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

    recheck(value: any): boolean {
        return value.toString().match(this._emailRegex)
    }

}