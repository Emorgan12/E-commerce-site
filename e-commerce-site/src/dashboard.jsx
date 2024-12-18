import reactDOM from 'react-dom/client';
import React, { useContext, useEffect, useState } from 'react';
import Navbar from './navbar.jsx';
import Footer from './footer.jsx';
import BASE_URL from '../main.js';
import {useNavigate, Link } from 'react-router-dom';
import { AuthContext } from './authcontext.jsx';
import { DashboardLayout } from '@toolpad/core/DashboardLayout';
import { AppProvider} from '@toolpad/core/AppProvider'

function Dashboard(){
    const {user} = useContext(AuthContext);

    return(
        <>
            <Navbar/>
            <AppProvider>
                <DashboardLayout>
                    <Footer/>
                </DashboardLayout>
            </AppProvider>
        </>
    )
}

export default Dashboard;