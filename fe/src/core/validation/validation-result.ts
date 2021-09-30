import {IValidationRule} from "./IValidationRule";

export class ValidationResult {
    result: boolean;
    validationRules: Array<IValidationRule>
}
