import type {ReactElement} from "react";
import {Box} from "@mui/material";

interface CenteredContentLayoutProps {
    children: ReactElement | ReactElement[];
}

const CenteredContentLayout = ({children}: CenteredContentLayoutProps) => {
    return (
        <Box
            sx={{
                flex: 1,
                display: "flex",
                flexDirection: "row",
                alignItems: "center",
                justifyContent: "center",
                textAlign: "center",
            }}
        >
            {children}
        </Box>
    );
};

export default CenteredContentLayout;