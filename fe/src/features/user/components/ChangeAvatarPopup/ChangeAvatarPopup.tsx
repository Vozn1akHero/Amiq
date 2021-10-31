import CenteredPopup from 'common/components/CenteredPopup/CenteredPopup';
import React from 'react';

const ChangeAvatarPopup = () => {
    return (
        <CenteredPopup id="change-avatar-popup"
                       title="Zmień zdjęcie"
                       controlsVisible={true}>
            <div className="change-avatar-popup">

            </div>
        </CenteredPopup>
    );
};

export default ChangeAvatarPopup;
