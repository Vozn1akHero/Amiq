import {IValidationRule} from "./IValidationRule";
import {ValidationRuleResult} from "./validation-rule-result";

export class NotEmptyValidationRule implements IValidationRule {
    validate(value: string):ValidationRuleResult {
        const output : ValidationRuleResult = new ValidationRuleResult();
        if(!value){
            output.content = this.content;
            output.result = false;
        }
        output.result = true;
        return output;
    }

    content: string = "Nie może być pusty";
}
