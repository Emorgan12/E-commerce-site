import reactDOM from 'react-dom/client';
import React, { useContext, useEffect, useState } from 'react';
import Navbar from './navbar.jsx';
import Footer from './footer.jsx';
import BASE_URL from '../main.js';
import {useNavigate, Link } from 'react-router-dom';
import { AuthContext } from './authcontext.jsx';

function NewProduct () {
    const {user} = useContext(AuthContext);
    const [image, setImage] = useState('');
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [category, setCategory] = useState('');
    const [size, setSize] = useState('');
    const [price, setPrice] = useState(0.0);
    const [colour, setColour] = useState('')
    const [material, setMaterial] = useState('')
    const [originCountry, setOriginCountry] = useState('')
    const [quantity, setQuantity] = useState(0)


    function handleAddProduct(event){
        event.preventDefault()
        fetch(`${BASE_URL}postProduct`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            name,
            image,
            colour,
            price,
            description,
            size,
            material,
            originCountry,
            quantity,
            category
        })})
    }

    return(
        <>
        <Navbar/>
        <div className='content'>
            {user.admin ? (
                <form>
                    <label>Name</label>
                    <input value={name} onChange={(e) => setName(e.target.value)} />
                    <label>Image URL</label>
                    <input value={image} onChange={(e) => setImage(e.target.value)} />
                    <label>Colour</label>
                    <input value={colour} onChange={e => setColour(e.target.value)} />
                    <label>Price</label>
                    <input value={price} onChange={(e) => setPrice(e.target.value)} />
                    <label>Description</label>
                    <input value={description} onChange={(e) => setDescription(e.target.value)} />
                    <label>Size</label>
                    <input value={size} onChange={(e) => setSize(e.target.value)} />
                    <label>Material</label>
                    <input value={material} onChange={e => setMaterial(e.target.value)} />
                    <label>Origin Country</label>
                    <input value={originCountry} onChange={e => setOriginCountry(e.target.value)} />
                    <label>Quantity</label>
                    <input value={quantity} onChange={e => setQuantity(e.target.value)} />
                    <label>Category</label>
                    <input value={category} onChange={(e) => setCategory(e.target.value)} />
                    <button type="submit" onClick={handleAddProduct}>Add Product</button>
                </form>
            ) : (
                <p>You do not have the correct permissions to access this page.</p>
            )}
        </div>
        <Footer/>
        </>
    )
}

export default NewProduct;