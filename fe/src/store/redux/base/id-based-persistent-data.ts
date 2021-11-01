export interface IIdBasedPersistentDataEntry<T> {
    data: T,
    id: number|string
}

export interface IIdBasedPersistentData<T>{
    entries: Array<IIdBasedPersistentDataEntry<T>>;
}