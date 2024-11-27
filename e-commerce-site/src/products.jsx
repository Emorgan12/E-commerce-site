import React, { useContext } from "react";
import reactDOM from "react-dom";
import { useState, useEffect } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import BASE_URL from "../main.js";
import Navbar from "./navbar.jsx";
import Footer from "./footer.jsx";
import { AuthContext } from "./authcontext.jsx"; 

function Products() {

    const location = useLocation();
    const { user} = useContext(AuthContext);
    const [products, setProducts] = useState([]);
    const [error, setError] = useState('')
    const [index, setIndex] = useState(0)
    const navigate = useNavigate();

    useEffect(() => {

        console.log("user", user.username);
        fetch(BASE_URL + encodeURIComponent('getProducts'))
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
}, [user]);

    return (
        <>
        <Navbar />

        <div className="content">
            <h2>Welcome, {user.username || 'Guest'}</h2>
            <div className="row">
                {screen.availWidth > 750 && products.slice(index, index + 10).map((product) => (
                    <div className="item" key={product.id}>
                    <img src={product.image} alt={product.name} />
                    <p className="name">{product.name}</p>
                    <p className="colour">{product.colour}</p>
                    <p className="price">£{product.price.toFixed(2)}</p>
                    <button 
                    className="view" 
                    onClick={() => navigate(`/product/${product.id}`)}
                >View</button>
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
                    <button className="previous" onClick={() => {if(screen.availWidth > 750) setIndex(index - 10); else if(screen.availWidth > 420) setIndex(index-9); else setIndex(index-8); }}>Previous page</button>
                }
                {
                    index + 10 < products.length &&
                        <button className="next" onClick={() => {if(screen.availWidth > 750) setIndex(index + 10); else if(screen.availWidth > 420) setIndex(index+9); else setIndex(index+8); }}>Next page</button>
                }
        </div>
        <Footer />
        
        </>
    );
}


export default Products;