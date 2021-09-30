import React, {Component} from 'react';
import "./page-avatar.scss"

type Props = {
    //userName: string;
    //userSurname: string;
    viewTitle: string;
}

class PageAvatar extends Component<Props, any> {
    avatarBgStyles : any = {
        backgroundImage: "url(\"https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg\")",
    }

    nameStyles: any = {
        fontWeight: "bold",
        marginTop: "15px"
    }

    render() {
        return (
            <div className="page-avatar uk-card uk-card-default uk-card-body">
                <div className="page-avatar__avatarBg" style={this.avatarBgStyles}></div>

                <img className="page-avatar__img"
                     src="https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"
                     sizes="(min-width: 150px) 150px, 100vw" width="150" height="150" alt="" />

                <div className="uk-margin-medium-top page-avatar__name-wrap">
                    <h3 className="uk-card-title" style={this.nameStyles}>{this.props.viewTitle}</h3>
                </div>
            </div>
        );
    }
}

export default PageAvatar;
