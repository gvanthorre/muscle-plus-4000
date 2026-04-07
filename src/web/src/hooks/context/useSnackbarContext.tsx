import { useContext } from "react";
import SnackbarContext from "../../contexts/SnackbarContext.tsx";

export const useSnackbarContext = () => {
    const context = useContext(SnackbarContext);

    if (!context) {
        throw new Error("useSnackBar must be used within an SnackBarProvider");
    }

    return context;
};
