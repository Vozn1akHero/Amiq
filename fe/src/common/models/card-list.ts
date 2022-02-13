import {Component} from "react";

export abstract class CardList<T> extends Component {
    elements: Array<T>;

    isEmpty(){
        return this.elements.length === 0;
    }
}