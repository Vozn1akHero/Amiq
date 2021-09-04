export class Store {

}

interface IStore {

}

namespace IStore {
    type Constructor<T> = {
        new (...args: any[]): T;
        readonly prototype: T;
    };
    const implementations: Constructor<IStore>[] = [];
    export function GetImplementations(): Constructor<IStore>[] {
        return implementations;
    }
    export function register<T extends Constructor<IStore>>(ctor: T) {
        implementations.push(ctor);
        return ctor;
    }
}