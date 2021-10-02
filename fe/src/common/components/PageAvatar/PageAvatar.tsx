import React, {Component} from 'react';
import "./page-avatar.scss"
import devConfig from "../../../dev-config.json";
import {Utils} from "../../../core/utils";

type Props = {
    //userName: string;
    //userSurname: string;
    viewTitle: string;
    avatarSrc: string;
}

class PageAvatar extends Component<Props, any> {
    avatarBgStyles : any = {
        backgroundImage: "url("+Utils.getImageSrc(this.props.avatarSrc)+")",
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
                     src={Utils.getImageSrc(this.props.avatarSrc)}
                     alt="" />

                <div className="uk-margin-medium-top page-avatar__name-wrap">
                    <h3 className="uk-card-title" style={this.nameStyles}>{this.props.viewTitle}</h3>
                </div>
            </div>
        );
    }
}

export default PageAvatar;
