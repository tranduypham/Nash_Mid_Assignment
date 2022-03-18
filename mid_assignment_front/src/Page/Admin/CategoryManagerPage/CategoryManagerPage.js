import axios from "axios";
import React, { useEffect, useRef, useState } from "react"
import { Link, useParams } from "react-router-dom";
import { useLoginState } from "../../hooks";

const CategoryManagerPage = () => {
    const btnClose = useRef();
    const [isLoading, setLoading] = useState(true);
    const [cateList, setCateList] = useState([]);
    const [loginState] = useLoginState();
    const [submitData, setSubmitData] = useState({
        categoryId: 0,
        categoryName: "",
    })
    
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

    console.log(cateList)
    useEffect(() => {
        fetchData("https://localhost:7140/api/Category?" + `pageSize=${paginate.PageSize}` + "&&" + `CurrentPage=${paginate.CurrentPage}`, "GET")
            .then(res => {
                let serverPaginate = JSON.parse(res.headers["x-pagination"]);
                setPaginate(serverPaginate);
                setCateList(res.data);
                setLoading(false);
            })
            .catch(err => {
                console.log(err);
            })
        console.log("fetch");
        return () => {
            console.log("clear");
        }
    }, [paginate.CurrentPage, submitData.categoryId]);

    const handleCloseModal = () => {
        document.getElementById("modalEdit").classList.remove("show", "d-block");
        document.getElementById("modalCreate").classList.remove("show", "d-block");
        document.querySelectorAll(".modal-backdrop")
            .forEach(el => el.classList.remove("modal-backdrop"));
    }

    const handleDelete = (id) => {
        fetchData("https://localhost:7140/api/Category/" + id, "DELETE")
            .then(res => {
                console.log(res.data);
                setCateList(cateList.filter((item) => {
                    return item.categoryId !== id;
                }))
            }).catch(err => {
                alert("Delete fail");
            })
    }

    const handleEdit = (id) => {
        console.log("id edit : ", id);
        fetchData("https://localhost:7140/api/Category/" + id, "GET")
            .then(res => {
                console.log(res.data);
                setSubmitData({
                    ...submitData,
                    categoryId: id,
                    categoryName: res.data.categoryName
                })
            }).catch(err => {
                alert("Edit fail");
            })
    }

    const handleCreate = () => {
        setSubmitData({
            ...submitData,
            categoryId: 666,
            categoryName: ""
        })
    }

    const handleCancelEdit = () => {
        console.log("cancel");
        setSubmitData({
            ...submitData,
            categoryId: 0,
            categoryName: ""
        })
    }

    const handleCancelCreate = () => {
        console.log("cancel");
        setSubmitData({
            ...submitData,
            categoryId: 0,
            categoryName: ""
        })
    }

    const handleSubmitEdit = () => {
        fetchData("https://localhost:7140/api/Category/" + submitData.categoryId, "PUT", submitData)
            .then(res => {
                console.log(res.data);
                alert(res.data.message);
                setSubmitData({
                    ...submitData,
                    categoryId: 0,
                    categoryName: ""
                })
                handleCloseModal();
            }).catch(err => {
                alert("Edit fail");
            })
    }

    const handleSubmitCreate = () => {
        
        fetchData("https://localhost:7140/api/Category", "POST", submitData)
            .then(res => {
                console.log(res.data);
                alert(res.data.message);
                setSubmitData({
                    ...submitData,
                    categoryId: 0,
                    categoryName: ""
                })
                handleCloseModal();
            }).catch(err => {
                alert("Create fail");
            })
    }

    const handleNameChange = (event) => {
        console.log(event.target.value)
        setSubmitData({
            ...submitData,
            categoryName: event.target.value
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
            <div className="m-2 ms-0">
                <button
                    className="btn btn-primary"
                    data-toggle="modal"
                    data-target="#modalCreate"
                    onClick={handleCreate}
                >
                    Create
                </button>
            </div>

            <table className="table table-bordered table-hover">
                <thead className="thead-dark">
                    <tr>
                        <th scope="col" style={{ width: "50px" }}>#</th>
                        <th scope="col">Name</th>
                        <th scope="col" style={{ width: "170px" }}>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {cateList.map((item, index) =>
                    (
                        <tr key={index}>
                            <td>{item.categoryId}</td>
                            <td>
                                <Link to={`/category/${item.categoryId}`}>
                                    {item.categoryName}
                                </Link>
                            </td>
                            <td>
                                <div>
                                    <button
                                        className="btn btn-danger me-2"
                                        onClick={() => {
                                            handleDelete(item.categoryId)
                                        }}
                                    >
                                        Delete
                                    </button>
                                    {/* // <Link to={"/category/edit/" + item.bookItem.bookId}> */}
                                    <button
                                        className="btn btn-warning"
                                        onClick={() => {
                                            handleEdit(item.categoryId)
                                        }}
                                        data-toggle="modal"
                                        data-target="#modalEdit"
                                    >
                                        Edit
                                    </button>
                                    {/* // </Link> */}
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

            {/* <!-- Button trigger modal --> */}
            {/* <button type="button" className="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
                Launch demo modal
            </button> */}

            {/* <!-- Modal --> */}
            <div className="modal fade" id="modalEdit" tabIndex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="false">
                <div className="modal-dialog" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="exampleModalLabel">Edit category's name</h5>
                        </div>

                        <div className="modal-body">
                            <form>
                                <div className="form-group">
                                    <input
                                        type="text"
                                        className="form-control"
                                        id="exampleInputEmail1"
                                        aria-describedby="emailHelp"
                                        placeholder="Enter name"
                                        value={submitData.categoryName}
                                        onChange={handleNameChange}
                                    />
                                    <small id="emailHelp" className="form-text text-muted"></small>
                                </div>
                            </form>
                        </div>


                        <div className="modal-footer">
                            <button
                                type="button"
                                className="btn btn-secondary"
                                data-dismiss="modal"
                                onClick={handleCancelEdit}
                                ref={btnClose}
                            >
                                Close
                            </button>

                            <button
                                type="button"
                                className="btn btn-primary"
                                onClick={handleSubmitEdit}
                            >
                                Save changes
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            {/* <!-- Modal --> */}
            <div className="modal fade" id="modalCreate" tabIndex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="false">
                <div className="modal-dialog" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title" id="exampleModalLabel">New category's name</h5>
                        </div>

                        <div className="modal-body">
                            <form>
                                <div className="form-group">
                                    <input
                                        type="text"
                                        className="form-control"
                                        id="exampleInputEmail1"
                                        aria-describedby="emailHelp"
                                        placeholder="Enter name"
                                        value={submitData.categoryName}
                                        onChange={handleNameChange}
                                    />
                                    <small id="emailHelp" className="form-text text-muted"></small>
                                </div>
                            </form>
                        </div>


                        <div className="modal-footer">
                            <button
                                type="button"
                                className="btn btn-secondary"
                                data-dismiss="modal"
                                onClick={handleCancelCreate}
                            >
                                Close
                            </button>

                            <button
                                type="button"
                                className="btn btn-primary"
                                onClick={handleSubmitCreate}
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

export default CategoryManagerPage