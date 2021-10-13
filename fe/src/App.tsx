import './App.scss';
import {BrowserRouter} from "react-router-dom";
import IdentityWrapperPageContainer from "./pages/identity-wrapper/IdentityWrapperPageContainer";
import React, {Component} from 'react';
import { Provider } from 'react-redux';
import store from "./store/redux/store";

class App extends Component {
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
