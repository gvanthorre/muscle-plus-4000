import { createContext } from "react";
import type { AlertSeverity } from "../types/alertSeverity.ts";

interface SnackbarState {
    showSnackbar: (text: string, alertColor: AlertSeverity) => void;
}

const SnackbarContext = createContext<SnackbarState | null>(null);

export default SnackbarContext;
