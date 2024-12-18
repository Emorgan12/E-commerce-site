import { useState, useContext, useEffect } from 'react';
import { useNavigate, useParams} from 'react-router-dom';
import BASE_URL from '../main';
import { AuthContext } from './authcontext.jsx';
import Navbar from './navbar.jsx';
import Footer from './footer.jsx';

const Product = () => {

    const navigate = useNavigate();
    const { user } = useContext(AuthContext);
    const [product, setProduct] = useState({});
    const [error, setError] = useState('');
    const { id } = useParams();

    useEffect(() => {
        fetch(`${BASE_URL}searchProduct?id=${id}`)
        .then((response) => {
            if (!response.ok) {
                throw new Error('Product not found');
            }
            return response.json();
        })
        .then((data) => {
            setProduct(data);
        })
        .catch((error) => {
            setError(error.message);
        });
    }, [id]);
    
    const handleAddToCart = () => {
        fetch(`${BASE_URL}updateCart?accountId=${user.id}&productId=${product.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
        })
        .then(response => {
            if (!response.ok) throw new Error('Failed to add to cart');
            navigate('/cart');
        })
        .catch(error => setError(error.message));
    };

    if (error) return <div>Error: {error}</div>;
    if (!product) return <div>Loading...</div>;

    return (
        <>
            <Navbar />
            <div className='content'>
                <div className="product-details">
                    <img src={product.image} alt={product.name} />
                    <div className='details'>
                        <h2>{product.name}</h2>
                        <p id='description'>{product.description}</p>
                        <p>Category: {product.category}</p>
                        {product.category == 'Clothes' &&
                            <p id='size'>Size: {product.size}</p>}
                        <p>Material: {product.material}</p>
                        <p>Country of Origin: {product.originCountry}</p>
                        <p id='price'>Price: £{product.price}</p>
                        <button onClick={handleAddToCart}>Add to Cart</button>
                    </div>
                </div>
            </div>
            <Footer />
        </>
    );
}

export default Product;