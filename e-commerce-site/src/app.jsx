import React from 'react';
import {Routes, Route, BrowserRouter } from 'react-router-dom';
import Login from './login';
import Register from './register';
import Products from './products';
import Cart from './cart';
import Product from './product';
import FAQ from './faq';
import About from './about';
import Checkout from './checkout'
import NewProduct from './new-product'
import Dashboard from './dashboard';
import ReactDOM from 'react-dom/client';
import { AuthProvider } from './authcontext';

function App() {
    return (
        ReactDOM.createRoot(document.getElementById('app')).render(
          <React.StrictMode>
            <AuthProvider>
              <BrowserRouter>
                <Routes>
                    <Route path="/" element={<Login />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/products" element={<Products />} />
                    <Route path="/cart" element={<Cart />} />
                    <Route path="/product/:id" element={<Product />} />
                    <Route path='/faq' element={<FAQ />} />
                    <Route path='/about' element={<About />} />
                    <Route path='/checkout' element={<Checkout />}/>
                    <Route path='/new-product' element={<NewProduct />}/>
                    <Route path="/dashboard" element={<Dashboard />} />
                </Routes>
              </BrowserRouter>
            </AuthProvider>
          </React.StrictMode>
        )
    );
}

export default App;