import React from 'react';
import {Grid} from "@mui/material";
import MyButton from "../components/MyButton";
import MyInput from "../components/MyInput";
import {useNavigate} from "react-router-dom";

const Registration = () => {
    const navigate = useNavigate();
    const registration = () => {
        navigate('/login');
    }

    return (
        <Grid container spacing={5} direction="column" justifyContent="space-between" alignItems="center" marginTop="100px">
            <Grid item>
                <div style={{position: 'absolute', left: '50%', top: '20%', transform: 'translate(-50%, -50%)', fontSize: '39px'}}>Регистрация</div>
            </Grid>

            <Grid item>
                <MyInput type="text" id="login" label="Email"/>
            </Grid>

            <Grid item>
                <MyInput type="password" id="password" label="Пароль"/>
            </Grid>

            <Grid item>
                <MyInput type="password" id="confirmPassword" label="Повторите пароль"/>
            </Grid>

            <Grid item>
                <MyButton onClick={registration} size="large" variant="contained">Регистрация</MyButton>
            </Grid>
        </Grid>
    );
};

export default Registration;