import type {ReactNode} from "react";

interface ProtectedRouteProps {
    children: ReactNode;
}

const ProtectedRoute = ({ children }: ProtectedRouteProps) => {
    // const { user, isLoadingUser } = useAuthContext();
    //
    // const navigate = useNavigate();
    //
    // useEffect(() => {
    //     if (!isLoadingUser) {
    //         if (!user) {
    //             navigate("/login", { replace: true });
    //             return;
    //         }
    //
    //         if (allowedRoles?.length && !allowedRoles.includes(user.profile.role)) {
    //             navigate("/forbidden", { replace: true });
    //         }
    //     }
    // }, [user, isLoadingUser, navigate, allowedRoles]);
    //
    // if (!user) return null;

    return children;
};

export default ProtectedRoute;