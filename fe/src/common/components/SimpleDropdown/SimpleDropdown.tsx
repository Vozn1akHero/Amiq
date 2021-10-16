import React, {Component} from 'react';
import IDropdownOption from "./IDropdownOption";

type State = {
    isOpen: boolean;
    isCursorOnBody: boolean;
}
type Props = {
    placeholder?: string;
    options: Array<IDropdownOption>;
    handleOptionClick(option: IDropdownOption);
    icon?: string;
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
                    this.setState({
                       isOpen: true
                    })
                }} onMouseOut={() => {
                    if(this.myVar){
                        this.myVar();
                    }
                }} className="uk-button uk-button-default" style={{borderRadius: "1rem"}}>
                    {
                        this.props.placeholder && <span className={this.props.icon && `uk-margin-right`}>{this.props.placeholder}</span>
                    }
                    {
                        this.props.icon && <span uk-icon={`icon:${this.props.icon}`}></span>
                    }
                </div>
                <div className={`uk-dropdown ${this.state.isOpen && `uk-open`}`}>
                    <ul className="uk-nav uk-dropdown-nav"
                        onMouseOver={() => {this.setState({isCursorOnBody: true}); this.myVar();}}
                        onMouseOut={() => {this.setState({isCursorOnBody: false}); this.myVar();}}
                    >
                        {
                            this.props.options.map((value, index) => {
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
                            })
                        }
                        {/*  <li className="uk-nav-divider"></li>
                        <li><a href="#">Inne</a></li>*/}
                    </ul>
                </div>
            </div>
        );
    }
}

export default SimpleDropdown;