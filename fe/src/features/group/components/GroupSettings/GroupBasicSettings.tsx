import React, {Component} from 'react';

class GroupBasicSettings extends Component {
    render() {
        return (
            <div className="group-basic-settings">
                <input minLength={1} className="uk-input" placeholder="Nazwa" />
                <textarea className="uk-textarea post-creation-form__textarea uk-margin-top"
                          rows={3}
                          placeholder="Opis" />
            </div>
        );
    }
}

export default GroupBasicSettings;