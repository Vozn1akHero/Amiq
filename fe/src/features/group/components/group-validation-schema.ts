import * as Yup from "yup";

export const GroupCreationPopupValidationSchema = Yup.object().shape({
    name: Yup.string()
        .max(100, "100 znaków")
        .required("Pole nie może być puste"),
    description: Yup.string()
        .max(500, "500 znaków")
        //.required("Pole nie może być puste"),
});