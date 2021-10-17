import React, {Component} from 'react';
import CenteredPopup from "common/components/CenteredPopup/CenteredPopup";
import {ErrorMessage, Field, Form, Formik} from "formik";
import {GroupCreationPopupValidationSchema} from "../group-validation-schema";
import CategoryInput from "../CategoryInput/CategoryInput";


const GroupCreationPopup  = () => {
    return (
        <CenteredPopup id="new-group-popup"
                       title="Utwórz grupę"
                       controlsVisible={true}>
            <div className="group-creation-form">
                <Formik
                    initialValues={{name: '', description: ''}}
                    onSubmit={(values, {setSubmitting}) => {

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
                            <button className="uk-button uk-button-default uk-modal-close uk-margin-small-top">
                                Anuluj
                            </button>
                            <button type="submit"
                                    className="uk-button-primary uk-button uk-margin-small-top uk-margin-small-left"
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