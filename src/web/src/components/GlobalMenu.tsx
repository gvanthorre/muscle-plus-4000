import {
    Divider,
    Drawer,
    List,
    ListItem,
    ListItemButton,
    ListItemIcon,
    ListItemText,
} from "@mui/material";
import * as React from "react";
import Box from "@mui/material/Box";
import HomeIcon from "@mui/icons-material/Home";
import EventIcon from "@mui/icons-material/Event";
import FitnessCenterIcon from "@mui/icons-material/FitnessCenter";
import TimerIcon from "@mui/icons-material/Timer";
import LogoutIcon from "@mui/icons-material/Logout";
import { useNavigate } from "react-router-dom";

interface GlobalMenuProps {
    open: boolean;
    setOpen: (open: boolean) => void;
}

const GlobalMenu = ({ open, setOpen }: GlobalMenuProps) => {
    const navigate = useNavigate();

    const toggleDrawer = (open: boolean) => (event: React.KeyboardEvent | React.MouseEvent) => {
        if (
            event.type === "keydown" &&
            ((event as React.KeyboardEvent).key === "Tab" ||
                (event as React.KeyboardEvent).key === "Shift")
        ) {
            return;
        }

        setOpen(open);
    };

    const handleLogout = async (e: React.MouseEvent) => {
        e.preventDefault();
        // await userManager.signoutSilent();
    };

    return (
        <Drawer anchor="left" open={open} onClose={toggleDrawer(false)}>
            <Box
                display="flex"
                flexDirection="column"
                height="100%"
                width={250}
                role="presentation"
                onClick={toggleDrawer(false)}
                onKeyDown={toggleDrawer(false)}
            >
                <List
                    sx={{ display: "flex", flexDirection: "column", height: "100%", flexGrow: 1 }}
                >
                    <ListItem disablePadding onClick={() => navigate("/")}>
                        <ListItemButton>
                            <ListItemIcon><HomeIcon /></ListItemIcon>
                            <ListItemText primary="Home" />
                        </ListItemButton>
                    </ListItem>
                    <Divider />
                    <ListItem disablePadding onClick={() => navigate("/workouts")}>
                        <ListItemButton>
                            <ListItemIcon><EventIcon /></ListItemIcon>
                            <ListItemText primary="Workouts" />
                        </ListItemButton>
                    </ListItem>
                    <Divider />
                    <ListItem disablePadding onClick={() => navigate("/exercises")}>
                        <ListItemButton>
                            <ListItemIcon><FitnessCenterIcon /></ListItemIcon>
                            <ListItemText primary="Exercises" />
                        </ListItemButton>
                    </ListItem>
                    <Divider />
                    <ListItem disablePadding onClick={() => navigate("/stopwatch")}>
                        <ListItemButton>
                            <ListItemIcon><TimerIcon /></ListItemIcon>
                            <ListItemText primary="Stopwatch" />
                        </ListItemButton>
                    </ListItem>
                    <ListItem disablePadding onClick={handleLogout} sx={{ mt: "auto" }}>
                        <ListItemButton>
                            <ListItemIcon><LogoutIcon /></ListItemIcon>
                            <ListItemText primary="Log Out" />
                        </ListItemButton>
                    </ListItem>
                </List>
            </Box>
        </Drawer>
    );
};

export default GlobalMenu;
