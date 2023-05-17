import Login from "../pages/Login";
import Materials from "../pages/Materials";
import Registration from "../pages/Registration";

export const publicRoutes = [
    {path: '/', element: <Materials/>, exact: true},
    {path: '/login', element: <Login/>, exact: true},
    {path: '/registration', element: <Registration/>, exact: true}
]