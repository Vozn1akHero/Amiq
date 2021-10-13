import React, {Component} from 'react';
import ReactDOM from "react-dom";
import "./centered-popup.scss"

type Props = {
    title: string;
    controlsVisible: boolean;
    id: string;
}

class CenteredPopup extends Component<Props> {
    container = document.getElementById("root");

    render() {
        return (
            <div id={this.props.id} uk-modal="true">
                <div className="uk-modal-dialog uk-modal-body">
                    <h2 className="uk-modal-title">{this.props.title}</h2>
                    {
                        this.props.controlsVisible && <p className="uk-text-right">
                            <button className="uk-button uk-button-default uk-modal-close" type="button">Anuluj</button>
                            <button className="uk-button uk-button-primary uk-margin-small-left" type="button">Zaakceptuj</button>
                        </p>
                    }
                </div>
            </div>
            /*ReactDOM.createPortal(
                <div id="centered-popup" className="uk-modal-container" uk-modal="true">
                    <div className="uk-modal-dialog uk-modal-body">
                        <h2 className="uk-modal-title">{this.props.title}</h2>
                        {
                            this.props.controlsVisible && <p className="uk-text-right">
                                <button className="uk-button uk-button-default uk-modal-close" type="button">Anuluj</button>
                                <button className="uk-button uk-button-primary" type="button">Zaakceptuj</button>
                            </p>
                        }
                    </div>
                </div>, this.container)*/
        );
    }
}

export default CenteredPopup;