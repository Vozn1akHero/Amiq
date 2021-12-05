import CenteredPopup from 'common/components/CenteredPopup/CenteredPopup';
import React, {useEffect, useState} from 'react';
import PageAvatar from "../../../../common/components/PageAvatar/PageAvatar";
import {Utils} from "../../../../core/utils";

type Props = {
    avatarSrc: string;
    pageTitle: string;
    onSubmit(file:File):void;
}

const ChangeAvatarPopup = (props: Props) => {
    const [currentAvatarSrc, setCurrentAvatarSrc] = useState(null);
    const [chosenFile, setChosenFile] = useState<File>(null);

    const imgInputRef = React.createRef<HTMLInputElement>()

    useEffect(() => {
        setCurrentAvatarSrc(Utils.getImageSrc(props.avatarSrc));
    }, []);

    const onFileUpload = (e) => {
        e.preventDefault();
        if(e.target.files){
            const file: File = e.target.files[0];
            setChosenFile(file);
            setCurrentAvatarSrc(URL.createObjectURL(file));
        }
    }

    const onResetClick = () => {
        setCurrentAvatarSrc(Utils.getImageSrc(props.avatarSrc));
        imgInputRef.current.files = new DataTransfer().files;
    }

    const submitAvatarChange = () => {
        props.onSubmit(chosenFile);
    }

    return (
        <CenteredPopup id="change-avatar-popup"
                       title="Zmień zdjęcie"
                       controlsVisible={true}>
            <div className="change-avatar-popup">
                <PageAvatar viewTitle={props.pageTitle}
                            avatarSrc={currentAvatarSrc}/>
                <input type="file"
                       ref={imgInputRef}
                       className="uk-margin-top"
                       accept="image/png, image/jpeg"
                       onChange={onFileUpload}/>

                <div className="uk-margin-medium-top">
                    <button className="uk-button uk-button-default"
                            onClick={onResetClick}>
                        Anuluj
                    </button>
                    <button disabled={!chosenFile}
                            onClick={submitAvatarChange}
                            className="uk-button uk-button-primary uk-margin-small-left">
                        Akceptuj
                    </button>
                </div>
            </div>
        </CenteredPopup>
    );
};

export default ChangeAvatarPopup;
