export default interface ISubpagePreloadSpecification {
    isSatisfied():boolean|Promise<boolean>;
}