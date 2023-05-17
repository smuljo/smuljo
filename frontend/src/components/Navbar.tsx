import React from 'react';
import {AppBar, Box, Button, createTheme, ThemeProvider, Toolbar, Typography} from "@mui/material";
import {cyan} from "@mui/material/colors";
import {Link, useNavigate} from "react-router-dom";

const theme = createTheme({
    palette: {
        primary: {
            main: cyan[800],
        },
        secondary: {
            main: '#d3f4fc',
        },
    },
});

const Navbar = () => {
    const navigate = useNavigate();
    const token = localStorage.getItem('accessToken');

    const logout = () => {
        localStorage.removeItem('accessToken');
        navigate('/');
    }

    return (
            token
            ?
            <Box sx={{ flexGrow: 1 }}>
                <ThemeProvider theme={theme}>
                    <AppBar position="static">
                        <Toolbar>
                            <Link to={'/'} style={{ textDecoration: 'none', color: 'inherit', marginRight: 10, flexGrow: 1, marginLeft: 5 }}>
                                <Typography variant="h6" component="div" sx={{marginLeft: 5 }}>Smuljo</Typography>
                            </Link>

                            <Button onClick={logout} color="inherit">
                                Выход
                            </Button>
                        </Toolbar>
                    </AppBar>
                </ThemeProvider>
            </Box>
            :
            <Box sx={{ flexGrow: 1 }}>
                <ThemeProvider theme={theme}>
                    <AppBar position="static">
                        <Toolbar>
                            <Link to={'/'} style={{ textDecoration: 'none', color: 'inherit', marginRight: 10, flexGrow: 1, marginLeft: 5 }}>
                                <Typography variant="h6" component="div" sx={{marginLeft: 5 }}>Smuljo</Typography>
                            </Link>

                            <Link to={'/login'} style={{ textDecoration: 'none', color: 'inherit', marginRight: 10 }}>
                                <Button color="inherit">Вход</Button>
                            </Link>

                            <Link to={'/registration'} style={{ textDecoration: 'none', color: 'inherit', marginRight: 10  }}>
                                <Button color="inherit">Регистрация</Button>
                            </Link>
                        </Toolbar>
                    </AppBar>
                </ThemeProvider>
            </Box>
    );
};

export default Navbar;