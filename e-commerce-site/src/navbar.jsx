import React from 'react';
import { Link } from 'react-router-dom';


function Navbar(){
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
                <li><b>TRADEZY</b></li>
                <li>Clothes</li>
                <li>Toys</li>
                <li>Baby</li>
                <li>Electronics</li>
                <li>Home</li>
                <li id="cart" ><Link to="/cart">< img id="cart-img" src="https://cdn-icons-png.flaticon.com/128/12599/12599129.png" /></Link></li>
            </ul>
        </nav>
    )
}

export default Navbar;