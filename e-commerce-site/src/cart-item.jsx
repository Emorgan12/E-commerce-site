import React from "react";

function CartItem(){

    return(
        <div class="cart-item">
                    <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQeJQeJyzgAzTEVqXiGe90RGBFhfp_4RcJJMQ&s" />
                    <div id="description">
                        <p id="cart-name">Name</p>
                        <p id="cart-colour">Colour</p>
                        <p id="cart-price">£0.00</p>
                    </div>
                </div>
    )
}

export default CartItem;