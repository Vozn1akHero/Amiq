import './App.scss';
import {BrowserRouter} from "react-router-dom";
import IdentityWrapperPageContainer from "./pages/identity-wrapper/IdentityWrapperPageContainer";
import React, {Component} from 'react';
import { Provider } from 'react-redux';
import store from "./store/redux/store";
import moment from "moment";
import 'moment/locale/pl'

class App extends Component {
    constructor(props) {
        super(props);

        moment.locale('pl');
    }

    render() {
        return (
            <Provider store={store}>
                <BrowserRouter>
                    <div id="amiqApp" className="amiq-app">
                        <IdentityWrapperPageContainer />
                    </div>
                </BrowserRouter>

            </Provider>
        );
    }
}

export default App;
