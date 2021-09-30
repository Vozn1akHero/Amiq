import {ValidationResult} from "./validation-result";

export abstract class ValidatorBase<T> {
    get isValid(): boolean {
        return this._isValid;
    }
    private _isValid : boolean = false;

    public abstract recheck(value: T) : ValidationResult;
}
