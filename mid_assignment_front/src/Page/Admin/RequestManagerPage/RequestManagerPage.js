import axios from "axios";
import { useEffect, useState } from "react";
import { useLoginState } from "../../hooks";

const RequestManagerPage = () => {
    const [isLoading, setLoading] = useState(true);
    const [requestList, setRequestList] = useState([]);
    const [loginState] = useLoginState();

    const [paginate, setPaginate] = useState({
        CurrentPage: 1,
        // TotalPage: NaN,
        PageSize: 10,
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

    console.log("request list : ", requestList)

    useEffect(() => {
        fetchData("https://localhost:7140/api/BookRequest?" + `pageSize=${paginate.PageSize}` + "&&" + `CurrentPage=${paginate.CurrentPage}`, "GET")
            .then(res => {
                let serverPaginate = JSON.parse(res.headers["x-pagination"]);
                setPaginate(serverPaginate);
                setRequestList(res.data);
                setLoading(false);
            })
            .catch(err => {
                console.log(err);
            })
        console.log("fetch");
        return () => {
            console.log("clear");
        }
    }, [paginate.CurrentPage]);

    const handleReject = (requestId) => {
        const submitData = {
            state: 2
        }
        fetchData(`https://localhost:7140/api/BookRequest/${requestId}?` + `pageSize=${paginate.PageSize}` + "&&" + `CurrentPage=${paginate.CurrentPage}`, "PUT", submitData)
            .then(res => {
                alert(res.data.message);
            }).catch(err => {

            })
    }

    const handleApprove = (requestId) => {
        const submitData = {
            state: 1
        }
        fetchData(`https://localhost:7140/api/BookRequest/${requestId}?` + `pageSize=${paginate.PageSize}` + "&&" + `CurrentPage=${paginate.CurrentPage}`, "PUT", submitData)
            .then(res => {
                alert(res.data.message);
            }).catch(err => {

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
            <table className="table table-bordered table-hover">
                <thead className="thead-dark">
                    <tr>
                        <th scope="col" style={{ width: "50px" }}>#</th>
                        <th scope="col" style={{ width: "50px" }}>#User</th>
                        <th scope="col">Username</th>
                        <th scope="col">Books</th>
                        <th scope="col" style={{ width: "200px" }}>State</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        requestList.map((item, index) =>
                        (
                            <tr key={index}>
                                <td>{item.bookRequest.bookBorrowingRequestId}</td>
                                <td>{item.bookRequest.userId}</td>
                                <td>{item.bookRequest.username}</td>
                                <td>
                                    <ul>
                                        {
                                            item.listDetail.map((item, index) => (<li key={"request" + index}>{item.bookName}</li>))
                                        }
                                    </ul>
                                </td>
                                <td>
                                    <div>
                                        {
                                            item.bookRequest.state == 0 ? <p className="text-primary">Waiting ...</p> :
                                                item.bookRequest.state == 1 ? <p className="text-success">Approve</p> :
                                                    <p className="text-danger">Reject</p>
                                        }
                                    </div>
                                    {
                                        item.bookRequest.state == 0 ?
                                            (
                                                <div>
                                                    <button
                                                        className="btn btn-danger me-2"
                                                        onClick={() => {
                                                            handleReject(item.bookRequest.bookBorrowingRequestId)
                                                        }}
                                                    >
                                                        Reject
                                                    </button>
                                                    {/* // <Link to={"/category/edit/" + item.bookItem.bookId}> */}
                                                    <button
                                                        className="btn btn-success"
                                                        onClick={() => {
                                                            handleApprove(item.bookRequest.bookBorrowingRequestId)
                                                        }}
                                                    >
                                                        Approve
                                                    </button>
                                                    {/* // </Link> */}
                                                </div>
                                            )
                                            :
                                            <></>
                                    }
                                </td>
                            </tr>
                        )
                        )
                    }
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

export default RequestManagerPage