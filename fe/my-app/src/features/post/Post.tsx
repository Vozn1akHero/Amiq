import React, {Component} from 'react';

class Post extends Component {
    render() {
        return (
            <>
                <ul className="uk-comment-list">
                    <li>
                        <article className="uk-comment uk-visible-toggle" >
                            <header className="uk-comment-header uk-position-relative">
                                <div className="uk-grid uk-grid-medium uk-flex-middle >" uk-grid>
                                    <div className="uk-width-auto uk-flex-first">
                                        <img className="uk-comment-avatar" src="https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg" width="80"
                                             height="80" alt=""/>
                                    </div>
                                    <div className="uk-width-expand">
                                        <h4 className="uk-comment-title uk-margin-remove"><a className="uk-link-reset"
                                                                                             href="#">Author</a></h4>
                                        <p className="uk-comment-meta uk-margin-remove-top"><a className="uk-link-reset"
                                                                                               href="#">12 days ago</a>
                                        </p>
                                    </div>
                                </div>
                                <div className="uk-position-top-right uk-position-small uk-hidden-hover">
                                    <a className="uk-link-muted" href="#">Reply</a>
                                </div>
                            </header>
                            <div className="uk-comment-body">
                                <p>Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod
                                    tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero
                                    eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea
                                    takimata sanctus est Lorem ipsum dolor sit amet.</p>
                            </div>
                        </article>
                        <ul>
                            <li>
                                <article className="uk-comment uk-comment-primary uk-visible-toggle">
                                    <header className="uk-comment-header uk-position-relative">
                                        <div className="uk-grid uk-grid-medium uk-flex-middle" uk-grid>
                                            <div className="uk-width-auto">
                                                <img className="uk-comment-avatar" src="https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg" width="80"
                                                     height="80" alt=""/>
                                            </div>
                                            <div className="uk-width-expand">
                                                <h4 className="uk-comment-title uk-margin-remove"><a
                                                    className="uk-link-reset" href="#">Author</a></h4>
                                                <p className="uk-comment-meta uk-margin-remove-top"><a
                                                    className="uk-link-reset" href="#">12 days ago</a></p>
                                            </div>
                                        </div>
                                        <div className="uk-position-top-right uk-position-small uk-hidden-hover"><a
                                            className="uk-link-muted" href="#">Reply</a></div>
                                    </header>
                                    <div className="uk-comment-body">
                                        <p>Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy
                                            eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam
                                            voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet
                                            clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit
                                            amet.</p>
                                    </div>
                                </article>
                            </li>
                        </ul>
                    </li>
                </ul>
            </>
        );
    }
}

export default Post;
