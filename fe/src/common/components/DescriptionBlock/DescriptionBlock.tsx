import React, {Component} from 'react';
import {IDescriptionBlock} from "../../models/description-block";

type Props = {
    descriptionBlock: IDescriptionBlock;
    editModeEnabled: boolean;
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
                {
                    this.props.editModeEnabled ? <input className="uk-input"
                                                        defaultValue={this.props.descriptionBlock.header}
                                                        onChange={(e) => {
                                                                this.setState({
                                                                    descriptionBlock: {
                                                                        ...this.props.descriptionBlock,
                                                                        header: e.target.value
                                                                    }
                                                                })
                                                            }
                                                        }
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