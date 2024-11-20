import React from "react";
import { Link } from "react-router-dom";

function Footer(){

    return(
        <footer>
            <div className="footer-section" id="faq-big">
                <h4>FAQ</h4>
                <ul>
                    <li><Link to="/faq#shipping">Shipping Information</Link></li>
                    <li><Link to="/faq#returns">Returns & Exchanges</Link></li>
                    <li><Link to="/faq#payment">Payment Methods</Link></li>
                </ul>
            </div>
            <div className="footer-section" id="contact-big">
                <h4>Contact Us</h4>
                <p>Email: tradezy@zoobdude.com</p>
            </div>
            <div className="footer-section" id="about-big">
                <h4><Link to="/Linkbout">Learn more about us</Link></h4>
            </div>
            <div className="small"> 
                <Link to="/faq#shipping" id="faq-small"><img src="https://cdn-icons-png.flaticon.com/128/471/471664.png"/></Link>
                <Link to="/faq#returns" id="contact-small"><img src="https://cdn-icons-png.flaticon.com/128/747/747314.png"/></Link>
                <Link to="/faq#payment" id="about-small"><img src="https://cdn-icons-png.flaticon.com/128/665/665049.png"/></Link>
            </div>
    </footer>
    )
}

export default Footer;