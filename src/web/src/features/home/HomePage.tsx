import CenteredContentLayout from "../../layouts/CenteredContentLayout";
import { Box } from "@mui/material";
import logo from "../../assets/beefcake.png";

const HomePage = () => {
    return (
        <CenteredContentLayout>
            <Box
                component="img"
                src={logo}
                alt="Muscle +4000"
                sx={{
                    maxWidth: "100%",
                    minWidth: 0,
                    height: "auto",
                    display: "block",
                    px: { xs: 2, sm: 0 },
                }}
            />
        </CenteredContentLayout>
    );
};

export default HomePage;
