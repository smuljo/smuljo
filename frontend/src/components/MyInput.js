import React from 'react';
import {TextField} from "@mui/material";

const MyInput = (props) => {
    return (
        <TextField {...props} sx={{width: 500, maxWidth: '100%'}} variant="outlined" />
    );
};

export default MyInput;