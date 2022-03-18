import axios from "axios";
import React, { useEffect } from "react";
import { Link, Route, Routes } from "react-router-dom";
import { useLoginState } from "../hooks";
import BookCreatePage from "./BookManagerPage/BookCreatePage/BookCreatePage";
import BookEditPage from "./BookManagerPage/BookEditPage/BookEditPage";
import BookManagerPage from "./BookManagerPage/BookManagerPage";
import CategoryManagerPage from "./CategoryManagerPage/CategoryManagerPage";
import RequestManagerPage from "./RequestManagerPage/RequestManagerPage";

const AdminPage = (props) => {
    const [loginState, setLoginState] = useLoginState();

    const fetchData = (url, method, data) => {
        const header = {}
        if (loginState.token !== "") {
            header.tokenAuth = loginState.token
        }
        return axios({
            method: method,
            url: url,
            headers: header,
            data: data
        })
    }
    useEffect(() => {
        fetchData("https://localhost:7140/api/User/verify", "POST")
        .then(res => {
            console.log(res.data.message);
        }).catch(err => {
            console.log(err.message);
            handleLogout();
        })
    })
    const isLogin = loginState.isLogin;

    const handleLogout = () => {
        setLoginState({
            token: "",
            isLogin: false,
            isAdmin: false
        })
        localStorage.removeItem("loginState");
    }
    return (
        <div className="container">
            <div className="row w-100">
                <div className="col-12">
                    <nav className="navbar navbar-expand-lg navbar-light bg-light p-3">
                        <span className="navbar-brand">Lib</span>
                        <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="collapse navbar-collapse" id="navbarSupportedContent">
                            <ul className="navbar-nav">
                                <li className="nav-item">
                                    <Link className="nav-link" to="book">Book</Link>
                                </li>
                                <li className="nav-item">
                                    <Link className="nav-link" to="category">Category</Link>
                                </li>
                                <li className="nav-item">
                                    <Link className="nav-link" to="request">Request</Link>
                                </li>
                                {isLogin === true ? <button className="btn btn-secondary" onClick={handleLogout}>Logout</button> : ""}
                            </ul>
                        </div>
                    </nav>
                    
                </div>

                <div className="col-12 mt-5">
                    <h1>Xin chao Admin</h1>
                    <Routes>
                        <Route path="/" element={<BookManagerPage />} />
                        <Route path="book" element={<BookManagerPage />} />
                        <Route path="book/create" element={<BookCreatePage />} />
                        <Route path="book/edit/:id" element={<BookEditPage />} />
                        <Route path="category" element={<CategoryManagerPage />} />
                        <Route path="category/:cateId" element={<BookManagerPage />} />
                        <Route path="request" element={<RequestManagerPage />} />
                    </Routes>
                </div>
            </div>
        </div>
    )
}

export default AdminPage
