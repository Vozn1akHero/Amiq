import React, {Component, memo} from 'react';

class UiKitDefaultSpinner extends Component {
    render() {
        return (
            <div className="max-width">
                <div uk-spinner="true"></div>
            </div>
        );
    }
}

export default memo(UiKitDefaultSpinner);