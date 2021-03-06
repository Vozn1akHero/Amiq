import React, {Component} from 'react';
import "./page-avatar.scss"
import {Utils} from "core/utils";
import {IUser} from "features/user/models/user";
import {IGroupData} from "features/group/models/group-models";

type Props = {
    viewTitle: string;
    avatarSrc: string;
    isChangeAvatarBtnVisible?: boolean;
    userSpecifics?: Partial<IUser>;
    groupSpecifics?: Partial<IGroupData>;
    onChangeAvatarBtnClick?(): void;
    //asBlob?:boolean;
}

class PageAvatar extends Component<Props, any> {
    nameStyles: any = {
        fontWeight: "bold",
        marginTop: "15px"
    }

    /*getBg = () => {
        if(this.props.asBlob){
            return "url(" + this.props.avatarSrc + ")";
        } else {
            return "url(" + Utils.getImageSrc(this.props.avatarSrc) + ")";
        }
    }

    getAvatar = () => {
        return this.props.asBlob ? this.props.avatarSrc : Utils.getImageSrc(this.props.avatarSrc);
    }*/

    render() {
        return (
            <div className="page-avatar uk-card uk-card-default uk-card-body">
                <div className="page-avatar__avatarBg"
                     style={{backgroundImage: "url(" + this.props.avatarSrc + ")"}}></div>

                <div className="page-avatar__img-wrap">
                    {
                        this.props.isChangeAvatarBtnVisible && <div className="page-avatar__change-avatar-btn">
                            <a onClick={e => {
                                    e.preventDefault();
                                    this.props.onChangeAvatarBtnClick();
                                }
                            }
                               className="uk-icon-button"
                               uk-icon="upload"/>
                        </div>
                    }
                    <img className="page-avatar__img"
                         src={this.props.avatarSrc}
                         alt=""/>
                </div>

                <div className="uk-margin-medium-top page-avatar__name-wrap uk-flex">
                    <h3 className="uk-card-title uk-margin-medium-left"
                        style={this.nameStyles}>{this.props.viewTitle}</h3>
                    <div className="page-avatar__controls uk-margin-small-right">
                        {
                            this.props.children
                        }
                    </div>
                </div>
            </div>
        );
    }
}

export default PageAvatar;
