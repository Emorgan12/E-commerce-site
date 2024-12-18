import React from "react";
import reactDOM from "react-dom/client";
import { useContext, useState, useEffect } from "react";
import Navbar from "./navbar";
import Footer from "./footer";
import { AuthContext } from "./authcontext";
import BASE_URL from "../main";

function Cart(){

    const [cart, setCart] = useState([]);
    const {user} = useContext(AuthContext);
    const [error, setError] = useState('');
    const [products, setProducts] = useState([]);
    const [totalPrice, setTotalPrice] = useState(0)

    useEffect(() => {
        fetch(`${BASE_URL}getCart?id=${user.cartId}`)
        .then(async (response) => {
            console.log('Received response:', response);
            if (!response.ok) {
                throw new Error(`Network response was not ok: ${response.statusText}`);
            }
            // Log response content for debugging
            const text = await response.text();
            console.log('Response text:', text);
            
            // Check if response is empty
            if (!text) {
                return [];
            }
            
            // Try to parse JSON
            try {
                return JSON.parse(text);
            } catch (e) {
                console.error('JSON parse error:', e);
                return [];
            }
        })
            .then((data) => {
                console.log('Success:', data);
                setCart(data);
                setProducts(data.products || []);
                let totalprice = 0;
                (data.products || []).forEach((product) => {
                    totalprice += product.price;
                });
                setTotalPrice(totalprice)
            })
            .catch((error) => {
                console.error('Error:', error);
                setError(error.message);
            });
        }, [user]);
    return(
        <>
            <Navbar />

            <div className="content">
                <h1>{user.username}</h1>
                <div class="cart-header">
                    <h2>Your Cart</h2>
                    <span>{products.length} items</span>
                </div>
                <div className="products-list">
                    {products.map((product) => (
                        <div className="cart-item" key={product.id} style={{paddingBottom: 15 + 'px' }}>
                            <img src={product.image} alt={product.name} />
                            <div className="details">
                                <p className="name">{product.name}</p>
                                <p className="colour">{product.colour}</p>
                                <p className="price">£{product.price.toFixed(2)}</p>
                            </div>
                        </div>
                    ))}
                </div>
                <div class="cart-summary">
                    <span>Subtotal:</span>
                    <span>£{totalPrice}</span>
                </div>
                
                <div class="cart-summary">
                    <span>Tax:</span>
                    <span>£{totalPrice*0.2}</span>
                </div>
                
                <div class="cart-summary" >
                    <span>Total:</span>
                    <span>£{totalPrice + (totalPrice*0.2)}</span>
                </div>
                
                <button class="checkout-btn">Proceed to Checkout</button>
            </div>
                

            <Footer />
        </>
    )
}

export default Cart;