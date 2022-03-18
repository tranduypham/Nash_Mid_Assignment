import { useContext } from "react"
import { Context } from "../App"
import { OrderContext } from "./User/UserPage"

export const useLoginState = () => {
    const [loginState, setLoginState] = useContext(Context);
    return [loginState, setLoginState];
}

export const useOrderState = () => {
    const [order, setOrder] = useContext(OrderContext);
    return [order, setOrder];
}

// export const useFetchData = ({url, method, data}) => {
//     const [loginState] = useLoginState();
//     const header = {}
//     if (loginState.token !== "") {
//         header.tokenAuth = loginState.token
//     }
//     return axios({
//         method: method,
//         url: url,
//         headers: header,
//         data: data
//     })
// }
