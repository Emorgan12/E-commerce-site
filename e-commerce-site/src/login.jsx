import reactDOM from 'react-dom/client';
import React, { useContext } from 'react';
import Navbar from './navbar.jsx';
import Footer from './footer.jsx';
import BASE_URL from '../main.js';
import {useNavigate, Link } from 'react-router-dom';
import { AuthContext } from './authcontext.jsx';

const Login = () => {

    const navigate = useNavigate();
    const{ login } = useContext(AuthContext);

    const [username, setUsername] = React.useState('');
    const [password, setPassword] = React.useState('');
    const loginUrl = `${BASE_URL}login/${encodeURIComponent(username)},${encodeURIComponent(password)}`;

    console.log("Loading login page");
    const handleLogin = (event) => {
        event.preventDefault();
        fetch(loginUrl, {
            method: 'GET'
        })
            .then(response => {if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.text(); // Use text() instead of json() to handle empty responses
        })
        .then(text => {
            if (text) {
                const userData = JSON.parse(text);
                login(userData);
                navigate('/products');
                return user;
            }
            return {}; // Return an empty object if the response is empty
        })
    }

    return (
        <>
            <Navbar />

            <div className='content'>
                <div className='container'>
                    <form className='container'>
                        <div>
                            <label id="username" onChange={(e) => setUsername(e.target.value)}>Username</label>
                            <input htmlFor="username" onChange={(e) => setUsername(e.target.value)}></input>
                        </div>
                        <div>
                            <label id="password" onChange={(e) => setPassword(e.target.value)}>Password</label>
                            <input type="password" htmlFor="password" onChange={(e) => setPassword(e.target.value)}></input>
                        </div>
                        <button type="submit" className='login' onClick={handleLogin}>Login</button>
                        <p>Don't have an account? <Link to="/register">Register</Link></p>
                    </form>
                </div>
            </div>

            <Footer />
        </>
    )
}


export default Login;