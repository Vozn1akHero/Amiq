export class HttpParams{
    private _params : object = {};
    public get params(){
        return this._params
    }

    append(name: string, value: string | number) : HttpParams{
        this._params = {...this._params, [name]: value}

        return this;
    }

    toStrParams(): string {
        let output = "";
        const count = Object.entries(this._params).length;
        for(let i = 0; i < count; i++){
            output += Object.values(this._params)[i];
            if(count - i > 1){
                output+="/";
            }
        }
        return output;
    }


}