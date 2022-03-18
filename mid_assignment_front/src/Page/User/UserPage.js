import axios from "axios";
import React, { createContext, useEffect, useState } from "react";
import { Link, Route, Routes } from "react-router-dom";
import { useFetchData, useLoginState } from "../hooks";
import LoginPage from "../LoginPage/LoginPage";
import BookPage from "./BookPage/BookPage";

export const OrderContext = createContext();
const UserPage = (props) => {
    const [loginState, setLoginState] = useLoginState();
    const [order, setOrder] = useState({
        bookRequest: {},
        listDetail: []
    });
    const fetchData = (url, method, data) => {
        const header = {}
        if (loginState.token !== "") {
            header.tokenAuth = loginState.token
        }
        console.log(header);
        return axios({
            method: method,
            url: url,
            headers: header,
            data: data
        })
    }

    const handleCloseModal = () => {
        document.getElementById("bookOrder").classList.remove("show", "d-block");
        document.querySelectorAll(".modal-backdrop")
            .forEach(el => el.classList.remove("modal-backdrop"));
    }

    useEffect(() => {
        fetchData("https://localhost:7140/api/User/verify", "POST")
            .then(res => {
                console.log(res.data.message);
            }).catch(err => {
                console.log(err.message);
                handleLogout();
            })
        console.log(order);
    })
    // const isLogin = loginState.isLogin;

    const handleLogout = () => {
        setLoginState({
            token: "",
            isLogin: false,
            isAdmin: false
        })
        localStorage.removeItem("loginState");
    }

    const handleDeleteOrder = ({ index }) => {
        let detailList = [...order.listDetail];
        detailList.splice(index - 1, 1);
        setOrder({
            ...order,
            listDetail: detailList
        })
    }

    const handleSubmitOrder = () => {
        fetchData("https://localhost:7140/api/BookRequest", "POST", order)
        .then(res => {
            alert(res.data.message);
            setOrder({
                ...order,
                listDetail: []
            });
            handleCloseModal();
        }).catch((err) => {
            // console.log(err.response.data);
            alert(err.response.data.message);
        })
    }

    return (
        <>
            <div className="container">
                <div className="row w-100">
                    <div className="col-12">
                        <nav className="navbar navbar-expand-lg navbar-light bg-light p-3">
                            <span className="navbar-brand">Lib</span>
                            <button
                                className="btn btn-light text-dark total-order"
                                data-toggle="modal"
                                data-target="#bookOrder"
                            >
                                <div className="total-order__content">
                                    <i class="fa fa-book"></i>
                                    <div id="amount__books">{order.listDetail.length}</div>
                                </div>
                            </button>
                            <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                                <span className="navbar-toggler-icon"></span>
                            </button>
                            <div className="collapse navbar-collapse" id="navbarSupportedContent">
                                <ul className="navbar-nav">
                                    <li className="nav-item">
                                        <Link className="nav-link" to="book">Book</Link>
                                    </li>
                                    {/* <li className="nav-item">
                                        <Link className="nav-link" to="category">Category</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link" to="request">Request</Link>
                                    </li> */}
                                    {loginState.isLogin === true ? <button className="btn btn-secondary" onClick={handleLogout}>Logout</button> : ""}
                                </ul>

                            </div>
                        </nav>

                    </div>

                    <OrderContext.Provider value={[order, setOrder]}>
                        <div className="col-12 mt-5">
                            <h1>Xin chao User</h1>
                            <Routes>
                                <Route path="/" element={<BookPage />} />
                                <Route path="book" element={<BookPage />} />
                                <Route path="book/category/:cateId" element={<BookPage />} />
                                {/* <Route path="book/create" element={<LoginPage />} />
                                <Route path="book/edit/:id" element={<LoginPage />} />
                                <Route path="category" element={<LoginPage />} />
                                <Route path="category/:cateId" element={<LoginPage />} />
                                <Route path="request" element={<LoginPage />} /> */}
                            </Routes>
                        </div>
                    </OrderContext.Provider>
                </div>
            </div>


            <div className="modal fade" id="bookOrder" tabIndex="-1" role="dialog" aria-labelledby="bookOrderHead" aria-hidden="false">
                <div className="modal-dialog" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="bookOrderHead">Your order</h5>
                        </div>

                        <div className="modal-body">
                            {order.listDetail.length == 0 ?
                                "Empty" :
                                <table class="table table-hover table-borderless">
                                    <tbody>
                                        {
                                            order.listDetail.map((item, index) => (
                                                <tr key={index}>
                                                    {/* <th scope="row">1</th> */}
                                                    <td>#{index + 1}</td>
                                                    <td>{item.bookName}</td>
                                                    <td style={{ width: "125px" }}>
                                                        <div>
                                                            <button
                                                                className="text-danger btn me-2"
                                                                onClick={() => {
                                                                    handleDeleteOrder({
                                                                        ...item,
                                                                        index: index + 1
                                                                    })
                                                                }}
                                                            >
                                                                <i class="fa fa-trash-alt"></i>
                                                                <span className="ps-2 fw-bold">Delete</span>
                                                            </button>
                                                        </div>
                                                    </td>
                                                </tr>
                                            ))
                                        }
                                    </tbody>
                                </table>
                            }
                        </div>


                        <div className="modal-footer">
                            <button
                                type="button"
                                className="btn btn-secondary"
                                data-dismiss="modal"
                            >
                                Close
                            </button>

                            <button
                                type="button"
                                className="btn btn-primary"
                                onClick={handleSubmitOrder}
                            >
                                Save changes
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}

export default UserPage