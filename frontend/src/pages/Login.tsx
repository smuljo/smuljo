import React, { useState, ChangeEvent, FormEvent }  from 'react';
import {Grid} from "@mui/material";
import MyButton from "../components/MyButton";
import MyInput from "../components/MyInput";
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import AddUniversityModal from "../components/AddUniversityModal";

interface FormData {
    userName: string;
    password: string;
}

interface AuthData {
    accessToken: string;
}

function Login() {
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

        axios.post<AuthData>('https://6e70-178-204-52-103.ngrok-free.app/api/login', jsonData, {
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => {
                localStorage.setItem('accessToken', response.data.accessToken);
                navigate('/');
            })
            .catch(error => {
                console.error(error);
            });
    };

    return (
        <form onSubmit={handleSubmit}>
            <Grid container spacing={5} direction="column" justifyContent="space-between" alignItems="center" marginTop="100px">
                <Grid item>
                    <div style={{position: 'absolute', left: '50%', top: '20%', transform: 'translate(-50%, -50%)', fontSize: '39px'}}>Вход</div>
                </Grid>

                <Grid item>
                    <MyInput type="text" name="userName" label="Имя пользователя" value={formData.userName} onChange={handleChange}/>
                </Grid>

                <Grid item>
                    <MyInput type="password" name="password" label="Пароль" value={formData.password} onChange={handleChange}/>
                </Grid>

                <Grid item>
                    <MyButton type="submit" size="large" variant="contained">Войти</MyButton>
                </Grid>

                <AddUniversityModal></AddUniversityModal>

            </Grid>
        </form>
    );
}

export default Login;