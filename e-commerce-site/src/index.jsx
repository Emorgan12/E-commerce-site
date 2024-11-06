import reactDOM from 'react-dom/client';
import React from 'react';
import { Auth0Provider } from '@auth0/auth0-react';
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Navbar from './navbar.jsx';
import Footer from './footer.jsx';

const Login = () => {
    //const {isAutheticated} = useAuth0();
    console.log("Loading login page");
    return (
        <>
            <form className='container'>
                <div>
                    <label id="username">Username</label>
                    <input HtmlFor="username"></input>
                </div>
                <div>
                    <label id="password">Password</label>
                    <input HtmlFor="password"></input>
                </div>
                <button type="submit">Login</button>
            </form>
        </>
    )
}

const root = reactDOM.createRoot(document.getElementById('app'))
root.render(
    <React.StrictMode>
        <Login />
    </React.StrictMode>
);