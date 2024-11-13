import reactDOM from 'react-dom/client';
import React from 'react';
import Navbar from './navbar.jsx';
import Footer from './footer.jsx';
import BASE_URL from '../main.js';

const Login = () => {
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
                return JSON.parse(text); // Parse the text to JSON if it's not empty
            }
            return {}; // Return an empty object if the response is empty
        })
            .catch(error => {
                console.error('Error:', error);
                alert('Error logging in');
            });
        }

    return (
        <>
            <div className='container'>
                <form className='container'>
                    <div>
                        <label id="username" onChange={(e) => setUsername(e.target.value)}>Username</label>
                        <input htmlFor="username" onChange={(e) => setUsername(e.target.value)}></input>
                    </div>
                    <div>
                        <label id="password" onChange={(e) => setPassword(e.target.value)}>Password</label>
                        <input htmlFor="password" onChange={(e) => setPassword(e.target.value)}></input>
                    </div>
                    <button type="submit" className='login' onClick={handleLogin}>Login</button>
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