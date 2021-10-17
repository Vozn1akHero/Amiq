import React, {Component} from 'react';
import {debounceTime, distinctUntilChanged, fromEvent, map, Subscription} from "rxjs";
import "./search-input.scss"
import UiKitDefaultSpinner from "../UIKitDefaultSpinner/UIKitDefaultSpinner";

type Props = {
    debounceTime: number;
    onDebounceInputChange(text:string):void;
    showSpinner: boolean;
}

class SearchInput extends Component<Props, never> {
    inputRef: React.RefObject<HTMLInputElement>
    sub: Subscription

    constructor(props) {
        super(props);
        this.inputRef = React.createRef();
    }

    componentDidMount() {
        this.onSearch();
    }

    onSearch = () => {
        this.sub = fromEvent(this.inputRef.current, 'keyup').pipe(
            map((event: any) => {
                return event.target.value;
            }),
            debounceTime(700),
            distinctUntilChanged()
        ).subscribe((text: string) => {
            this.props.onDebounceInputChange(text);
        });
    }

    componentWillUnmount() {
        this.sub?.unsubscribe();
    }

    render() {
        return (
            <div className="search-input">
                <input className="uk-input" ref={this.inputRef} type="text" placeholder="Szukaj"/>
                <div className="search-input__align-right-content">
                    {
                        this.props.showSpinner ? <UiKitDefaultSpinner /> : <span className="search-input__icon" uk-icon="search"></span>
                    }
                </div>
            </div>
        );
    }
}

export default SearchInput;