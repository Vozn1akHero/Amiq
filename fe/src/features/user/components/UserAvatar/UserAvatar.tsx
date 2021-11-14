import React, {useEffect, useState} from 'react';
import PageAvatar from "common/components/PageAvatar/PageAvatar";
import {IUser} from "../../models/user";

type Props = {
    userData: IUser;
    isViewerProfile: boolean;
    removeFriend(friendId: number): void;
    acceptFriendRequest(destUserId: number): void;
    sendFriendRequest(destUserId: number): void;
    rejectFriendRequest(destUserId: number): void;
    cancelFriendRequest(destUserId: number): void;
    blockUser(destUserId: number): void;
}

const UserAvatar = (props: Props) => {
    const [controls, setControls] = useState(null);

    useEffect(() => {
        setControls(controlsFactory);
    }, [JSON.stringify([props.userData.blockedByIssuer,
        props.userData.issuerBlocked,
        props.userData.issuerReceivedFriendRequest,
        props.userData.issuerSentFriendRequest,
        props.userData.isIssuerFriend])])

    const controlsFactory = () => {
        const {
            userId,
            blockedByIssuer,
            issuerBlocked,
            issuerReceivedFriendRequest,
            issuerSentFriendRequest,
            isIssuerFriend
        } = props.userData;

        if (!props.isViewerProfile) {
            if (!issuerBlocked && !blockedByIssuer && !issuerReceivedFriendRequest && !issuerSentFriendRequest && !isIssuerFriend) {
                return <>
                    <button onClick={() => props.sendFriendRequest(userId)}
                            className="uk-button uk-button-primary page-avatar__control">Dodaj
                    </button>
                    <button onClick={() => props.blockUser(userId)}
                            className="uk-button uk-button-secondary uk-margin-small-left page-avatar__control">Zablokuj
                    </button>
                </>
            } else if (issuerReceivedFriendRequest) {
                return <>
                    <button onClick={() => props.acceptFriendRequest(userId)}
                            className="uk-button uk-button-primary">Zatwierdź
                    </button>
                    <button onClick={() => props.rejectFriendRequest(userId)}
                            className="uk-button uk-button-danger uk-margin-small-left">Odrzuć
                    </button>
                </>
            } else if (issuerSentFriendRequest) {
                return <button onClick={() => props.cancelFriendRequest(userId)}
                               className="uk-button uk-button-secondary">Anuluj</button>
            } else if (isIssuerFriend) {
                return <button onClick={() => props.removeFriend(userId)}
                               className="uk-button uk-button-danger uk-margin-small-left">Usuń
                </button>
            }
        }
    }

    return (
        <PageAvatar avatarSrc={props.userData.avatarPath}
                    isChangeAvatarBtnVisible={props.isViewerProfile}
                    userSpecifics={props.userData}
                    viewTitle={props.userData.name + " " + props.userData.surname}>
            {controls}
        </PageAvatar>
    );
};

export default UserAvatar;
