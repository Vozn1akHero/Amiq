import React, {Component} from 'react';

type Props = {
    handleSubmit(text: string):void;
    publishAsAdminOptionVisible:boolean;
}

class PostCreationForm extends Component<Props, {isCreateButtonVisible: boolean, value: string}> {
    constructor(props) {
        super(props);
        this.state = {
            isCreateButtonVisible: false,
            value: ""
        }
    }

    handleSubmit = (e) => {
        e.preventDefault();
        this.props.handleSubmit(this.state.value);
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <fieldset className="uk-fieldset">
                    <legend className="uk-legend" style={{color: 'black'}}>Stwórz nowy wpis</legend>

                    {/*<div className="uk-margin">
                        <input className="uk-input" type="text" placeholder="Temat" />
                    </div>*/}

                    <div className="uk-margin">
                        <textarea className="uk-textarea"
                                  rows={5}
                                  placeholder="Treść"
                                  onChange={(e) => {
                                      this.setState({
                                          value: e.target.value,
                                          isCreateButtonVisible: e.target.value?.length > 0
                                      })
                                  }}/>
                    </div>
                </fieldset>
                {
                    this.props.publishAsAdminOptionVisible && this.state.isCreateButtonVisible && <div className="comment-as-group-admin-wrap">
                        <label><input className="uk-checkbox" type="checkbox"/> jako administrator</label>
                    </div>
                }
                {
                    this.state.isCreateButtonVisible && <button type="submit" className="uk-button uk-button-primary uk-margin-small-top">Wyślij</button>
                }
            </form>
        );
    }
}

export default PostCreationForm;
