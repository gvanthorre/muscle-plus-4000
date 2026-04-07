import type { AlertSeverity } from "../types/alertSeverity.ts";

let showSnackbar: ((text: string, alertColor: AlertSeverity) => void) | null = null;

export const registerSnackbar = (fn: (text: string, alertColor: AlertSeverity) => void) => {
    showSnackbar = fn;
};

export const notify = (text: string, alertColor: AlertSeverity = "info") => {
    showSnackbar?.(text, alertColor);
};
