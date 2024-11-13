import React from "react";
import reactDOM from "react-dom";
import { useState, useEffect } from "react";
import Navbar from "./navbar";
import CartItem from "./cart-item";
import Footer from "./footer";

function Cart(){
    return(
        <>
            <Navbar />

            <div className="content">
            <CartItem /><CartItem /><CartItem /><CartItem />
            </div>

            <Footer />
        </>
    )
}

reactDOM.render(<Cart />, document.getElementById("app"))