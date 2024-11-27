import React from 'react';
import {Routes, Route } from 'react-router-dom';
import Login from './src/login';
import Register from './src/register';
import Products from './src/products';
import Cart from './src/cart';
import Product from './src/product';
import FAQ from './src/faq';

function App() {
    return (
        <Routes>
            <Route path="/" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="/products" element={<Products />} />
            <Route path="/cart" element={<Cart />} />
            <Route path="/product/:id" element={<Product />} />
            <Route path='/FAQ' element={<FAQ />} />
        </Routes>
    );
}

export default App;