import axios from "axios";
import React, { useEffect, useState } from "react"
import { Link, useParams } from "react-router-dom";
import { useLoginState } from "../../hooks";


const BookManagerPage = () => {
    const { cateId } = useParams();
    const [isLoading, setLoading] = useState(true);
    const [bookList, setBookList] = useState([]);
    const [loginState] = useLoginState();
    const [paginate, setPaginate] = useState({
        CurrentPage: 1,
        // TotalPage: NaN,
        PageSize: 3,
        // HasNext: true,
        // HasPrevious: false
    })
    
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
        if(cateId !== null && cateId > 0){
            fetchData("https://localhost:7140/api/Category/books/" + cateId + `?pageSize=${paginate.PageSize}` + "&&" + `CurrentPage=${paginate.CurrentPage}`, "GET")
                .then(res => {
                    let serverPaginate = JSON.parse(res.headers["x-pagination"]);
                    if(paginate.CurrentPage > serverPaginate.TotalPage){
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
        }else {
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

    const handleDelete = (id) => {
        fetchData("https://localhost:7140/api/Book/" + id, "DELETE")
            .then(res => {
                console.log(res.data);
                setBookList(bookList.filter((item) => {
                    return item.bookItem.bookId !== id;
                }))
            }).catch(err => {
                alert("Delete fail");
            })
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
            {cateId == null || cateId <= 0 ? 
                <div className="m-2 ms-0">
                    <Link to="create">
                        <button className="btn btn-primary">
                            Create
                        </button>
                    </Link>
                </div>
                :
                <div className="m-2 ms-0"></div>
            }

            <table className="table table-bordered table-hover">
                <thead className="thead-dark">
                    <tr>
                        <th scope="col" style={{ width: "50px" }}>#</th>
                        <th scope="col">Name</th>
                        <th scope="col">Category</th>
                        <th scope="col" style={{ width: "170px" }}>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {bookList.map((item, index) =>
                    (
                        <tr key={index}>
                            <td>{item.bookItem.bookId}</td>
                            <td>{item.bookItem.bookName}</td>
                            <td>
                                <Link to={"/category/" + item.categoryOfBook.categoryId}>
                                    {item.categoryOfBook.categoryName}
                                </Link>
                            </td>
                            <td>
                                <div>
                                    <button
                                        className="btn btn-danger me-2"
                                        onClick={() => {
                                            handleDelete(item.bookItem.bookId)
                                        }}
                                    >
                                        Delete
                                    </button>
                                    <Link to={"/book/edit/" + item.bookItem.bookId}>
                                        <button className="btn btn-warning">Edit</button>
                                    </Link>
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

export default BookManagerPage