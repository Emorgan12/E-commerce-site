import React from "react";

function Footer(){

    return(
        <footer>
            <div className="footer-section" id="faq-big">
                <h4>FAQ</h4>
                <ul>
                    <li><a href="/faq#shipping">Shipping Information</a></li>
                    <li><a href="/faq#returns">Returns & Exchanges</a></li>
                    <li><a href="/faq#payment">Payment Methods</a></li>
                </ul>
            </div>
            <div className="footer-section" id="contact-big">
                <h4>Contact Us</h4>
                <p>Email: tradezy@zoobdude.com</p>
            </div>
            <div className="footer-section" id="about-big">
                <h4><a href="/about">Learn more about us</a></h4>
            </div>
            <div className="small"> 
                <a id="faq-small"><img src="https://cdn-icons-png.flaticon.com/128/471/471664.png"/></a>
                <a id="contact-small"><img src="https://cdn-icons-png.flaticon.com/128/747/747314.png"/></a>
                <a id="about-small"><img src="https://cdn-icons-png.flaticon.com/128/665/665049.png"/></a>
            </div>
    </footer>
    )
}

export default Footer;