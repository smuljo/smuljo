import React, {useContext} from 'react';
import {Grid} from "@mui/material";
import MyButton from "../components/MyButton";
import MyInput from "../components/MyInput";
import {AuthContext} from "../context";
import { useNavigate } from 'react-router-dom';

function Login() {
    const {setIsAuth} = useContext(AuthContext);
    const navigate = useNavigate();

    const login = () => {
        setIsAuth(true);
        localStorage.setItem('auth', 'true')
        navigate('/');
    }

    return (
        <form>
        <Grid container spacing={5} direction="column" justifyContent="space-between" alignItems="center" marginTop="100px">
            <Grid item>
                <div style={{position: 'absolute', left: '50%', top: '20%', transform: 'translate(-50%, -50%)', fontSize: '39px'}}>Вход</div>
            </Grid>

            <Grid item>
                <MyInput type="text" id="login" label="Email"/>
            </Grid>

            <Grid item>
                <MyInput type="password" id="password" label="Пароль"/>
            </Grid>

            <Grid item>
                <MyButton onClick={login} size="large" variant="contained">Войти</MyButton>
            </Grid>
        </Grid>
        </form>
    );
}

export default Login;