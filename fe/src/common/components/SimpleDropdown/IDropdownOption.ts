export default interface IDropdownOption{
    id: number;
    text: string;
    event?: () => void;
}