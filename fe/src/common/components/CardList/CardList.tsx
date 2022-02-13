import React, {Component, Fragment} from 'react';

class CardList extends Component {
    render() {
        return (
            <Fragment>
                {
                    !this.props.children ?
                        <div style={{
                            width: "100%",
                            height: "3rem",
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "center"
                        }}>
                            <span>Lista jest pusta</span>
                        </div>
                        : this.props.children
                }
            </Fragment>
        );
    }
}

export default CardList;