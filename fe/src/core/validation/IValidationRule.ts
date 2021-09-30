import {ValidationRuleResult} from "./validation-rule-result";

export interface IValidationRule {
    content: string;
    validate(value:string):ValidationRuleResult;
}
