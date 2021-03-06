import React from 'react';
import ReactDOM from 'react-dom';
import './index.scss';
import App from './App';
import reportWebVitals from './reportWebVitals';

/*import "uikit/dist/js/uikit.js";
import "uikit/dist/js/uikit-icons.js";
//import "uikit/dist/js/uikit.min.js";
import "uikit/dist/css/uikit.min.css";
import "uikit/dist/css/uikit-core.min.css";*/

import "reflect-metadata";
import moment from "moment";

moment.locale('pl')

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
