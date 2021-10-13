import React, {Component} from 'react';

class SimpleDropdown extends Component {
    render() {
        return (
            <div className="simple-dropdown">
                <button className="uk-button uk-button-default" type="button">Pokaż</button>
                <div uk-dropdown="true">
                    <ul className="uk-nav uk-dropdown-nav">
                        <li><a href="#">Wszystkie</a></li>
                        <li><a href="#">Administrowane</a></li>
                        <li><a href="#">Nieadministrowane</a></li>
                        {/*  <li className="uk-nav-divider"></li>
                        <li><a href="#">Inne</a></li>*/}
                    </ul>
                </div>
            </div>
        );
    }
}

export default SimpleDropdown;