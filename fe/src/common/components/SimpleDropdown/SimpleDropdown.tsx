import React, {Component, MouseEvent} from 'react';
import IDropdownOption from "./IDropdownOption";

type State = {
    isOpen: boolean;
    isCursorOnBody: boolean;
}
type Props = {
    options: Array<IDropdownOption>;
    handleOptionClick(option: IDropdownOption);
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

    onOptionClick = (e:MouseEvent<HTMLAnchorElement>, option: IDropdownOption) => {
        e.preventDefault();
        this.props.handleOptionClick(option);
        return false;
    }

    render() {
        return (
            <div className="simple-dropdown">
                <button onMouseOver={() => {
                    clearTimeout(this.myVar);
                    this.setState({
                       isOpen: true
                    })
                }} onMouseOut={() => {
                    if(this.myVar){
                        this.myVar();
                    }
                }} className="uk-button uk-button-default" type="button">Pokaż</button>
                <div className={`uk-dropdown ${this.state.isOpen && `uk-open`}`}>
                    <ul className="uk-nav uk-dropdown-nav"
                        onMouseOver={() => {this.setState({isCursorOnBody: true}); this.myVar();}}
                        onMouseOut={() => {this.setState({isCursorOnBody: false}); this.myVar();}}
                    >
                        {
                            this.props.options.map((value, index) => {
                                return <li>
                                    <a onClick={e => this.onOptionClick(e, value)} href="#">
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