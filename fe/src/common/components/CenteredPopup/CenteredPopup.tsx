import React, {Component} from 'react';
import "./centered-popup.scss"
import {ModalService} from "../../../core/modal-service";

type Props = {
    title: string;
    controlsVisible: boolean;
    id: string;
}

class CenteredPopup extends Component<Props> {
    onClickOutside = () => {
        ModalService.close();
    }

    render() {
        return (
            <div className="centered-popup">
                <div id="centered-popup-background" className="centered-popup__background">
                    <div className="centered-popup__body uk-margin-large-top"
                         onMouseOver={() => {
                             document.getElementById("centered-popup-background")
                                 .removeEventListener("click", this.onClickOutside)
                         }}
                         onMouseLeave={() => {
                             document.getElementById("centered-popup-background")
                                 .addEventListener("click", this.onClickOutside)
                         }}>
                        <h2 className="uk-modal-title">{this.props.title}</h2>
                        {this.props.children}
                    </div>
                </div>
            </div>
            /*ReactDOM.createPortal(
                <div className="centered-popup">
                    <div className="centered-popup__background" onClick={this.onClickOutside}>
                        <div className="centered-popup__body uk-margin-large-top">
                            <h2 className="uk-modal-title">{this.props.title}</h2>
                            { this.props.children }
                        </div>
                    </div>
                </div>, document.getElementById("modal"))*/
        );
    }
}

export default CenteredPopup;