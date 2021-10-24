import React, {Component, MouseEvent } from 'react';
import {IPostComment} from "./models/post-comment";
import {Utils} from "../../core/utils";
import "./comment.scss"
import CommentCreationForm from "./CommentCreationForm";

type Props = {
    comment: IPostComment;
    onReplyClick(commentId: string):void;
    onRemoveComment(postCommentId: string);
}

class Comment extends Component<Props> {
    comment : IPostComment = this.props.comment;

    onReplyClick = (event:MouseEvent) => {
        event.preventDefault();
        this.props.onReplyClick(this.comment.commentId);
    }

    onRemoveClick = () => {
        this.props.onRemoveComment(this.comment.commentId);
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
                                        <img className="user-avatar-common border-radius-50 uk-comment-avatar" src={this.comment.group
                                            ? Utils.getImageSrc(this.comment.group.avatarSrc) : Utils.getImageSrc(this.comment.author.avatarPath)}
                                             alt=""/>
                                    </div>
                                    <div className="uk-width-expand">
                                        <h4 className="uk-comment-title uk-margin-remove">
                                            <a className="uk-link-reset" href="#">{this.comment.group ?
                                                this.comment.group.name : this.comment.author.name + " " + this.comment.author.surname}</a>
                                        </h4>
                                        <p className="uk-comment-meta uk-margin-remove-top">
                                            <a className="uk-link-reset" href="#">
                                                {this.comment.createdAt}
                                            </a>
                                        </p>
                                    </div>
                                </div>
                                <div className="uk-position-top-right uk-position-small uk-hidden-hover">
                                    <a href="#" onClick={this.onReplyClick} uk-icon="reply" className="uk-icon-link"></a>
                                    <a href="#" uk-icon="trash" className="uk-icon-link uk-margin-small-left"></a>
                                </div>
                            </header>
                            <div className="uk-comment-body">
                                <p>{this.comment.textContent}</p>
                            </div>
                        </article>
                    </li>
                </ul>
                {
                    this.comment.children && <ul className="children comment--list">
                        {
                            this.comment.children.map((value, index) => {
                                return <li key={index} className="child  uk-margin-small-top">
                                    <article className="uk-comment uk-comment-primary uk-visible-toggle">
                                        <header className="uk-comment-header uk-position-relative">
                                            <div className="uk-grid uk-grid-medium uk-flex-middle">
                                                <div className="uk-width-auto">
                                                    <img className="user-avatar-common border-radius-50 uk-comment-avatar" src={value.authorVisibilityType === 'GA'
                                                        ? Utils.getImageSrc(value.group.avatarSrc) : Utils.getImageSrc(value.author.avatarPath)}
                                                         alt=""/>
                                                </div>
                                                <div className="uk-width-expand">
                                                    <h4 className="uk-comment-title uk-margin-remove">
                                                        <a className="uk-link-reset" href="#">{value.authorVisibilityType === 'GA' ?
                                                            value.group.name : value.author.name + " " + value.author.surname}</a>
                                                    </h4>
                                                    <p className="uk-comment-meta uk-margin-remove-top">
                                                        <a className="uk-link-reset" href="#">
                                                            {value.createdAt}
                                                        </a>
                                                    </p>
                                                </div>
                                            </div>
                                            <div className="uk-position-top-right uk-position-small uk-hidden-hover">
                                                <a href="#" onClick={this.onReplyClick} uk-icon="reply" className="uk-icon-link"></a>
                                                <a href="#" uk-icon="trash" className="uk-icon-link uk-margin-small-left"></a>
                                            </div>
                                        </header>
                                        <div className="uk-comment-body">
                                            <p>{value.textContent}</p>
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