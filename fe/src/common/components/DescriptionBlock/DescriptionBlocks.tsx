import React, {Component, Fragment} from 'react';
import {IDescriptionBlock} from "../../models/description-block";
import DescriptionBlock from "./DescriptionBlock";
import  "./description-blocks.scss"

type Props = {
    descriptionBlocks: Array<IDescriptionBlock>;
}

type State = {

}

class DescriptionBlocks extends Component<Props, State> {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="description-blocks">
                <ul uk-accordion="collapsible: false">
                    {
                        this.props.descriptionBlocks.map(((value, index) => {
                            return <DescriptionBlock key={index} descriptionBlock={value} />
                        }))
                    }
                </ul>
            </div>
        );
    }
}

export default DescriptionBlocks;