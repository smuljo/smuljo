import React from 'react';
import {styled} from "@mui/material/styles";
import {Button} from "@mui/material";
import {cyan, purple} from "@mui/material/colors";

const ColorButton = styled(Button)(({ theme }) => ({
    color: theme.palette.getContrastText(purple[500]),
    backgroundColor: cyan[800],
    '&:hover': {
        backgroundColor: cyan[900],
    },
}));

function MyButton({ children, ...props }) {
    return (
        <ColorButton {...props} size="large" variant="contained">
            {children}
        </ColorButton>
    );
}

export default MyButton;