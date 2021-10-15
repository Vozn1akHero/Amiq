import {useField} from "formik";

export const FormikTextArea = ({...props}) => {
    const [field, meta] = useField(props as any);
    return (
        <>
            <textarea className="text-area" {...field} {...props} />
            {meta.touched && meta.error ? (
                <div className="error">{meta.error}</div>
            ) : null}
        </>
    );
};