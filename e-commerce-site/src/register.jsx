import React from "react";
import reactDOM from "react-dom";
import Footer from "./footer";
import BASE_URL from "../main";
import { useState, useEffect } from "react";
import { BrowserRouter, Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";

function Register(){

    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const formatErrorMessage = (text) => {
        if (text.includes("System.ArgumentException")) {
            return "Username already exists";
        }
        return "Registration failed";
    };

    console.log("Loading register page");
    function CreateAccount(event)
    {
        event.preventDefault();
        fetch(BASE_URL + encodeURIComponent('newAccount'), {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, username, password })
        })
        .then(response => {
            if (!response.ok) {
                return response.text().then(text => {
                    throw new Error(formatErrorMessage(text));
                });
            } else {
                navigate('/')
                return response.text();
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert(error.message);
        });
    }

    return(
        <>
            <div className="container">
                <form>
                    <div>
                        <label id="email">Email</label><br />
                        <input htmlFor="email" type="email" onChange={(e) => setEmail(e.target.value)}></input>
                    </div>
                    <div>
                        <label id="username">Username</label><br />
                        <input htmlFor="username" onChange={e => setUsername(e.target.value)}></input>
                    </div>
                    <div>
                        <label id="password">Password</label><br />
                        <input htmlFor="password" type="password" onChange={(e) => setPassword(e.target.value)}></input>
                    </div>
                    <div className="container"> 
                        <button type="submit" className='register' onClick={CreateAccount}>Register</button>
                        <p>Already have an account? <Link to="/">Login</Link></p>
                    </div>
                </form>
            </div>

            <Footer/>
        </>

    )
}

export default Register;