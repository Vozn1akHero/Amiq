export class HttpParams{
    private _params : object = [];
    public get params(){
        return this._params
    }

    append(name: string, value: string) : HttpParams{
        this._params = {...this._params, [name]: value}

        return this;
    }
}