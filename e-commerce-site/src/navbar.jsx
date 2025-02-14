import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { AuthContext } from "./authcontext";
import { useContext } from 'react';


function Navbar(){

    const navigate = useNavigate();
    const{ logout } = useContext(AuthContext);
    
    const LogOut = () => {
        logout()
        navigate('/')
    }

    return(
        <nav>
            <ul id="mobile">
                <li id="home"><Link to="/products"><img className="mobile-img" src="https://cdn-icons-png.flaticon.com/128/12891/12891793.png" /></Link></li>
                <li className="container">
                    <p><b>TRADEZY</b></p>
                </li>
                <li id="cart" ><Link to="/cart">< img id="cart-img" className="mobile-img" src="https://cdn-icons-png.flaticon.com/128/12599/12599129.png" /></Link></li>
            </ul>
            <ul id="big-scrn">
                <li id="home"><Link to="/products"><img src="https://cdn-icons-png.flaticon.com/128/12891/12891793.png" /></Link></li>
                <li id="cart" ><Link to="/cart">< img id="cart-img" src="https://cdn-icons-png.flaticon.com/128/12599/12599129.png" /></Link></li>
                <li><b>TRADEZY</b></li>
                <li><button id="invis-btn" onClick={() => navigate('/dashboard')}><img id='pfp' src="https://i.postimg.cc/9fh73TTc/image-1.png"/></button></li>
                <li><button id="nav-btn" onClick={() => LogOut()}>Log Out</button></li>
            </ul>
        </nav>
    )
}

export default Navbar;