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
                            <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAWlBMVEXv8fNod4f19vhkdIRcbX52g5KPmqX29/iYoq3l6OuCj5vd4eTr7fBfcIFaa33M0dbBx82SnKe7wchtfIt8iZejq7TU2N2Ik6CwuL/Gy9Gqsrqbpa/P1NmhqrNz0egRAAADBklEQVR4nO3c63KqMBRAYUiwwUvEete27/+ax1tVAqhwEtnprO+XM62Oyw2CGTFJAAAAAAAAAAAAAAAAAAAAAAAAAAAAAJe6Mb5vqL7jjsws/wgln/dddzBZZjocuxj2HaiWNg1JL/oO3GVBA9PUzvvdF80q7AgPQ/zot1DlOnThyFBIIYWvFtrMK3mFdj30aWzFFWZjr+/qE4mFXh+YwrehsDMK34bCzmIoVEad1nC6PbD8QpXMNwOdDvKi2xMUX2jm2h7/onU2WHcZo/RCld8WN3TWZR1CeKH6LK1tTGftE2UXqpmzPGXbLwnKLkzcT8X6s/UQRReqWWX9LWs9RNGF5qOysmFb74miC9XCDUzt6k8VJtXC9jsihW9Tu5Uuq/vhvlKokuGjc1bRhWZVLdw5MWq8mU6zfNL4wKILk/W0spW6dyvOZ61p4wKd7EIzcoZot+UQVVxeA62bEmUXJuPyIV8PnDsVtxXtpikKL1S7++1U6/IZzV1g8xSFFx4i9HWMdjksNZQCGxOlFyZq8jW1VmubpZV90PngUZ8ovvDYuNt//Wy/1ZPAhsQICo+rUMa4T70msP7tJorCun8vKofKhilGWlg7wfopxlnYMMHaKUZZ2DjBuinGWPgwsDLFCAufBLqJ8RU+DXQ21OgKXwgsTzG2wpcCj1O8nsJGVvjgMNE0xbgKX5zgeYqXxKgKX57geYrnDTWmwhYTvJtiRIUtA3/fbuIpbB14mWI0hR0Cz1OMpbBT4CkxiaOwY+BpQ42isNVhwk283hJc2HmC5Va5hf8xwTgK/UxQcKGvQLGF3gKlFvoLFFroMVBmoc9AkYWeDhNyC1Xh9aJLeYV+Jyiw0Os+KLHQe6C0Qv+BwgoDBMoqDBEoqtCECJRUOPz2e5gQV2jnYa7qllOYBvr5CEGFgVBIIYXPmJ/ghZueZ+hexOWd+w3q9ycuwg5R2377DsapDflbX7rTFah+TbajQSij/aT/wNNF26FUvoELAAAAAAAAAAAAAAAAAAAAAAAAAAAA4G/4B9L3P1vg3y4/AAAAAElFTkSuQmCC" alt={product.name} />
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