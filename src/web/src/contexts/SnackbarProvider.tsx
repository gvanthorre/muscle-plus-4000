import { useState } from "react";
import { Alert, Snackbar } from "@mui/material";
import SnackbarContext from "./SnackbarContext.tsx";
import type { ReactNode } from "react";
import type { AlertSeverity } from "../types/alertSeverity.ts";

const SnackbarProvider = ({ children }: { children: ReactNode }) => {
    const [open, setOpen] = useState<boolean>(false);
    const [message, setMessage] = useState<string>("");
    const [typeColor, setTypeColor] = useState<AlertSeverity>("info");

    const showSnackbar = (text: string, color: AlertSeverity) => {
        setMessage(text);
        setTypeColor(color);
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
        setTypeColor("info");
    };

    return (
        <SnackbarContext.Provider value={{ showSnackbar }}>
            <Snackbar
                open={open}
                autoHideDuration={6000}
                anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
                onClose={handleClose}
            >
                <Alert onClose={handleClose} severity={typeColor} sx={{ whiteSpace: "pre-line" }}>
                    {message}
                </Alert>
            </Snackbar>
            {children}
        </SnackbarContext.Provider>
    );
};

export default SnackbarProvider;
