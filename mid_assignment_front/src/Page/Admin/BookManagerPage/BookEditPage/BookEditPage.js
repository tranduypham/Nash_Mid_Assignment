import axios from "axios";
import { useFormik } from "formik";
import * as Yup from 'yup';
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
    
    const submit = (data) => {
        console.log(data);
        fetchData("https://localhost:7140/api/Book/" + id, "PUT", submitData)
        .then(res => {
            alert(res.data.message);
            navigate("/book");
        }).catch(err => {
            alert("Edit fail");
        })
    }

    const formik = useFormik({
        enableReinitialize: true,
        initialValues: {
            bookName: submitData.bookName !== "" ? submitData.bookName : "",
            categoryId: submitData.categoryId !== "" ? submitData.categoryId : undefined
        },
        validationSchema: Yup.object({
            bookName: Yup.string()
                .required('Book name is required'),
            categoryId: Yup.number()
                .required("Category is required")
                .moreThan(0, "Invalid category")
        }),
        onSubmit: value => {
            submit(value);
        }
    })

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
                formik.resetForm(submitData);
                console.log("reset form");
            });
        return () => {
            console.log("clear");
        }
    }, [])



    return (
        <>
            <div className="mt-2 mb-3">
                <form onSubmit={formik.handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="exampleInputEmail1">Book's name</label>
                        <input
                            type="text"
                            className="form-control"
                            id="bookName"
                            name="bookName"
                            aria-describedby="bookNameError"
                            placeholder="Enter name"
                            value={formik.values.bookName}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                        />
                        <small id="bookNameError" className="form-text text-danger">
                            {
                                formik.touched.bookName && formik.errors.bookName ?
                                    formik.errors.bookName :
                                    null
                            }
                        </small>
                    </div>
                    <div className="form-group">
                        <label htmlFor="exampleFormControlSelect1">Category</label>
                        <select
                            className="form-control"
                            id="categoryId"
                            name="categoryId"
                            value={formik.values.categoryId}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                        >
                            <option></option>
                            {cateData.map((item, index) =>
                                (<option value={item.categoryId} key={index}>{item.categoryName}</option>)
                            )}
                        </select>
                        <small id="categoryIdError" className="form-text text-danger">
                            {
                                formik.touched.categoryId && formik.errors.categoryId ?
                                    formik.errors.categoryId :
                                    null
                            }
                        </small>
                    </div>
                    <button type="submit" className="btn btn-primary">Submit</button>
                </form>
            </div>
            <Link to="/book">Back to list</Link>
        </>
    )
}

export default BookEditPage