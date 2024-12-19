import reactDOM from 'react-dom/client';
import React, { useContext, useEffect, useState } from 'react';
import Navbar from './navbar.jsx';
import Footer from './footer.jsx';
import BASE_URL from '../main.js';
import {useNavigate, Link } from 'react-router-dom';
import { AuthContext } from './authcontext.jsx';


function Dashboard(){
    const {user} = useContext(AuthContext);

    return(
        <>
        </>
    )
}

export default Dashboard;