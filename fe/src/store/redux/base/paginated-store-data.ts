export interface IPaginatedStoreData<T> {
    entities: Array<T>,
    loaded: boolean,
    loading: boolean,
    currentPage: number,
    length: number
}