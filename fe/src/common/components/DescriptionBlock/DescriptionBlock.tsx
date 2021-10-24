import React, {Component} from 'react';
import {IDescriptionBlock} from "../../models/description-block";

type Props = {
    descriptionBlock: IDescriptionBlock;
}

type State = {
    descriptionBlock: IDescriptionBlock;
}

class DescriptionBlock extends Component<Props, State> {
    constructor(props) {
        super(props);

        this.state = {
            descriptionBlock: this.props.descriptionBlock
        }
    }


    render() {
        return (
            <li className="description-block">
                <a className="uk-accordion-title" href="#">{this.props.descriptionBlock.header}</a>

                <div className="uk-accordion-content">
                    <p>{this.props.descriptionBlock.content}</p>
                </div>
            </li>
        );
    }
}

export default DescriptionBlock;