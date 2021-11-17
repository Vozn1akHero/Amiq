import React from "react";
import {BehaviorSubject} from "rxjs";

export class ModalService {
    private static _component: BehaviorSubject<React.ReactElement> = new BehaviorSubject<React.ReactElement>(null);
    private static _isOpen: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

    static get isOpen$() {
        return this._isOpen.asObservable();
    }

    static get component$(){
        return this._component.asObservable();
    }

    static open(component: React.ReactElement){
        ModalService._component.next(component);
        ModalService._isOpen.next(true);
    }

    static close(){
        ModalService._isOpen.next(false);
        ModalService._component.next(null);
    }
}