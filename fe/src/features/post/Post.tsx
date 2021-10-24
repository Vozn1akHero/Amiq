import React, {Component, ComponentClass, MouseEvent} from 'react';
import "./post.scss"
import {IUserPost} from "./models/user-post";
import {IPostComment, IPostCommentCreation} from "./models/post-comment";
import Comment from "./Comment";
import CommentCreationForm from "./CommentCreationForm";
import {IPost} from "./models/post";

type Props  = {
    postId: string;
    //authorId: number;
    avatarPath: string;
    viewName: string;
    authorLink: string;
    createdAt: Date;
    text: string;
    deleteBtnVisible: boolean;
    publishCommentAsAdminOptionVisible: boolean;
    comments: Array<IPostComment>;
    hasMoreCommentsThanPassed: boolean;
    onCommentCreated(data: IPostCommentCreation);
    onRemoveComment(postCommentId: string);
    onDeletePost(postId: string);
    onRemoveComment(postCommentId: string);
}

type State = {
    isCommentCreationFormVisible: boolean;
    isCommentCreationFormFocused: boolean;
    responseCreationRunningEntity: {commentId: string;}
}

class Post extends Component<Props, State> {
    postCreationFormRef = React.createRef<HTMLDivElement>()

    constructor(props) {
        super(props);
        this.state = {
            isCommentCreationFormVisible: false,
            isCommentCreationFormFocused: false,
            responseCreationRunningEntity: null
        }
        //this.postCreationFormRef = React.createRef()
    }

    handleReply = () => {
        this.setState({
            isCommentCreationFormVisible: true,
            isCommentCreationFormFocused: true
        }, ()=>{
            this.setState({
                isCommentCreationFormFocused: false
            })
            //this.postCreationFormRef.current.scrollIntoView({ behavior: "smooth" });
        })
    }

    onReplyToPostClick = (e) => {
        e.preventDefault();
        this.handleReply();
    }

    onCommentCreationFormBlur = (text: string) => {
        this.setState({
            isCommentCreationFormFocused: false
        })
        if(!text){
            this.setState({
                isCommentCreationFormVisible: false
            })
        }
    }

    onCommentSubmit = (text: string, commentVisibilityType: string) => {
        let entity : Partial<IPostCommentCreation> = {};
        entity.authorVisibilityType = commentVisibilityType;
        entity.postId = this.props.postId;
        if(this.state.responseCreationRunningEntity){
            const responseCreationRunningEntity = this.props.comments
                .filter(value => value.commentId === this.state.responseCreationRunningEntity.commentId)[0];
            entity.mainParentId = responseCreationRunningEntity.mainParentId
                ? responseCreationRunningEntity.mainParentId : responseCreationRunningEntity.commentId;
            entity.parentId = responseCreationRunningEntity.commentId;
        }
        entity.textContent = text;
        this.props.onCommentCreated(entity as IPostCommentCreation);
    }

    onCommentReplyClick = (commentId: string) => {
        this.setState({
            responseCreationRunningEntity: {commentId}
        });
        this.handleReply();
    }

    onDropResponseCreationRunningEntity = (commentId: string) => {
        this.setState({
            responseCreationRunningEntity: null
        })
    }

    handlePostDelete = (e: MouseEvent) => {
        e.preventDefault();
        this.props.onDeletePost(this.props.postId);
    }

    render() {
        return (
            <div className="post uk-margin-large-bottom">
                <article className="uk-comment uk-visible-toggle uk-margin-medium-bottom" >
                    <header className="uk-comment-header uk-position-relative">
                        <div className="uk-grid uk-grid-medium uk-flex-middle >" >
                            <div className="uk-width-auto uk-flex-first">
                                <img className="border-radius-50 user-avatar-common" src={this.props.avatarPath}
                                     alt=""/>
                            </div>
                            <div className="uk-width-expand">
                                <h4 className="uk-comment-title uk-margin-remove">
                                    <a className="uk-link-reset" href={this.props.authorLink}>{this.props.viewName}</a>
                                </h4>
                                <p className="uk-comment-meta uk-margin-remove-top">
                                    <a className="uk-link-reset" href="#">{this.props.createdAt}</a>
                                </p>
                            </div>
                        </div>

                        <div className="uk-position-top-right uk-position-small uk-hidden-hover">
                            <a href="" onClick={this.onReplyToPostClick} uk-icon="reply" className="uk-icon-link"></a>
                            <a href="" onClick={this.handlePostDelete} uk-icon="trash" className="uk-icon-link uk-margin-small-left"></a>
                        </div>
                    </header>
                    <div className="uk-comment-body">
                        <p>{this.props.text}</p>
                    </div>
                </article>
                {
                    this.props.comments?.map((value, index) => {
                        return <Comment onReplyClick={this.onCommentReplyClick}
                                        onRemoveComment={this.props.onRemoveComment}
                                        comment={value}
                                        key={index} />
                    })
                }
                {
                    this.props.hasMoreCommentsThanPassed && <button className="get-more-children-btn">Wyświetl więcej</button>
                }
                { this.state.isCommentCreationFormVisible &&
                    <div ref={this.postCreationFormRef} className="comment-creation-form-wrap uk-margin-medium-top">
                         <CommentCreationForm isFocused={this.state.isCommentCreationFormFocused}
                                              publishAsAdminOptionVisible={this.props.publishCommentAsAdminOptionVisible}
                                              responseCreationRunningEntity={this.state.responseCreationRunningEntity}
                                              dropResponseCreationRunningEntity={this.onDropResponseCreationRunningEntity}
                                              onCommentSubmit={this.onCommentSubmit}
                                              onCommentFormBlur={this.onCommentCreationFormBlur}
                         />
                    </div>
                }
            </div>
        );
    }
}

export default Post;
