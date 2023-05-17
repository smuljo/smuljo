import React from 'react';
import {TextField} from "@mui/material";

interface IMyInputProps {
    [key:string]: any
}

function MyInput({ ...props }: IMyInputProps) {
    return (
        <TextField {...props} sx={{width: 500, maxWidth: '100%'}} variant="outlined" />
    );
}

export default MyInput;