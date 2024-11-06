import React from "react";
import reactDOM from "react-dom";
import Footer from "./footer";
import BASE_URL from "../main";
import { useState, useEffect } from "react";

function Register(){

    const [email, setEmail] = useState('');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    function CreateAccount(event)
    {
        event.preventDefault();

        // Here you would typically send the username and password to your server
        // For example, using fetch:
        fetch(BASE_URL + encodeURIComponent('newAccount'), {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, username, password })
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert('Account created successfully!');
            } else {
                alert('Error creating account: ' + data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Error creating account');
        });
    }

    return(
        <>
            <div className="container">
                <form className='container'>
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
                    <button type="submit" className='register' onClick={CreateAccount}>Register</button>
                    <p>Already have an account? <a href="/">Login</a></p>
                </form>
            </div>

            <Footer/>
        </>

    )
}

reactDOM.render(<Register/>, document.getElementById("app"))