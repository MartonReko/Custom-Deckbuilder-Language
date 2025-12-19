import { Link, NavLink } from "react-router";

export function Sidebar() {
    return (
        <div className="grid grid-rows w-64 h-screen bg-gray-900 text-white flex flex-col pt-4 pb-4 pl-1 pr-1" >
            <nav>
                <Link to="/editor">
                    <p>
                        <button className="btn bg-gray-800 text-white w-full">
                            Editor
                        </button>
                    </p>
                </Link>
                <Link to="/game">
                    <p>
                        <button className="btn bg-gray-800 text-white w-full">
                            Game
                        </button>
                    </p>
                </Link>
            </nav>
        </div >
    );
}
