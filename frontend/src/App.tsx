import {BrowserRouter} from "react-router-dom";
import './App.css'
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
        <BrowserRouter>
            <Navbar/>
            <AppRoutes/>
        </BrowserRouter>
    );
}

export default App;