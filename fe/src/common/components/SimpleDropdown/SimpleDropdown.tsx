import React, {Component} from 'react';
import IDropdownOption from "./IDropdownOption";
import "./simple-dropdown.scss"
import UiKitDefaultSpinner from "../UIKitDefaultSpinner/UIKitDefaultSpinner";

type State = {
    isOpen: boolean;
    isCursorOnBody: boolean;
}
type Props = {
    isStatic: boolean;
    placeholder?: string;
    options: Array<IDropdownOption>;
    handleOptionClick(option: IDropdownOption);
    icon?: string;
    areOptionsLoaded?:boolean;
    onDropdownMouseOver?: () => void;
}

class SimpleDropdown extends Component<Props,State> {
    myVar:any;

    constructor(props) {
        super(props);

        this.state = {
            isOpen: false,
            isCursorOnBody: false
        }

        this.myVar = () => {
            setTimeout(()=>{
                if(!this.state.isCursorOnBody)
                    this.setState({
                        isOpen: false
                    })
            }, 200);
        }
    }

    render() {
        return (
            <div className="simple-dropdown">
                <div onMouseOver={() => {
                    clearTimeout(this.myVar);
                    if(this.props.onDropdownMouseOver)
                        this.props.onDropdownMouseOver();
                    this.setState({
                       isOpen: true
                    })
                }} onMouseOut={() => {
                    if(this.myVar){
                        this.myVar();
                    }
                }} className="uk-button uk-button-default simple-dropdown__btn">
                    {
                        this.props.placeholder && <span className={this.props.icon && `uk-margin-right`}>{this.props.placeholder}</span>
                    }
                    {
                        this.props.icon && <span uk-icon={`icon:${this.props.icon}`}></span>
                    }
                </div>
                <div className={`simple-dropdown__dropdown uk-dropdown uk-background-default ${this.state.isOpen && `uk-open`}`}>
                    <ul className="simple-dropdown__nav uk-nav uk-dropdown-nav"
                        onMouseOver={() => {this.setState({isCursorOnBody: true}); this.myVar();}}
                        onMouseOut={() => {this.setState({isCursorOnBody: false}); this.myVar();}}
                    >
                        {
                            this.props.isStatic || this.props.areOptionsLoaded ? this.props.options.map((value, index) => {
                                return <li key={index}>
                                    <a onClick={e => {
                                        if(value.event) {
                                            e.preventDefault();
                                            value.event();
                                        }
                                        else {
                                            e.preventDefault();
                                            this.props.handleOptionClick(value);
                                            //return false;
                                        }
                                    }
                                    } href="#">
                                        {value.text}
                                    </a>
                                </li>
                            }) : <UiKitDefaultSpinner />
                        }
                    </ul>
                </div>
            </div>
        );
    }
}

export default SimpleDropdown;