import React from "react";

function Footer(){

    return(
        <footer>
            <div className="footer-section" id="faq">
                <h4>FAQ</h4>
                <ul>
                    <li><a href="/faq#shipping">Shipping Information</a></li>
                    <li><a href="/faq#returns">Returns & Exchanges</a></li>
                    <li><a href="/faq#payment">Payment Methods</a></li>
                </ul>
            </div>
            <div className="footer-section" id="contact">
                <h4>Contact Us</h4>
                <p>Email: tradezy@zoobdude.com</p>
            </div>
            <div className="footer-section" id="about">
                <h4><a href="/about">Learn more about us</a></h4>
            </div>
    </footer>
    )
}

export default Footer;