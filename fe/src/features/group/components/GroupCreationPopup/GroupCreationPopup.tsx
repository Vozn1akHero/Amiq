import React from 'react';
import CenteredPopup from "common/components/CenteredPopup/CenteredPopup";
import {ErrorMessage, Field, Form, Formik} from "formik";
import {GroupCreationPopupValidationSchema} from "../group-validation-schema";
import CategoryInput from "../CategoryInput/CategoryInput";
import {IGroupCard} from "../../models/group-models";

type Props = {
    createGroup(groupData: Pick<IGroupCard, 'name' & 'description'>): void;
}

const GroupCreationPopup = (props: Props) => {
    return (
        <CenteredPopup id="new-group-popup"
                       title="Utwórz grupę"
                       controlsVisible={true}>
            <div className="group-creation-form">
                <Formik
                    initialValues={{name: '', description: ''}}
                    onSubmit={(values, {setSubmitting}) => {
                        setSubmitting(true)
                        props.createGroup({
                            name: values.name,
                            description: values.description
                        });
                        setSubmitting(false)
                    }}
                    validationSchema={GroupCreationPopupValidationSchema}
                >
                    {({isSubmitting, errors, handleSubmit}) => (
                        <Form onSubmit={handleSubmit}>
                            <Field name="name" className="uk-input" placeholder="Nazwa"/>
                            <ErrorMessage name="name" component="div"/>
                            <Field as="textarea" name="description" placeholder="Opis"
                                   className="uk-textarea uk-margin-small-top" rows={3}/>
                            <ErrorMessage name="description" component="div"/>
                            <div className="uk-margin-small-top">
                                <CategoryInput/>
                            </div>
                            <button type="submit"
                                    className="uk-button-primary uk-button uk-margin-small-top"
                                    disabled={isSubmitting}>
                                Utwórz
                            </button>
                        </Form>
                    )}
                </Formik>
            </div>
        </CenteredPopup>
    );
}

export default GroupCreationPopup;