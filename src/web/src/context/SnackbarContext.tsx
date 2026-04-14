import {createContext, type ReactNode, useContext, useEffect, useState} from "react";
import type { AlertSeverity } from "../types/alertSeverity.ts";
import {Alert, Snackbar} from "@mui/material";
import {registerSnackbar} from "../services/snackbarService.ts";

interface SnackbarState {
    showSnackbar: (text: string, alertColor: AlertSeverity) => void;
}

const SnackbarContext = createContext<SnackbarState | null>(null);

export const SnackbarProvider = ({ children }: { children: ReactNode }) => {
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

export const useSnackbarContext = () => {
    const context = useContext(SnackbarContext);

    if (!context) {
        throw new Error("useSnackBar must be used within an SnackBarProvider");
    }

    return context;
};

export const SnackbarBridge = () => {
    const { showSnackbar } = useSnackbarContext();

    useEffect(() => {
        registerSnackbar(showSnackbar);
    }, [showSnackbar]);

    return null;
};
