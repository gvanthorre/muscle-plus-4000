import { createTheme, responsiveFontSizes } from "@mui/material/styles";
import type {} from "@mui/x-date-pickers/themeAugmentation";

let theme = createTheme({
    typography: {
        h2: {
            fontSize: "3.75rem",
            "@media (max-width:900px)": {
                fontSize: "2.5rem",
            },
            "@media (max-width:600px)": {
                fontSize: "2rem",
            },
            "@media (max-width:450px)": {
                fontSize: "1.5rem",
            },
        },
    },
    components: {
        MuiTextField: {
            defaultProps: {
                variant: "outlined",
                fullWidth: true,
            },
        },
        MuiButton: {
            defaultProps: {
                variant: "contained",
            },
        },
        MuiDatePicker: {
            defaultProps: {
                slotProps: {
                    textField: {
                        fullWidth: true,
                    },
                },
            },
        },
        MuiDateTimePicker: {
            defaultProps: {
                slotProps: {
                    textField: {
                        fullWidth: true,
                    },
                },
            },
        },
    },
});

theme = responsiveFontSizes(theme);

export { theme };
