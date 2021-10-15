import React, {Component} from 'react';
import {IGroupData} from "../../group-models";

type Props = {
    groupData: IGroupData;
}

const GroupBasicSettings = (props:Props) => {
    return (
        <div className="group-basic-settings">
            <input minLength={1} defaultValue={props.groupData.name} className="uk-input" placeholder="Nazwa" />
            <textarea className="uk-textarea post-creation-form__textarea uk-margin-top"
                      defaultValue={props.groupData.description}
                      rows={3}
                      placeholder="Opis" />
        </div>
    );
};

export default GroupBasicSettings;
