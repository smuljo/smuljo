import React, { useState, ChangeEvent, FormEvent } from 'react';
import {Grid} from "@mui/material";
import MyButton from "../components/MyButton";
import MyInput from "../components/MyInput";
import {useNavigate} from "react-router-dom";
import axios from 'axios';

interface FormData {
    userName: string;
    password: string;
}

const Registration = () => {
    const navigate = useNavigate();

    const [formData, setFormData] = useState<FormData>({
        userName: '',
        password: ''
    });

    const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        const jsonData = JSON.stringify(formData);
        axios.post('http://localhost:5000/api/registration', jsonData, {
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                navigate('/login');
                console.log(response.data);
            })
            .catch(error => {
                console.error(error);
            });
    };

    return (
        <form onSubmit={handleSubmit}>
            <Grid container spacing={5} direction="column" justifyContent="space-between" alignItems="center" marginTop="100px">
                <Grid item>
                    <div style={{position: 'absolute', left: '50%', top: '20%', transform: 'translate(-50%, -50%)', fontSize: '39px'}}>Регистрация</div>
                </Grid>

                <Grid item>
                    <MyInput type="text" name="userName" value={formData.userName} onChange={handleChange} label="Имя пользователя"/>
                </Grid>

                <Grid item>
                    <MyInput type="password" name="password" value={formData.password} onChange={handleChange} label="Пароль"/>
                </Grid>

                <Grid item>
                    <MyInput type="password" id="confirmPassword" label="Повторите пароль"/>
                </Grid>

                <Grid item>
                    <MyButton type="submit" size="large" variant="contained">Регистрация</MyButton>
                </Grid>
            </Grid>
        </form>
    );
};

export default Registration;