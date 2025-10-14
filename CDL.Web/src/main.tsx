import ReactDOM from "react-dom/client";
import {
    BrowserRouter, Routes, Route,
} from "react-router";

import './index.css'
import App from './App.tsx'

ReactDOM.createRoot(document.getElementById('root')!).render(
    <div className="w-screen h-screen">
        <BrowserRouter>
            <App />
        </BrowserRouter>
    </div>
);

//createRoot(document.getElementById('root')!).render(
//    <StrictMode>
//        <App />
//    </StrictMode>,
//)
