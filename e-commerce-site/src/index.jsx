import React from "react";
import reactDOM from "react-dom";
import { useState, useEffect } from "react";
import BASE_URL from "../main.js";

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
        <nav>
            <ul id="mobile">
                <li id="home"><a href="../index.html"><img className="mobile-img" src="https://cdn-icons-png.flaticon.com/128/12891/12891793.png" /></a></li>
                <li className="container">
                    <p><b>TRADEZY</b></p>
                </li>
                <li id="cart" ><a href="../cart.html">< img id="cart-img" className="mobile-img" src="https://cdn-icons-png.flaticon.com/128/12599/12599129.png" /></a></li>
            </ul>
            <ul id="big-scrn">
                <li id="home"><a href="../index.html"><img src="https://cdn-icons-png.flaticon.com/128/12891/12891793.png" /></a></li>
                <li><b>TRADEZY</b></li>
                <li>Clothes</li>
                <li>Toys</li>
                <li>Baby</li>
                <li>Electronics</li>
                <li>Home</li>
                <li id="cart" ><a href="../cart.html">< img id="cart-img" src="https://cdn-icons-png.flaticon.com/128/12599/12599129.png" /></a></li>
            </ul>
        </nav>

        <div className="content">
            <div className="row">
                {screen.availWidth > 750 && products.slice(index, index + 10).map((product) => (
                    <div className="item" key={product.id}>
                        <img src={product.image} alt={product.name} />
                        <p className="name">{product.name}</p>
                        <p className="colour">{product.colour}</p>
                        <p className="price">£{product.price.toFixed(2)}</p>
                    </div>
                ))}
                {screen.availWidth <= 750  && screen.availWidth > 420 && products.slice(index, index + 9).map((product) => (
                    <div className="item" key={product.id}>
                        <img src={product.image} alt={product.name} />
                        <p className="name">{product.name}</p>
                        <p className="colour">{product.colour}</p>
                        <p className="price">£{product.price.toFixed(2)}</p>
                    </div>
                ))}
                {screen.availWidth <= 420 && products.slice(index, index + 8).map((product) => (
                    <div className="item" key={product.id}>
                        <img src={product.image} alt={product.name} />
                        <p className="name">{product.name}</p>
                        <p className="colour">{product.colour}</p>
                        <p className="price">£{product.price.toFixed(2)}</p>
                    </div>
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