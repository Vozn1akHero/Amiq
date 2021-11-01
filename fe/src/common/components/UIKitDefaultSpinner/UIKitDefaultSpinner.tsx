import React, {Component, memo} from 'react';

class UiKitDefaultSpinner extends Component {
    render() {
        return (
            <div uk-spinner="true"></div>
        );
    }
}

export default memo(UiKitDefaultSpinner);