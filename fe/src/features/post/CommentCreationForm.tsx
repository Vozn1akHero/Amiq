import React, {Component} from 'react';

type Props = {
    isFocused: boolean;
    responseCreationRunningEntity: {commentId: string};
    dropResponseCreationRunningEntity(commentId: string);
    onCommentFormBlur(content: string):void;
    onCommentSubmit(text: string):void;
    publishAsAdminOptionVisible:boolean;
}

class CommentCreationForm extends Component<Props, {isCreateButtonVisible: boolean; value: string}> {
    constructor(props) {
        super(props);
        this.state = {
            isCreateButtonVisible: false,
            value: ""
        }
    }

    handleSubmit = (e) => {
        e.preventDefault();
        this.props.onCommentSubmit(this.state.value);
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <fieldset className="uk-fieldset">
                    <div className="uk-margin">
                        <textarea onBlur={e=>{this.props.onCommentFormBlur(e.target.value)}}
                              autoFocus={this.props.isFocused}
                              className="uk-textarea"
                              rows={5}
                              onChange={(e) => {
                                this.setState({
                                    value: e.target.value,
                                    isCreateButtonVisible: e.target.value?.length > 0
                                })
                              }}
                             placeholder="Stwórz komentarz"/>
                    </div>
                </fieldset>
                {
                    this.props.responseCreationRunningEntity &&
                    <div className="response-creation-warning">
                        <span className="uk-text-primary">Odpowiadasz na komentarz {this.props.responseCreationRunningEntity.commentId}</span>
                        <button onClick={()=>this.props.dropResponseCreationRunningEntity(this.props.responseCreationRunningEntity.commentId)}
                                className="uk-button uk-icon-button">X</button>
                    </div>
                }
                {
                   this.props.publishAsAdminOptionVisible && this.state.isCreateButtonVisible &&  <div className="comment-as-group-admin-wrap">
                        <label><input className="uk-checkbox" type="checkbox"/> jako administrator</label>
                    </div>
                }
                {
                    this.state.isCreateButtonVisible && <button type="submit"
                                                                className="uk-button uk-button-primary uk-margin-small-top">Wyślij</button>
                }
            </form>
        );
    }
}

export default CommentCreationForm;
