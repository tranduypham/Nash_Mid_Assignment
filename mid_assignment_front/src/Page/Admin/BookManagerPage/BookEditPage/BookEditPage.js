import axios from "axios";
import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { useLoginState } from "../../../hooks";

const BookEditPage = (props) => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [loginState] = useLoginState();
    const [cateData, setCateData] = useState([]);
    const [submitData, setSubmitData] = useState({
        bookName: "",
        categoryId: ""
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
        fetchData("https://localhost:7140/api/Category", "GET")
            .then(res => {
                setCateData(res.data)
            });
        fetchData("https://localhost:7140/api/Book/" + id, "GET")
            .then(res => {
                // console.log(res.data);
                setSubmitData({
                    bookName: res.data.bookName,
                    categoryId: res.data.categoryId
                })
            });
        return () => {
            console.log("clear");
        }
    }, [])

    const handleCateSelect = (event) => {
        console.log(event.target.value)
        setSubmitData({
            ...submitData,
            categoryId: event.target.value
        })
    }

    const handleNameChang = (event) => {
        console.log(event.target.value)
        setSubmitData({
            ...submitData,
            bookName: event.target.value
        })
    }

    const handleCreate = (event) => {
        event.preventDefault();
        console.log(submitData);
        fetchData("https://localhost:7140/api/Book/" + id, "PUT", submitData)
        .then(res => {
            alert(res.data.message);
            navigate("/book");
        }).catch(err => {
            alert("Edit fail");
        })
    }

    return (
        <>
            <div className="mt-2 mb-3">
                <form onSubmit={handleCreate}>
                    <div className="form-group">
                        <label htmlFor="exampleInputEmail1">Book's name</label>
                        <input
                            type="text"
                            className="form-control"
                            id="exampleInputEmail1"
                            aria-describedby="emailHelp"
                            placeholder="Enter name"
                            value={submitData.bookName}
                            onChange={handleNameChang}
                        />
                        <small id="emailHelp" className="form-text text-muted"></small>
                    </div>
                    <div className="form-group">
                        <label htmlFor="exampleFormControlSelect1">Category</label>
                        <select
                            className="form-control"
                            id="exampleFormControlSelect1"
                            name="categoryId"
                            value={submitData.categoryId}
                            onChange={handleCateSelect}
                        >
                            <option></option>
                            {cateData.map((item, index) =>
                                (<option value={item.categoryId} key={index}>{item.categoryName}</option>)
                            )}
                        </select>
                    </div>
                    <button type="submit" className="btn btn-primary">Submit</button>
                </form>
            </div>
            <Link to="/book">Back to list</Link>
        </>
    )
}

export default BookEditPage