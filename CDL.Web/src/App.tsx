import { Route, Routes } from 'react-router';
import { Sidebar } from './Sidebar';
import { Editor } from './Editor';
import { Game } from './Game';
import Welcome from './Welcome';
import { Configuration, GameApi } from '../generated-sources/openapi';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

const api = new GameApi(new Configuration({ basePath: 'http://localhost:5035' }));

// Create a client
const queryClient = new QueryClient()

function App() {
    return (
        <QueryClientProvider client={queryClient}>
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
        </QueryClientProvider>
    )
}
//Map: {gameState.map.nodesByLevel.map(name => (<p>{name}</p>))}
export default App
