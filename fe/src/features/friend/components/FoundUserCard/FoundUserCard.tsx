import {Utils} from "../../../../core/utils";
import React from "react";
import { Link } from "react-router-dom";
import "./found-user-card.scss"

type FriendCardProps = {
    userId: number;
    name: string;
    surname: string;
    avatarPath: string;
    blockedByIssuer?: boolean;
    issuerBlocked?: boolean;
    isIssuerFriend?: boolean;
    issuerReceivedFriendRequest?: boolean;
    issuerSentFriendRequest?: boolean;
    friendRequestId?: string;
    onAcceptFriendRequest?(friendRequestId:string):void;
    onCancelFriendRequest?(friendRequestId:string):void;
    onRejectFriendRequest?(friendRequestId:string):void;
    onRemoveFriendById?(friendId: number):void;
}

const FoundUserCard = (props: Partial<FriendCardProps>) => {
    const avatarBgStyles = {
        backgroundImage: "url(" + Utils.getImageSrc(props.avatarPath) + ")"
    }

    const renderControls = () => {
        if(props.isIssuerFriend){
            return <>
                <Link to={`/chat?to=${props.userId}`}
                      uk-icon="mail"
                      className="uk-icon-link"/>
                <a uk-icon="trash"
                   onClick={e => {
                       e.preventDefault();
                       props.onRemoveFriendById(props.userId);
                   }}
                   className="uk-icon-link uk-margin-small-left" />
            </>
        } else if (props.blockedByIssuer) {
            return <>
                <a href="#" uk-icon="unlock" className="uk-icon-link uk-margin-small-left"></a>
            </>
        } else if(props.issuerBlocked) {
            return <a href="#" uk-icon="lock" className="uk-icon-link uk-margin-small-left"></a>
        } else if(props.issuerReceivedFriendRequest) {
            return <>
                <a uk-icon="check"
                   onClick={e=>{
                       e.preventDefault();
                       props.onAcceptFriendRequest(props.friendRequestId);
                   }}
                   className="uk-icon-link uk-margin-small-left" />
                <a href=""
                   uk-icon="close"
                   onClick={e=>{
                       e.preventDefault();
                       props.onRejectFriendRequest(props.friendRequestId);
                   }}
                   className="uk-icon-link uk-margin-small-left" />
            </>
        } else if(props.issuerSentFriendRequest) {
            return <a uk-icon="close"
                      onClick={e=>{
                          e.preventDefault();
                          props.onCancelFriendRequest(props.friendRequestId);
                      }}
                      className="uk-icon-link uk-margin-small-left"></a>;
        }
    }

    return (
        <div className="found-user-card uk-card uk-card-default uk-card-body" style={{zIndex: 1, overflow: "hidden"}}>
            <div className="found-user-card__avatar-wrap">
                <div className="found-user-card__avatar-bg" style={avatarBgStyles}></div>

                <Link to={`/profile/${props.userId}`}>
                    <img style={{borderRadius: '50%', border: "3px solid white", marginBottom: "3rem"}}
                         src={Utils.getImageSrc(props.avatarPath)}
                         sizes="(min-width: 120px) 120px, 100vw" width="120" height="120" alt="" />
                </Link>
            </div>

            <div className="found-user-card__name-wrap">
                <Link to={`/profile/${props.userId}`}>
                    <h3 className="uk-card-title found-user-card__user-name">
                        {props.name} {props.surname}
                    </h3>
                </Link>
                <div className="found-user-card__controls">
                    {renderControls()}
                </div>
            </div>
        </div>
    );
}

export default FoundUserCard;
