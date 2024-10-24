import React from "react";
import reactDOM from "react-dom";
import { useState, useEffect } from "react";

function Cart(){
    return(
        <>
            <nav>
                <a href="../index.html"><img id="home" src="https://cdn-icons-png.flaticon.com/128/12891/12891793.png" /></a>
                <ul>
                    <li><b>BRAND NAME</b></li>
                    <li>Clothes</li>
                    <li>Toys</li>
                    <li>Baby</li>
                    <li>Electronics</li>
                    <li>Home</li>
                </ul>
                <a href="../cart.html"><img id="cart" src="https://cdn-icons-png.flaticon.com/128/12599/12599129.png" /></a>
            </nav>
            <div className="content">
                <div className="item">
                    <img src="https://cdn-icons-png.flaticon.com/128/649/649730.png"></img>
                    <p>Name</p>
                    <p>Shipping Cost</p>
                </div>
            </div>
        </>
    )
}

reactDOM.render(<Cart />, document.getElementById("app"))