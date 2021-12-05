import React, {Component} from 'react';
import "./post-creation-form.scss"

type Props = {
    handleSubmit(value: {text: string, createAsAdmin?: boolean }):void;
    publishAsAdminOptionVisible:boolean;

}

class PostCreationForm extends Component<Props, {isCreateButtonVisible: boolean, isCreateAsAdminChecked?:boolean, value: string}> {
    constructor(props) {
        super(props);
        this.state = {
            isCreateButtonVisible: false,
            value: "",
            isCreateAsAdminChecked: true
        }
    }

    handleSubmit = (e) => {
        e.preventDefault();

        let post: {text: string, createAsAdmin?: boolean } = {
            text: this.state.value
        }
        if(this.props.publishAsAdminOptionVisible){
            post = {
                ...post,
                createAsAdmin: this.state.isCreateAsAdminChecked
            }
        }

        this.props.handleSubmit(post);
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit} className="post-creation-form">
                <fieldset className="uk-fieldset">
                    <legend className="uk-legend" style={{color: 'black'}}>Stwórz nowy wpis</legend>

                    {/*<div className="uk-margin">
                        <input className="uk-input" type="text" placeholder="Temat" />
                    </div>*/}

                    <div className="uk-margin">
                        <textarea className="uk-textarea post-creation-form__textarea"
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
                        <label><input className="uk-checkbox"
                                      checked={this.state.isCreateAsAdminChecked}
                                      onChange={()=>{
                                          this.setState({
                                              isCreateAsAdminChecked: !this.state.isCreateAsAdminChecked
                                          })
                                      }}
                                      type="checkbox" /> jako administrator</label>
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
