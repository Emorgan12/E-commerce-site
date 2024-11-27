import { Link } from "react-router-dom"
import Navbar from "./navbar"
import Footer from "./footer"

function FAQ() {
    return (
        <>
        <Navbar />
        <div>
            <h1>FAQ</h1>
            <p>Here are some frequently asked questions</p>
        </div>
        <Footer />
        </>
    )
}

export default FAQ;