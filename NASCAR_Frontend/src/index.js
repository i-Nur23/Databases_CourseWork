import React from 'react';
import { BrowserRouter } from 'react-router-dom'
import { createRoot } from 'react-dom/client';
import   App   from './App';
import 'bootstrap-icons/font/bootstrap-icons.css'
import './bootstrap.min.css';
import './customStyles.css';
import * as serviceWorkerRegistration from './serviceWorkerRegistration';
import reportWebVitals from './reportWebVitals';

const container = document.getElementById('root');
const root = createRoot(container);
root.render(
    <BrowserRouter>
        <App />
    </BrowserRouter>
);

serviceWorkerRegistration.unregister();

reportWebVitals();
