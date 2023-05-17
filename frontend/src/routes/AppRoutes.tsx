import React, {useContext} from 'react';
import {Route, Routes} from "react-router-dom";
import {publicRoutes} from "./index";
import {AuthContext} from "../context";
import Materials from "../pages/Materials";

const AppRouter = () => {
    const {isAuth} = useContext(AuthContext);

    return (
        isAuth
            ?
            <Routes>
                {publicRoutes.map(route =>
                    <Route
                        path={route.path}
                        element={route.element}
                        key={route.path}
                    />
                )}
                <Route path="/login" element={<Materials/>} />
                <Route path="/registration" element={<Materials/>} />
            </Routes>
            :
            <Routes>
                {publicRoutes.map(route =>
                    <Route
                        path={route.path}
                        element={route.element}
                        key={route.path}
                    />
                )}
            </Routes>

    );
};

export default AppRouter;