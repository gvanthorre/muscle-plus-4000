import { AppBar, IconButton, Toolbar, Typography } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import { useState } from "react";
import GlobalMenu from "./GlobalMenu.tsx";

const GlobalHeader = () => {
    // const { isStopwatchRunning } = useGlobalContext();
    // const { userFullName } = useAuthContext();

    const [openGlobalMenu, setOpenGlobalMenu] = useState<boolean>(false);

    return (
        <>
            <AppBar position="fixed">
                <Toolbar>
                    <IconButton color="inherit" onClick={() => setOpenGlobalMenu((prev) => !prev)}>
                        <MenuIcon />
                    </IconButton>
                    <Typography
                        variant="h6"
                        sx={{
                            flexGrow: 1,
                            textAlign: "center",
                        }}
                    >
                        Track workouts and become a beefcake!
                    </Typography>
                </Toolbar>
            </AppBar>
            <GlobalMenu open={openGlobalMenu} setOpen={setOpenGlobalMenu} />
        </>
    );
};

export default GlobalHeader;
