import React, {Component, useEffect, useState} from 'react';
import {IGroupData} from "../../models/group-models";
import {GroupService} from "../../services/group-service";
import {StatusCodes} from "http-status-codes";
import {GroupCreationPopupValidationSchema} from "../group-validation-schema";
import {ErrorMessage, Field, Form, Formik} from "formik";
import * as UIkit from "uikit"
import CategoryInput from "../CategoryInput/CategoryInput";

type Props = {
    groupData: IGroupData;
}

const GroupBasicSettings = (props:Props) => {
    //const [areControlsAvailable, setAreControlsAvailable] = useState(false)

    const [name, setName] = useState(props.groupData.name);
    const [description, setDescription] = useState(props.groupData.description);

    const groupService : GroupService = new GroupService();

    /*useEffect(() => {
    }, [name, description])*/

    const onResetClick = () => {
        setName(props.groupData.name);
        setDescription(props.groupData.description);
    }

    return (
        <div className="group-basic-settings">
            <Formik
                initialValues={{name, description}}
                onSubmit={(values, {setSubmitting}) => {
                    setSubmitting(true);
                    const dto: Partial<IGroupData> = {
                        name, description
                    };
                    groupService.edit(dto).then(res => {
                        if(res.status === StatusCodes.OK){
                            UIkit.notification({message: 'Zapisano', pos: 'bottom-right'})
                        }
                    }).finally(() => {
                        setSubmitting(false);
                    })
                }}
                validationSchema={GroupCreationPopupValidationSchema}
            >
                {({isSubmitting, errors}) => (
                    <Form>
                        <Field name="name" className="uk-input" placeholder="Nazwa"/>
                        <ErrorMessage name="name" component="div"/>
                        <Field as="textarea" name="description" placeholder="Opis"
                               className="uk-textarea uk-margin-small-top" rows={3}/>
                        <ErrorMessage name="description" component="div"/>
                        <div className="uk-margin-small-top">
                            <CategoryInput />
                        </div>
                        <div className="uk-margin-top">
                            <button className="uk-button uk-button-default"
                                    disabled={isSubmitting}
                                    onClick={onResetClick}>
                                Anuluj
                            </button>
                            <button type="submit"
                                    disabled={isSubmitting}
                                    className="uk-button uk-button-primary uk-margin-small-left">
                                Akceptuj
                            </button>
                        </div>
                    </Form>
                )}
            </Formik>
        </div>
    );
};

export default GroupBasicSettings;
