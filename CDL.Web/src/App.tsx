import { Route, Routes } from 'react-router';
import { Sidebar } from './Sidebar';
import { Editor } from './Editor';
import { Game } from './Game';
import Welcome from './Welcome';
import { Configuration, GameApi } from '../generated-sources/openapi';

const api = new GameApi(new Configuration({ basePath: 'http://localhost:5035' }));
function App() {
    return (
        <div className="flex h-screen">
            <Sidebar />
            <main className="flex-1 flex flex-col overflow-auto">
                <Routes>
                    <Route path="/" element={<Welcome />} />
                    <Route path="/editor" element={<Editor api={api} />} />
                    <Route path="/game" element={<Game api={api} />} />
                </Routes>
            </main>
        </div>
    )
}
//Map: {gameState.map.nodesByLevel.map(name => (<p>{name}</p>))}
export default App
