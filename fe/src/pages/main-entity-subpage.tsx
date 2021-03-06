import React, {Component} from 'react';
import {Link} from "react-router-dom";

type Props = {
    goBackLink: string;
}

class MainEntitySubpage extends Component<Props> {
    render() {
        return (
            <div className="main-entity-subpage">
                <Link to={this.props.goBackLink}
                      className="uk-background-muted uk-text-center uk-margin-remove-bottom border-radius-50 uk-box-shadow-small uk-padding-small"
                      style={{color: "black"}}>
                        <span className="uk-icon"
                              uk-icon="chevron-double-left"></span>
                </Link>
                <div className="uk-margin-medium-top">
                    {this.props.children}
                </div>
            </div>
        );
    }
}

export default MainEntitySubpage;