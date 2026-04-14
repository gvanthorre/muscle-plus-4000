import "./App.css";
import { SnackbarBridge, SnackbarProvider } from "./context/SnackbarContext.tsx";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import ProtectedRoute from "./router/ProtectedRoute.tsx";
import HomePage from "./features/home/HomePage.tsx";
import ForbiddenPage from "./features/forbidden/ForbiddenPage.tsx";
import NotFoundPage from "./features/notFound/NotFoundPage.tsx";
import GlobalHeaderLayout from "./layouts/GlobalHeaderLayout.tsx";

function App() {
    return (
        <SnackbarProvider>
            <SnackbarBridge />
            <BrowserRouter>
                <Routes>
                    <Route element={<GlobalHeaderLayout />}>
                        <Route
                            path="/"
                            element={
                                <ProtectedRoute>
                                    <HomePage />
                                </ProtectedRoute>
                            }
                        />
                        <Route
                            path="/forbidden"
                            element={
                                <ProtectedRoute>
                                    <ForbiddenPage />
                                </ProtectedRoute>
                            }
                        />
                        <Route
                            path="*"
                            element={
                                <ProtectedRoute>
                                    <NotFoundPage />
                                </ProtectedRoute>
                            }
                        />
                    </Route>
                </Routes>
            </BrowserRouter>
        </SnackbarProvider>
    );
}

export default App;
