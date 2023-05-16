import {BrowserRouter} from "react-router-dom";
import './App.css'
import {AuthContext} from "./context";
import {useEffect, useState} from "react";
import AppRoutes from "./routes/AppRoutes";
import Navbar from "./components/Navbar";

function App() {
    const [isAuth, setIsAuth] = useState(false);

    useEffect(() => {
        if(localStorage.getItem('auth')){
            setIsAuth(true)
        }
    }, [])

    return (
        // @ts-ignore
        <AuthContext.Provider value={{
            isAuth,
            setIsAuth
        }}>
            <BrowserRouter>
                <Navbar/>
                <AppRoutes/>
            </BrowserRouter>
        </AuthContext.Provider>
    );
}

export default App;