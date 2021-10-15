import React, {Component} from 'react';
import CenteredPopup from "common/components/CenteredPopup/CenteredPopup";
import {ErrorMessage, Field, Form, Formik} from "formik";
import * as Yup from 'yup';


const GroupCreationPopup  = () => {
    const GroupCreationPopupValidationSchema = Yup.object().shape({
        name: Yup.string()
            .max(100, "100 znaków")
            .required("Pole nie może być puste"),
        description: Yup.string()
            .max(100, "100 znaków")
            .required("Pole nie może być puste"),
    });

    return (
        <CenteredPopup id="new-group-popup"
                       title="Utwórz grupę"
                       controlsVisible={true}>
            <div className="group-creation-form">
                <Formik
                    initialValues={{name: '', description: '', category: ''}}
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
                            <Field name="category" placeholder="Kategorie" className="uk-input uk-margin-small-top"/>
                            <ErrorMessage name="category" component="div"/>
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