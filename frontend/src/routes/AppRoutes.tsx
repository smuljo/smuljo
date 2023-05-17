import React from 'react';
import {Route, Routes} from "react-router-dom";
import {publicRoutes} from "./index";
import Materials from "../pages/Materials";

const AppRouter = () => {
    const token = localStorage.getItem('accessToken');

    return (
        token
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