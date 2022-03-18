import axios from "axios";
import React, { useEffect, useState } from "react"
import { Link, useParams } from "react-router-dom";
import { useLoginState, useOrderState } from "../../hooks";

const BookPage = (props) => {
    const { cateId } = useParams();
    const [isLoading, setLoading] = useState(true);
    const [bookList, setBookList] = useState([]);
    const [loginState] = useLoginState();
    const [order, setOrder] = useOrderState();
    const [paginate, setPaginate] = useState({
        CurrentPage: 1,
        // TotalPage: NaN,
        PageSize: 3,
        // HasNext: true,
        // HasPrevious: false
    })

    console.log("cate Id: ", cateId);

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
        console.log(cateId);
        if (cateId !== null && cateId > 0) {
            fetchData("https://localhost:7140/api/Category/books/" + cateId + `?pageSize=${paginate.PageSize}` + "&&" + `CurrentPage=${paginate.CurrentPage}`, "GET")
                .then(res => {
                    let serverPaginate = JSON.parse(res.headers["x-pagination"]);
                    if (paginate.CurrentPage > serverPaginate.TotalPage) {
                        serverPaginate = {
                            ...serverPaginate,
                            CurrentPage: 1
                        }
                    }
                    setPaginate(serverPaginate);
                    setBookList(res.data);
                    setLoading(false);
                })
                .catch(err => {
                    console.log(err);
                })
        } else {
            fetchData("https://localhost:7140/api/Book?" + `pageSize=${paginate.PageSize}` + "&&" + `CurrentPage=${paginate.CurrentPage}`, "GET")
                .then(res => {
                    let serverPaginate = JSON.parse(res.headers["x-pagination"]);
                    setPaginate(serverPaginate);
                    setBookList(res.data);
                    setLoading(false);
                })
                .catch(err => {
                    console.log(err);
                })
        }
        console.log("fetch");
        return () => {
            console.log("clear");
        }
    }, [paginate.CurrentPage, cateId]);

    const handleAddOrder = ({ bookId, bookName }) => {
        let listDetail = order.listDetail;
        let size = listDetail.length;
        if(size >= 5){
            alert("You cannot borrow more than 5 books");
        }else {
            listDetail.push({
                bookId: bookId,
                bookName: bookName
            })
            setOrder({
                ...order,
                listDetail: listDetail
            })
        }
    }

    const handlePage = (newCurrentPage) => {
        console.log(newCurrentPage)
        setPaginate({
            ...paginate,
            CurrentPage: newCurrentPage
        })
    }

    const handlePrev = () => {
        let current = paginate.CurrentPage;
        setPaginate({
            ...paginate,
            CurrentPage: current - 1
        })
    }

    const handleNext = () => {
        let current = paginate.CurrentPage;
        setPaginate({
            ...paginate,
            CurrentPage: current + 1
        })
    }

    if (isLoading === true) {
        return (
            <div className="m-2 ms-0">
                <h2>Loading....</h2>
            </div>
        )
    }


    return (
        <>
            {cateId > 0 ?
                <div className="m-2 ms-0">
                    <Link to="/">
                        <button className="btn btn-success">
                            Reset
                        </button>
                    </Link>
                </div>
                :
                <div className="m-2 ms-0"></div>
            }

            <table className="table table-hover">
                <thead className="thead-dark">
                    <tr>
                        <th scope="col" style={{ width: "50px" }}>#</th>
                        <th scope="col">Name</th>
                        <th scope="col">Category</th>
                        <th scope="col" style={{ width: "170px", textAlign: "center" }}>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {bookList.map((item, index) =>
                    (
                        <tr key={index}>
                            <td>{item.bookItem.bookId}</td>
                            <td>{item.bookItem.bookName}</td>
                            <td>
                                <Link to={`/book/category/${item.categoryOfBook.categoryId}`}>
                                    {item.categoryOfBook.categoryName}
                                </Link>
                            </td>
                            <td>
                                <div>
                                    <button
                                        className="text-danger btn me-2"
                                        onClick={() => {
                                            handleAddOrder({
                                                bookId: item.bookItem.bookId,
                                                bookName: item.bookItem.bookName
                                            })
                                        }}
                                    >
                                        <i class="fa fa-plus mr-2"></i>
                                        <span className="ps-2 fw-bold">Add to order</span>
                                    </button>
                                </div>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            {
                paginate.TotalPage > 0 ?
                    (
                        <nav aria-label="Page navigation example">
                            <ul className="pagination justify-content-end">
                                <li className={`page-item ${paginate.HasPrevious ? "" : "disabled"}`}><span className="page-link" onClick={handlePrev}>Previous</span></li>
                                {
                                    [...Array(paginate.TotalPage)].map((item, index) => (
                                        <li className={`page-item ${(index + 1) === paginate.CurrentPage ? "active" : ""}`} key={index + 1}>
                                            <span className="page-link" onClick={() => handlePage(index + 1)}> {index + 1} </span>
                                        </li>
                                    ))
                                }
                                <li className={`page-item ${paginate.HasNext ? "" : "disabled"}`}><span className="page-link" onClick={handleNext}>Next</span></li>
                            </ul>
                        </nav>
                    )
                    :
                    (
                        <nav></nav>
                    )
            }

        </>
    )
}

export default BookPage;