import reactDOM from 'react-dom/client';
import React from 'react';
import Navbar from './navbar.jsx';
import Footer from './footer.jsx';

const Login = () => {

    console.log("Loading login page");
    return (
        <>
            <div className='container'>
                <form className='container'>
                    <div>
                        <label id="username">Username</label>
                        <input HtmlFor="username"></input>
                    </div>
                    <div>
                        <label id="password">Password</label>
                        <input HtmlFor="password"></input>
                    </div>
                    <button type="submit" className='login'>Login</button>
                    <p>Don't have an account? <a href="/register">Register</a></p>
                </form>
            </div>

            <Footer />
        </>
    )
}

const root = reactDOM.createRoot(document.getElementById('app'))
root.render(
    <React.StrictMode>
        <Login />
    </React.StrictMode>
);