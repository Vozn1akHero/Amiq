import { ValidatorBase } from "./validator-base";
import {ValidationResult} from "./validation-result";
import {IValidationRule} from "./IValidationRule";

export class EmailValidator extends ValidatorBase<string> {
    private readonly _emailRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

    recheck(value: string, validationRules? : Array<IValidationRule>): ValidationResult {
        const output = new ValidationResult();
        const res = this._emailRegex.test(value)
        output.result = res;
        return output;
    }

}
