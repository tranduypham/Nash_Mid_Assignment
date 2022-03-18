import axios from "axios";
import React, { useContext, useState } from "react";
import { Context } from "../../App";

const LOGING = {
    LOADING: 'loading',
    FAIL: 'fail',
    SUCCESS: 'success',
    NONE: 'none'
}
const LoginPage = (props) => {
    const [loginState, setLoginState] = useContext(Context);
    const [isLoging, setLoging] = useState(LOGING.NONE);
    const [login, setLogin] = useState({
        username: "",
        password: ""
    })
    const handleUsername = (event) => {
        setLoging(LOGING.NONE);
        setLogin({
            ...login,
            username: event.target.value
        })
    }
    const handlePassword = (event) => {
        setLoging(LOGING.NONE);
        setLogin({
            ...login,
            password: event.target.value
        })
    }
    const handleSubmit = (event) => {
        setLoging(LOGING.LOADING)
        event.preventDefault();
        let header = {
            // "Content-Type": "application/json"
        };
        // console.log(header);
        axios({
            method: "POST",
            url: "https://localhost:7140/api/User/login",
            headers: header,
            data: login
        }).then((res) => {
            // let token = res.data.tokenValue;
            setLoginState({
                ...loginState,
                token: res.data.tokenValue,
                isLogin: true,
                isAdmin: res.data.isAdmin
            })
            // console.log(JSON.stringify(loginState));
            localStorage.setItem("loginState", JSON.stringify({
                token: res.data.tokenValue,
                isLogin: true,
                isAdmin: res.data.isAdmin
            }));
            // setLoging(LOGING.SUCCESS);
            // console.log(loginState);
            // console.log(res.data);
        }).catch((err) => {
            setLoging(LOGING.FAIL);
            // console.log(err);
        })
    }
    return (
        <div className="login container">
            <div className="screen">
                <div className="screen__content">
                    <form className="login" onSubmit={handleSubmit}>
                        <div className="login__field">
                            <i className="login__icon fas fa-user"></i>
                            <input
                                type="text"
                                className="login__input"
                                placeholder="User name"
                                name="username"
                                id="username"
                                value={login.username}
                                onChange={handleUsername}
                            />
                        </div>
                        <div className="login__field">
                            <i className="login__icon fas fa-lock"></i>
                            <input
                                type="password"
                                className="login__input"
                                placeholder="Password"
                                name="password"
                                id="password"
                                value={login.password}
                                onChange={handlePassword}
                            />
                        </div>
                        <button className="button login__submit" disabled={isLoging === LOGING.LOADING}>
                            <span className="button__text">
                                {isLoging === LOGING.LOADING ? <>Loging in </> :
                                isLoging === LOGING.FAIL ? <div className="text-danger">Login fail</div> :
                                "Login now" }
                            </span>
                            {isLoging === LOGING.LOADING ? <img src="../../Icon/Rolling-1s-16px.svg" alt="loading img" /> :
                            <i className="button__icon fas fa-chevron-right"></i> }
                            
                        </button>
                    </form>
                </div>
                <div className="screen__background">
                    <span className="screen__background__shape screen__background__shape4"></span>
                    <span className="screen__background__shape screen__background__shape3"></span>
                    <span className="screen__background__shape screen__background__shape2"></span>
                    <span className="screen__background__shape screen__background__shape1"></span>
                </div>
            </div>
        </div>
    )
}

export default LoginPage;