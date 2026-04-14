import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./styles/index.css";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import App from "./App.tsx";
import { ThemeProvider } from "@mui/material";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterLuxon } from "@mui/x-date-pickers/AdapterLuxon";
import { theme } from "./styles/theme.ts";

const queryClient = new QueryClient();

createRoot(document.getElementById("root")!).render(
    <StrictMode>
        <LocalizationProvider dateAdapter={AdapterLuxon} adapterLocale={navigator.language}>
            <ThemeProvider theme={theme}>
                <QueryClientProvider client={queryClient}>
                    <App />
                    {import.meta.env.DEV && <ReactQueryDevtools initialIsOpen={false} />}
                </QueryClientProvider>
            </ThemeProvider>
        </LocalizationProvider>
    </StrictMode>,
);
