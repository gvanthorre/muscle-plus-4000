import Box from "@mui/material/Box";
import GlobalHeader from "../components/GlobalHeader.tsx";
import { Container, Toolbar } from "@mui/material";
import { Outlet } from "react-router-dom";

const GlobalHeaderLayout = () => {
    return (
        <Box
            id="global-header-layout"
            sx={{ flex: 1, display: "flex", flexDirection: "column" }}
        >
            <GlobalHeader />
            <Toolbar />
            <Box component="main" sx={{ flex: 1, display: "flex", flexDirection: "column", overflow: "auto" }}>
                <Container maxWidth="xl" sx={{ flex: 1, display: "flex", flexDirection: "column", py: 2 }}>
                    <Outlet />
                </Container>
            </Box>
        </Box>
    );
};

export default GlobalHeaderLayout;
