export  class HttpQueryParams{
    private _params : object = {};
    public get params(){
        return this._params
    }

    append(name: string, value: string | number) : HttpQueryParams{
        this._params = {...this._params, [name]: value}
        return this;
    }

    toStringQuery(): string{
        let output = "?";
        const count = Object.entries(this._params).length;
        for(let i = 0; i < count; i++){
            output += Object.entries(this._params)[i][0]+"="+Object.entries(this._params)[i][1];
            if(count - i > 1){
                output+="&";
            }
        }
        return output;
    }
}