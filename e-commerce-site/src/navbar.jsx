

function Navbar(){
    return(
        <nav>
            <ul id="mobile">
                <li id="home"><a href="../index.html"><img className="mobile-img" src="https://cdn-icons-png.flaticon.com/128/12891/12891793.png" /></a></li>
                <li className="container">
                    <p><b>TRADEZY</b></p>
                </li>
                <li id="cart" ><a href="../cart.html">< img id="cart-img" className="mobile-img" src="https://cdn-icons-png.flaticon.com/128/12599/12599129.png" /></a></li>
            </ul>
            <ul id="big-scrn">
                <li id="home"><a href="../index.html"><img src="https://cdn-icons-png.flaticon.com/128/12891/12891793.png" /></a></li>
                <li><b>TRADEZY</b></li>
                <li>Clothes</li>
                <li>Toys</li>
                <li>Baby</li>
                <li>Electronics</li>
                <li>Home</li>
                <li id="cart" ><a href="../cart.html">< img id="cart-img" src="https://cdn-icons-png.flaticon.com/128/12599/12599129.png" /></a></li>
            </ul>
        </nav>
    )
}

export default Navbar;