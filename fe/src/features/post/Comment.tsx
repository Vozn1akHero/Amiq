import React, {Component} from 'react';
import {IGroupPostComment, IPostComment} from "./models/post-comment";
import {Utils} from "core/utils";
import "./comment.scss"
import moment from "moment";

type Props = {
    comment: Partial<IGroupPostComment & IPostComment>;
    onReplyClick(commentId: string): void;
    onRemoveComment(postCommentId: string);
}

class Comment extends Component<Props> {
    /*onReplyClick = (event:MouseEvent) => {
        event.preventDefault();
        this.props.onReplyClick(this.props.comment.commentId);
    }*/

    onRemoveClick = () => {
        this.props.onRemoveComment(this.props.comment.commentId);
    }

    render() {
        return (
            <div className="comment uk-margin-small-top">
                <ul className="comment--list">
                    <li>
                        <article className="uk-comment uk-comment-primary uk-visible-toggle">
                            <header className="uk-comment-header uk-position-relative">
                                <div className="uk-grid uk-grid-medium uk-flex-middle">
                                    <div className="uk-width-auto">
                                        <img className="user-avatar-common border-radius-50 uk-comment-avatar"
                                             src={this.props.comment.group
                                                 ? Utils.getImageSrc(this.props.comment.group.avatarSrc) : Utils.getImageSrc(this.props.comment.author.avatarPath)}
                                             alt=""/>
                                    </div>
                                    <div className="uk-width-expand">
                                        <h4 className="uk-comment-title uk-margin-remove">
                                            <a className="uk-link-reset" href="#">{this.props.comment.group ?
                                                this.props.comment.group.name : this.props.comment.author.name + " " + this.props.comment.author.surname}</a>
                                        </h4>
                                        <p className="uk-comment-meta uk-margin-remove-top">
                                            <a className="uk-link-reset" href="#">
                                                {moment(this.props.comment.createdAt).fromNow()}
                                            </a>
                                        </p>
                                    </div>
                                </div>
                                <div className="uk-position-top-right uk-position-small uk-hidden-hover">
                                    <a onClick={e => {
                                        e.preventDefault();
                                        this.props.onReplyClick(this.props.comment.commentId);
                                    }}
                                       uk-icon="reply"
                                       className="uk-icon-link"/>
                                    <a uk-icon="trash"
                                       onClick={e => {
                                           e.preventDefault();
                                           this.props.onRemoveComment(this.props.comment.commentId);
                                       }}
                                       className="uk-icon-link uk-margin-small-left"/>
                                </div>
                            </header>
                            <div className="uk-comment-body">
                                <p>{this.props.comment.textContent}</p>
                            </div>
                        </article>
                    </li>
                </ul>
                {
                    this.props.comment.children && <ul className="children comment--list">
                        {
                            this.props.comment.children.map((value, index) => {
                                return <li key={index} className="child  uk-margin-small-top">
                                    <article className="uk-comment uk-comment-primary uk-visible-toggle">
                                        <header className="uk-comment-header uk-position-relative">
                                            <div className="uk-grid uk-grid-medium uk-flex-middle">
                                                <div className="uk-width-auto">
                                                    <img
                                                        className="user-avatar-common border-radius-50 uk-comment-avatar"
                                                        src={value.authorVisibilityType === 'GA'
                                                            ? Utils.getImageSrc(value.group.avatarSrc) : Utils.getImageSrc(value.author.avatarPath)}
                                                        alt=""/>
                                                </div>
                                                <div className="uk-width-expand">
                                                    <h4 className="uk-comment-title uk-margin-remove">
                                                        <a className="uk-link-reset"
                                                           href="#">{value.authorVisibilityType === 'GA' ?
                                                            value.group.name : value.author.name + " " + value.author.surname}</a>
                                                    </h4>
                                                    <p className="uk-comment-meta uk-margin-remove-top">
                                                        <a className="uk-link-reset" href="#">
                                                            {moment(value.createdAt).fromNow()}
                                                        </a>
                                                    </p>
                                                </div>
                                            </div>
                                            {
                                                !value.isRemoved && <div
                                                    className="uk-position-top-right uk-position-small uk-hidden-hover">
                                                    <a onClick={e => {
                                                        e.preventDefault();
                                                        this.props.onReplyClick(value.commentId);
                                                    }}
                                                       uk-icon="reply"
                                                       className="uk-icon-link"/>
                                                    <a uk-icon="trash"
                                                       onClick={e => {
                                                           e.preventDefault();
                                                           this.props.onRemoveComment(value.commentId);
                                                       }}
                                                       className="uk-icon-link uk-margin-small-left"/>
                                                </div>
                                            }
                                        </header>
                                        <div className="uk-comment-body">
                                            {value.isRemoved ?
                                                <p className="uk-text-bold">Komentarz został usunięty</p> :
                                                <p>{value.textContent}</p>}
                                        </div>
                                    </article>
                                </li>
                            })
                        }
                        {
                            //TODO
                            /*this.props.hasMoreCommentsThanPassed && <button className="get-more-children-btn">Wyświetl więcej</button>*/
                        }
                    </ul>
                }
            </div>
        );
    }
}

export default Comment;