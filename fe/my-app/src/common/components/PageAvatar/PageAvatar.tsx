import React, {Component} from 'react';

class PageAvatar extends Component {
    avatarBgStyles : any = {
        backgroundImage: "url(\"https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg\")",
        backgroundPosition: "top center",
        backgroundSize: "cover",
        filter: "blur(15px)",
        opacity: "0.6",
        position: "absolute",
        top: 0,
        left: 0,
        height: "100%",
        width: "100%",
        zIndex: -1,
        transform: "scale(1.2)"
    }

    nameStyles: any = {
        fontWeight: "bold",
        marginTop: "15px"
    }

    nameWrapStyles: any = {
        width: "100%",
        height: "24%",
        position: "absolute",
        left: 0,
        bottom: 0,
        paddingLeft: "42px",
        background: "white"
    }

    render() {
        return (
            <div className="uk-card uk-card-default uk-card-body" style={{zIndex: 1, overflow: "hidden"}}>
                <div className="avatarBg" style={this.avatarBgStyles}></div>

                <img style={{borderRadius: '50%', border: "3px solid white"}}
                     src="https://pbs.twimg.com/profile_images/1086075447224328192/AJkoXqMq_400x400.jpg"
                     sizes="(min-width: 150px) 150px, 100vw" width="150" height="150" alt="" uk-img />

                <div className="uk-margin-medium-top name-wrap" style={this.nameWrapStyles}>
                    <h3 className="uk-card-title" style={this.nameStyles}>Dima Vozniachuk</h3>
                </div>
            </div>
        );
    }
}

export default PageAvatar;
