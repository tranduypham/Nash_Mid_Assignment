import { createContext, useEffect, useState } from 'react';
import { BrowserRouter } from 'react-router-dom';
import './App.css';
import { AdminPage } from './Page/Admin';
import LoginPage from './Page/LoginPage/LoginPage';
import { UserPage } from './Page/User';

export const Context = createContext();

function App() {
  const [loginState, setLoginState] = useState({
    token: "",
    isLogin: false,
    isAdmin: false
  });
  console.log(loginState);

  useEffect(() => {
    if (localStorage.getItem("loginState") !== null) {
      setLoginState(JSON.parse(localStorage.getItem("loginState")));
    }
  }, [])

  return (
    <Context.Provider value={[loginState, setLoginState]}>
      <BrowserRouter>
        {
          loginState.isLogin === false ? <LoginPage /> :
            loginState.isAdmin === false ? <UserPage /> :
              <AdminPage />
        }
      </BrowserRouter>
    </Context.Provider>
  );
}

export default App;
