export abstract class ValidatorBase {
    get isValid(): boolean {
        return this._isValid;
    }
    private _isValid : boolean = false;

    public abstract recheck(value: any) : boolean;
}