import React from "react";

function Product(){

    return(
        <div className="item" key={product.id}>
            <img src={product.image} alt={product.name} />
            <p className="name">{product.name}</p>
            <p className="colour">{product.colour}</p>
            <p className="price">£{product.price.toFixed(2)}</p>
        </div>
    )
}

export default Product;