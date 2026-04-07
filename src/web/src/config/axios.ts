import axios, { AxiosError } from "axios";
import type { Problem } from "../types/problem.ts";
import { notify } from "../services/snackbarService.ts";
// import { userManager } from "../features/auth/userManager.ts";

export const apiClient = axios.create({
    baseURL: import.meta.env.VITE_API_URL ?? "api",
});

// apiClient.interceptors.request.use(async (config) => {
//     const user = await userManager.getUser();
//
//     if (user?.access_token) {
//         config.headers.Authorization = `Bearer ${user.access_token}`;
//     }
//
//     return config;
// });

apiClient.interceptors.response.use(
    (response) => response,
    (error: AxiosError<Problem>) => {
        const status = error.response?.status;

        if (status === 400) {
            const errors = error.response?.data.errors;
            const joinedErrorsText = errors ? Object.values(errors).join("\n") : "";

            console.error("400 Bad Request:", error.response?.data);
            notify(`Invalid request. Please check your input.\n${joinedErrorsText}`, "error");
        }

        if (status === 401) {
            notify("You are not authenticated.", "warning");
        }

        if (status === 403) {
            notify("You are not allowed to do this.", "error");
        }

        if (status && status >= 500) {
            console.error("Server error:", error);
            notify("Server error. Please try again later.", "error");
        }

        return Promise.reject(error);
    },
);
