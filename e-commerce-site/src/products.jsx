import React from "react";
import reactDOM from "react-dom";
import { useState, useEffect } from "react";
import BASE_URL from "../main.js";
import Navbar from "./navbar.jsx";
import Product from "./product.jsx";
import Footer from "./footer.jsx";

function LandingPage() {

    const [products, setProducts] = useState([]);
    const [error, setError] = useState('')
    const [index, setIndex] = useState(0)

    useEffect(() => {
        fetch(BASE_URL + encodeURIComponent('get'))
        .then((response) => {
            console.log('Received response:', response);
            if (!response.ok) {
                throw new Error(`Network response was not ok: ${response.statusText}`);
            }
            return response.json();
        })
        .then((data) => {
            console.log('Success:', data);
            setProducts(data);
        })
        .catch((error) => {
            console.error('Error:', error);
            setError(error.message);
        });
}, []);

    return (
        <>
        <Navbar />

        <div className="content">
            <div className="row">
                {screen.availWidth > 750 && products.slice(index, index + 10).map((product) => (
                    <Product />
                ))}
                {screen.availWidth <= 750  && screen.availWidth > 420 && products.slice(index, index + 9).map((product) => (
                    <Product />
                ))}
                {screen.availWidth <= 420 && products.slice(index, index + 8).map((product) => (
                    <Product />
                    ))}
            </div>
                {
                    index > 0 && 
                    <button onClick={() => {if(screen.availWidth > 750) setIndex(index - 10); else if(screen.availWidth > 420) setIndex(index-9); else setIndex(index-8); }}>Previous page</button>
                }
                {
                    index + 10 < products.length &&
                        <button onClick={() => {if(screen.availWidth > 750) setIndex(index + 10); else if(screen.availWidth > 420) setIndex(index+9); else setIndex(index+8); }}>Next page</button>
                }
        </div>
        <Footer />
        
        </>
    );
}

const root = reactDOM.createRoot(document.getElementById('app'))
root.render(
    <React.StrictMode>
        <LandingPage />
    </React.StrictMode>
);

export default LandingPage;