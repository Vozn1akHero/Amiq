import React, {useState} from 'react';
import {ErrorMessage, Field, Form, Formik} from "formik";
import {DatePickerFormicField} from "../../common/components/DatePickerFormicField/DatePickerFormicField";

const BasicUserSettingsSubpage = () => {
    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
    const [description, setDescription] = useState("");
    const [birthdate, setBirthdate] = useState(new Date(2021, 11, 2));
    const [sex, setSex] = useState("M");

    return (
        <div className="">
            <Formik
                initialValues={{name, surname, description, birthdate, sex}}
                onSubmit={(values, {setSubmitting}) => {
                    setSubmitting(true);
                    console.log(values)
                }}
            >
                {({isSubmitting, isValid}) => (
                    <Form>
                        <div className="user-settings-page__inputs-wrap">
                            <div className="uk-flex">
                                <Field name="name" className="uk-input" placeholder="Imię"/>
                                <ErrorMessage name="name" component="div"/>
                                <Field name="surname" className="uk-input uk-margin-left" placeholder="Nazwisko"/>
                                <ErrorMessage name="surname" component="div"/>
                            </div>

                            <div className="uk-margin-top">
                                <Field as="textarea" name="description" placeholder="Opis"
                                       minLength={0}
                                       className="uk-textarea uk-margin-small-top" rows={3}/>
                            </div>

                            <div className="uk-margin-top">
                                <DatePickerFormicField className="uk-input"
                                                       placeholder="Data urodzenia"
                                                       name="birthdate" />
                                <ErrorMessage name="birthdate" component="div"/>
                            </div>

                            <div className="uk-margin-top uk-flex">
                                <div id="sex-radio-group">Płeć</div>
                                <div role="group" className="uk-margin-left" aria-labelledby="sex-radio-group">
                                    <label>
                                        <Field type="radio" name="sex" value="M" />
                                        M
                                    </label>
                                    <label className="uk-margin-small-left">
                                        <Field type="radio" name="sex" value="F" />
                                        K
                                    </label>
                                </div>
                            </div>

                        </div>
                        <div className="uk-margin-medium-top">
                            <button type="submit"
                                    disabled={isSubmitting || !isValid}
                                    className="uk-button uk-button-primary">
                                Akceptuj
                            </button>
                        </div>
                    </Form>)
                }
            </Formik>
        </div>
    );
};

export default BasicUserSettingsSubpage;
