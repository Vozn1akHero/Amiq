import './App.scss';
import {BrowserRouter} from "react-router-dom";
import IdentityWrapperPageContainer from "./pages/identity-wrapper/IdentityWrapperPageContainer";
import React, {Component} from 'react';
import {Provider} from 'react-redux';
import store from "./store/redux/store";
import moment from "moment";
import 'moment/locale/pl'
import {ModalService} from "./core/modal-service";
import {Subscription} from "rxjs";

type State = {
    modal: React.ReactElement;
    isModalOpen: boolean;
}

class App extends Component<any, State> {
    isOpenSub: Subscription;
    modalComponentSub: Subscription;

    constructor(props) {
        super(props);
        this.state = {
            modal: <></>,
            isModalOpen: false
        };
        moment.locale('pl');
    }

    componentDidMount() {
        this.subscribeToModalService();
    }

    subscribeToModalService() {
        this.isOpenSub = ModalService.isOpen$.subscribe(isModalOpen => {
            this.setState({
                isModalOpen
            })
        })
        this.modalComponentSub = ModalService.component$.subscribe(component => {
            this.setState({
                modal: component == null ? <></> : component
            })
        })
    }

    componentWillUnmount() {
        this.isOpenSub.unsubscribe();
        this.modalComponentSub.unsubscribe();
    }

    render() {
        return (
            <Provider store={store}>
                <BrowserRouter>
                    <div id="amiqApp" className="amiq-app">
                        {this.state.isModalOpen && this.state.modal}
                        <IdentityWrapperPageContainer/>
                    </div>
                </BrowserRouter>

            </Provider>
        );
    }
}

export default App;
