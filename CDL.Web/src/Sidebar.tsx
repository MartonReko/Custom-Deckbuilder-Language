import { Link, NavLink } from "react-router";

export function Sidebar() {
    return (
        <div className="grid grid-rows w-64 h-screen bg-gray-900 text-white flex flex-col p-4" >
            <nav>
                <Link to="/editor">
                    <p>
                        Editor
                    </p>
                </Link>
                <Link to="/game">
                    <p>
                        Game
                    </p>
                </Link>
            </nav>
        </div >
    );
}
