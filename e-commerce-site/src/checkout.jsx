import React from "react";
import reactDOM from "react-dom/client";
import { useContext, useState, useEffect } from "react";
import Navbar from "./navbar";
import Footer from "./footer";
import { AuthContext } from "./authcontext";
import BASE_URL from "../main";
import { capitalize } from "@mui/material";

function Checkout(){
    const {user} = useContext(AuthContext);
    const [products, setProducts] = useState([])
    const [totalPrice, setTotalPrice] = useState(0)
    const [error, setError] = useState('')
    const [cart, setCart] = useState([]);

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
                <h1><span id="username">{user.username}</span>'s checkout</h1>
                <div class="cart-header">
                    <h2>Your Cart</h2>
                    <span>{products.length} items</span>
                </div>
                <table>
                    
                    <tbody>
                        {products.map((product) => (
                            <tr key={product.id}>
                                <td>{product.name}</td>
                                <td>£{product.price}</td>
                            </tr>
                        ))}

                        <tr>
                            <td style={{fontWeight: 700}}>Subtotal</td>
                            <td>£{totalPrice}</td>
                        </tr>
                        <tr>
                            <td style={{fontWeight: 700}}>Tax</td>
                            <td>£{(totalPrice*0.2).toFixed(2)}</td>
                        </tr>
                        <tr>
                            <td style={{fontWeight: 700}}>Total Cost</td>
                            <td>£{(totalPrice + (totalPrice*0.2)).toFixed(2)}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
                

            <Footer />
        </>
    )
}

export default Checkout;