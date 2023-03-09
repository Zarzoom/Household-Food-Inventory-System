import { Outlet, Link } from "react-router-dom";

const Layout = () => {
    return (
        <>
            <ul className="nav">
                <li className="nav-item">
                    <a className="nav-link" href="/">Home</a>
                </li>
                <li className="nav-item">
                    <a className="nav-link" href="#"></a>
                </li>
                <li className="nav-item">
                    <a className="nav-link" href="#"></a>
                </li>
            </ul>
            <Outlet />
        </>
    )
};

export default Layout;