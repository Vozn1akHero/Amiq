import React, {Component} from 'react';

class PostCreationForm extends Component {
    render() {
        return (
            <form>
                <fieldset className="uk-fieldset">
                    <legend className="uk-legend" style={{color: 'black'}}>Stwórz nowy wpis</legend>

                    <div className="uk-margin">
                        <input className="uk-input" type="text" placeholder="Temat" />
                    </div>

                    <div className="uk-margin">
                        <textarea className="uk-textarea" rows={5} placeholder="" ></textarea>
                    </div>

                </fieldset>
            </form>
        );
    }
}

export default PostCreationForm;
