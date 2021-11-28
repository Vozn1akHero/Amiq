import React, {Component} from 'react';
import {IGroupPostComment, IPostComment} from "./models/post-comment";
import {Utils} from "../../core/utils";
import moment from "moment";

type Props = {
    isFocused: boolean;
    responseCreationRunningEntity: Partial<IGroupPostComment & IPostComment>;
    dropResponseCreationRunningEntity(commentId: string);
    onCommentFormBlur(content: string): void;
    onCommentSubmit(text: string, commentVisibilityType: string): void;
    publishAsAdminOptionVisible: boolean;
}

class CommentCreationForm extends Component<Props, {
    isCreateButtonVisible: boolean;
    createAsAdmin: boolean;
    value: string
}> {
    constructor(props) {
        super(props);
        this.state = {
            createAsAdmin: false,
            isCreateButtonVisible: false,
            value: ""
        }
    }

    handleSubmit = (e) => {
        e.preventDefault();
        this.props.onCommentSubmit(this.state.value, this.state.createAsAdmin ? 'GA' : 'U');
    }

    componentDidUpdate(prevProps: Readonly<Props>, prevState: Readonly<{ isCreateButtonVisible: boolean; createAsAdmin: boolean; value: string }>, snapshot?: any) {
        console.log(this.props.responseCreationRunningEntity)
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
                <fieldset className="uk-fieldset">
                    <div className="uk-margin">
                        <textarea onBlur={e => {
                            this.props.onCommentFormBlur(e.target.value)
                        }}
                                  autoFocus={this.props.isFocused}
                                  className="uk-textarea"
                                  rows={3}
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
                    <div className="response-creation-info">
                        <div className="response-creation-info__wrap uk-flex">
                            <span>Odpowiadasz na komentarz</span>
                            <div className="uk-comment uk-comment-primary uk-margin-small-left">
                                <div className="uk-comment-header">
                                    <div className="uk-grid uk-grid-medium uk-flex-middle">
                                        <div className="uk-width-auto">
                                            <img
                                                className="user-avatar-common border-radius-50 uk-comment-avatar"
                                                src={this.props.responseCreationRunningEntity.authorVisibilityType === 'GA'
                                                    ? Utils.getImageSrc(this.props.responseCreationRunningEntity.group.avatarSrc)
                                                    : Utils.getImageSrc(this.props.responseCreationRunningEntity.author.avatarPath)}
                                                alt=""/>
                                        </div>
                                        <div className="uk-width-expand">
                                            <h4 className="uk-comment-title uk-margin-remove">
                                                <a className="uk-link-reset"
                                                   href="#">{this.props.responseCreationRunningEntity.authorVisibilityType === 'GA' ?
                                                    this.props.responseCreationRunningEntity.group.name
                                                    : this.props.responseCreationRunningEntity.author.name + " " + this.props.responseCreationRunningEntity.author.surname}</a>
                                            </h4>
                                            <p className="uk-comment-meta uk-margin-remove-top">
                                                <a className="uk-link-reset" href="#">
                                                    {moment(this.props.responseCreationRunningEntity.createdAt).fromNow()}
                                                </a>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <span
                                    className="uk-comment-body">{this.props.responseCreationRunningEntity.textContent}</span>
                            </div>
                            <a onClick={(e) => {
                                e.preventDefault();
                                this.props.dropResponseCreationRunningEntity(this.props.responseCreationRunningEntity.commentId)
                            }}
                               className="uk-icon-button uk-margin-small-left"
                               uk-icon="close"/>
                        </div>

                    </div>
                }
                {
                    this.props.publishAsAdminOptionVisible && this.state.isCreateButtonVisible &&
                    <div className="comment-as-group-admin-wrap">
                        <label><input className="uk-checkbox"
                                      type="checkbox"
                                      checked={this.state.createAsAdmin}
                                      onChange={() => {
                                          this.setState({
                                              createAsAdmin: !this.state.createAsAdmin
                                          })
                                      }}/> jako administrator</label>
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
