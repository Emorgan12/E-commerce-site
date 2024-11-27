import { Link } from "react-router-dom"
import Navbar from "./navbar"
import Footer from "./footer"
import Collapsible from "./collapsible";

function FAQ() {
    return (
        <>
        <Navbar />
        <div className="content">
            <div className="container"><h1>Frequently Asked Questions</h1></div>
            <Collapsible title={"Shipping"} id="shipping">
                <div className="questions">
                    <div>
                        <p>Q: How long does shipping take?</p>
                        <p>A: We aim to dispatch all items in 3-5 business days after the order has been proccessed</p>
                    </div>
                    <div>
                        <p>Q: Where is my order?</p>
                        <p>A: Orders can be tracked using the Royal Mail tracking number attached to your order</p>
                    </div>
                    <div>
                        <p>Q: Do you ship internationally?</p>
                        <p>A: Yes, we ship to most countries worldwide. Shipping costs will be calculated at checkout.</p>
                    </div>
                    <div>
                        <p>Q: Can I change my shipping address after placing an order?</p>
                        <p>A: Unfortunately, we cannot change the shipping address once the order has been processed. Please ensure your address is correct before placing your order.</p>
                    </div>
                </div>
            </Collapsible>
            <Collapsible title={"Returns"} id="returns">
                <div className="questions">
                    <div>
                        <p>Q: Can I return my order?</p>
                        <p>A: Yes, we offer a 30 day return policy. Please contact us at tradezy@zoobdude.com to arrange a return.</p>
                    </div>
                    <div>
                        <p>Q: How long does it take to process a return?</p>
                        <p>A: Returns are processed within 14 days of receiving the item.</p>
                    </div>
                    <div>
                        <p>Q: Do I have to pay for return shipping?</p>
                        <p>A: Yes, you are responsible for return shipping costs.</p>
                    </div>
                    <div>
                        <p>Q: Can I return an item bought at a discounted rate?</p>
                        <p>A: No, Discounted items are non-refundable.</p>
                    </div>
                </div>
            </Collapsible>
            <Collapsible title={"Payment"} id="payment">
                <div className="questions">
                    <div>
                        <p>Q: What payment methods do you accept?</p>
                        <p>A: We accept all major credit and debit cards, as well as PayPal</p>
                    </div>
                    <div>
                        <p>Q: Is it safe to use my credit card on your website?</p>
                        <p>A: Yes, we use SSL encryption to ensure that your payment information is secure.</p>
                    </div>
                    <div>
                        <p>Q: Can I pay with multiple credit cards?</p>
                        <p>A: No, we only accept one form of payment per order.</p>
                    </div>
                    <div>
                        <p>Q: When will my credit card be charged?</p>
                        <p>A: Your credit card will be charged at the time of purchase.</p>
                    </div>
                </div>
            </Collapsible>
            <div id="contact">
                <h1>Still have a question or concern?</h1>
                <h2>Contact us at:</h2>
                <div id="email">
                    <img id="email-icon" src="https://cdn-icons-png.flaticon.com/128/747/747314.png" />
                    <p>tradezy@zoobdude.com</p>
                </div>
            </div>
            
        </div>
        <Footer />
        </>
    )
}

export default FAQ;