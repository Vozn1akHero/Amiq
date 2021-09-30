import React, {Component} from 'react';
import {ErrorMessage, Field, Form, Formik} from "formik";
import {DtoRegister} from "../../core/auth/dto-register";

type Props = {
    register(dto: DtoRegister):void;
}

class JoinupPage extends Component<Props,any> {
    render() {
        return (
            <div className="joinup-page">
                <Formik
                    initialValues={{ login: '', password: '' }}
                    onSubmit={(values, { setSubmitting }) => {
                        setTimeout(() => {
                            const dto = new DtoRegister();
                            dto.email = values["email"];
                            dto.surname = values["surname"]
                            dto.login  = values["login"]
                            dto.name = values["name"]
                            dto.birthdate = values["birthdate"]
                            dto.password = values["password"]
                            this.props.register(dto);
                            setSubmitting(false);
                        }, 400);
                    }}
                >
                    {({ isSubmitting }) => (
                        <Form>
                            <Field name="name" className="uk-input uk-margin-small-top" placeholder="Imię" />
                            <Field name="surname" className="uk-input uk-margin-small-top" placeholder="Nazwisko" />
                            <Field name="email" type="email" className="uk-input uk-margin-small-top" placeholder="Email" />
                            <ErrorMessage name="email" component="div" />
                            <Field name="login" className="uk-input uk-margin-small-top" placeholder="Login" autoComplete="new-login" />
                            <ErrorMessage name="login" component="div" />
                            <Field type="password" name="password" className="uk-input uk-margin-small-top" placeholder="Hasło" autoComplete="new-password"  />
                            <ErrorMessage name="password" component="div" />
                            <button type="submit" className="uk-button-primary uk-button uk-margin-small-top" disabled={isSubmitting}>
                                Dołącz
                            </button>
                        </Form>
                    )}
                </Formik>
            </div>
        );
    }
}

export default JoinupPage;