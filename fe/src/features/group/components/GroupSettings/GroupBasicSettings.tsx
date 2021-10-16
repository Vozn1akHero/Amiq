import React, {Component, useEffect, useState} from 'react';
import {IGroupData} from "../../models/group-models";

type Props = {
    groupData: IGroupData;
}

const GroupBasicSettings = (props:Props) => {
    let groupDataState = props.groupData;

    //const [areControlsAvailable, setAreControlsAvailable] = useState(false)

    const [name, setName] = useState(props.groupData.name);
    const [description, setDescription] = useState(props.groupData.description);

    /*useEffect(() => {
    }, [name, description])*/

    const onAcceptClick = () => {

    }

    const onResetClick = () => {
        setName(props.groupData.name);
        setDescription(props.groupData.description);
    }

    return (
        <div className="group-basic-settings">
            <input minLength={1}
                   value={name}
                   onChange={e => setName(e.target.value)}
                   className="uk-input" placeholder="Nazwa" />
            <textarea className="uk-textarea post-creation-form__textarea uk-margin-top"
                      value={description}
                      onChange={e => setDescription(e.target.value)}
                      rows={3}
                      placeholder="Opis" />
            <div className="uk-margin-top">
                <button className="uk-button uk-button-default" onClick={onResetClick}>
                    Anuluj
                </button>
                <button className="uk-button uk-button-primary uk-margin-small-left" onClick={onAcceptClick}>
                    Akceptuj
                </button>
            </div>
        </div>
    );
};

export default GroupBasicSettings;
