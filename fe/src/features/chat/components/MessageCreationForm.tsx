import React, {Component} from 'react';
import "./message-creation-form.scss"

type Props = {
    isFocused: boolean;
    onFormBlur(content: string):void;
    onSubmit(text: string):void;
}

class MessageCreationForm extends Component<Props, {isCreateButtonVisible: boolean; value: string}> {
    constructor(props) {
        super(props);
        this.state = {
            isCreateButtonVisible: false,
            value: ""
        }
    }

    handleSubmit = (e) => {
        e.preventDefault();
        this.props.onSubmit(this.state.value);
    }

    render() {
        return (
            <form className="message-creation-form uk-background-muted" onSubmit={this.handleSubmit}>
                <fieldset className="uk-fieldset">
                    <div className="uk-margin">
                        <textarea onBlur={e=>{this.props.onFormBlur(e.target.value)}}
                                  autoFocus={this.props.isFocused}
                                  className="message-creation-form__textarea uk-textarea"
                                  rows={3}
                                  onChange={(e) => {
                                      this.setState({
                                          value: e.target.value,
                                          isCreateButtonVisible: e.target.value?.length > 0
                                      })
                                  }}
                                  placeholder="Napisz wiadomość"/>
                    </div>
                </fieldset>
                <button type="submit" className="uk-button uk-button-primary uk-margin-small-top">Wyślij</button>
            </form>
        );
    }
}

export default MessageCreationForm;
