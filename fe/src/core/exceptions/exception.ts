export abstract class Exception {
    _message:string;

    public get message(){
        return this._message;
    }

    protected constructor(message:string) {
        this._message = message;
    }
}