import React from 'react';
import BasePage from "../base-page";
import { Formik, Form, Field, ErrorMessage } from 'formik';

type Props = {
    authenticate(login: string, password: string): void
}


class AuthPage extends BasePage<Props, any> {
    render() {
        return (
            <div className="auth-page">
                <Formik
                    initialValues={{ login: '', password: '' }}
                    onSubmit={(values, { setSubmitting }) => {
                        setTimeout(() => {
                            this.props.authenticate(values["login"], values["password"]);
                            setSubmitting(false);
                        }, 400);
                    }}
                >
                    {({ isSubmitting }) => (
                        <Form>
                            <Field name="login" />
                            <ErrorMessage name="login" component="div" />
                            <Field type="password" name="password" />
                            <ErrorMessage name="password" component="div" />
                            <button type="submit" disabled={isSubmitting}>
                                Submit
                            </button>
                        </Form>
                    )}
                </Formik>
            </div>
        );
    }
}

export default AuthPage;