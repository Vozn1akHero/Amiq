import React, {Component, Fragment} from 'react';
import {IDescriptionBlock} from "../../models/description-block";
import DescriptionBlock from "./DescriptionBlock";
import  "./description-blocks.scss"

type Props = {
    descriptionBlocks: Array<IDescriptionBlock>;
}

type State = {
    editModeEnabled: boolean;
}

class DescriptionBlocks extends Component<Props, State> {
    constructor(props) {
        super(props);

        this.state = {
            editModeEnabled: false
        }
    }


    render() {
        return (
            <div className="description-blocks">
                <a className="edit-btn" href="#" uk-icon="icon: pencil"></a>
                <ul uk-accordion="collapsible: false">
                    {
                        this.props.descriptionBlocks.map(((value, index) => {
                            return <DescriptionBlock key={index} descriptionBlock={value} editModeEnabled={false} />
                        }))
                    }
                </ul>
            </div>
        );
    }
}

export default DescriptionBlocks;