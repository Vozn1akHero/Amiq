import React, {Component} from 'react';
import {debounceTime, distinctUntilChanged, fromEvent, map, Subscription} from "rxjs";

type Props = {
    debounceTime: number;
    onDebounceInputChange(text:string):void;
}

class DebounceInput extends Component<Props, never> {
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
            <input className="uk-input" ref={this.inputRef} type="text" placeholder="Szukaj grup"/>
        );
    }
}

export default DebounceInput;