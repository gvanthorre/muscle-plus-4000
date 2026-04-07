import { registerSnackbar } from "../services/snackbarService.ts";
import { useSnackbarContext } from "../hooks/context/useSnackbarContext.ts";
import { useEffect } from "react";

const SnackbarBridge = () => {
    const { showSnackbar } = useSnackbarContext();

    useEffect(() => {
        registerSnackbar(showSnackbar);
    }, [showSnackbar]);

    return null;
};

export default SnackbarBridge;
