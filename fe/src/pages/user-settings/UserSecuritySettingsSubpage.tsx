import React, {useState} from 'react';
import {ErrorMessage, Field, Form, Formik} from "formik";
import AuthService from "../../core/auth/auth-service";

type Props = {
    email: string;
}

const UserSecuritySettingsSubpage = (props:Props) => {
    const [email, setEmail] = useState(props.email);
    const [newPassword, setNewPassword] = useState("");
    const [oldPassword, setOldPassword] = useState("");

    const authService : AuthService = new AuthService();

    const changeEmail = () => {
        authService.changeEmail(email).then(res => {
            console.log(res)
        })
    }

    const changePassword = () => {
        authService.changePassword(oldPassword, newPassword).then(res => {
            console.log(res)
        })
    }

    return (
        <div>
            <Formik
                initialValues={{}}
                onSubmit={(values, {setSubmitting}) => {
                    setSubmitting(true);
                    changePassword();
                    console.log(values)
                }}
            >
                {({isSubmitting, isValid}) => (
                    <Form>
                        <h3>Zmień email</h3>
                        <div className="user-settings-page__inputs-wrap">
                            <div className="uk-margin-top">
                                <Field name="email" className="uk-input"
                                       autoComplete="off"
                                       placeholder="Email" />
                                <ErrorMessage name="email" component="div" />
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
            <hr />
            <Formik
                initialValues={{}}
                onSubmit={(values, {setSubmitting}) => {
                    setSubmitting(true);
                    changeEmail();
                    console.log(values)
                }}
            >
                {({isSubmitting, isValid}) => (
                    <Form>
                        <h3>Zmień hasło</h3>
                        <div className="user-settings-page__inputs-wrap">
                            <div className="uk-margin-top uk-flex">
                                <Field type="password" name="oldPassword" className="uk-input"
                                       placeholder="Stare hasło" autoComplete="off" />
                                <ErrorMessage name="oldPassword" component="div" />
                                <Field type="password" name="newPassword" className="uk-input uk-margin-left"
                                       autoComplete="off"
                                       placeholder="Nowe hasło" />
                                <ErrorMessage name="newPassword" component="div"  />
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

export default UserSecuritySettingsSubpage;
