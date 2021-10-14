import React, {Component} from 'react';
import {IDescriptionBlock} from "../../models/description-block";

type Props = {
    descriptionBlock: IDescriptionBlock;
    editModeEnabled: boolean;
}

type State = {

}

class DescriptionBlock extends Component<Props> {
    render() {
        return (
            <li>
                {
                    this.props.editModeEnabled ? <input className="uk-input"
                                                        value={this.props.descriptionBlock.header}
                                                        minLength={1} />
                        :
                        <a className="uk-accordion-title" href="#">{this.props.descriptionBlock.header}</a>
                }

                <div className="uk-accordion-content">
                    <p>{this.props.descriptionBlock.content}</p>
                </div>
            </li>
        );
    }
}

export default DescriptionBlock;