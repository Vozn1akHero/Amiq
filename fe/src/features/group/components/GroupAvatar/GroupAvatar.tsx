import React, {useEffect, useState} from 'react';
import PageAvatar from "../../../../common/components/PageAvatar/PageAvatar";
import {Utils} from "../../../../core/utils";

type Props = {
    avatarSrc: string;
    groupName: string;
    isChangeAvatarBtnVisible: boolean;
    openChangeAvatarPopup():void;
}

const GroupAvatar = (props:Props) => {
    const [controls, setControls] = useState(null);

    useEffect(() => {
        setControls(controlsFactory);
    }, [])

    const controlsFactory = () => {

    }

    return (
        <PageAvatar avatarSrc={Utils.getImageSrc(props.avatarSrc)}
                    onChangeAvatarBtnClick={props.openChangeAvatarPopup}
                    isChangeAvatarBtnVisible={props.isChangeAvatarBtnVisible}
                    viewTitle={props.groupName}>
            {controls}
        </PageAvatar>
    );
};

export default GroupAvatar;
