import { Outlet, Link } from "react-router-dom";

const Layout = () => {
    return (
        <>
            <ul className="nav">
                <li className="nav-item">
                    <Link className="nav-link" to="/">Home</Link>
                </li>
                <li className="nav-item">
                    <Link className="nav-link" to="/pantry">Pantry</Link>
                </li>
                <li className="nav-item">
                    <Link className="nav-link" to="/item">Items</Link>
                </li>
            </ul>
            <Outlet />
        </>
    )
};

export default Layout;