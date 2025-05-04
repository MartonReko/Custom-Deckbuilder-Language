import { useEffect, useState } from 'react'
import axios from 'axios'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

const API_BASE = "http://localhost:5035"

 async function getGameState() {
     try {
         const response = await axios.get(`${API_BASE}/Test`)
         return response.data
     } catch (error) {
         console.error("Error fetching state:", error);
         return null;
     }
}
/*
 async function sendTest() {
    const response = await axios.post(`${API_BASE}/test`, 1)
    return response.data
}
*/
function App() {
    const [count, setCount] = useState(0)
    const [gameState, setGameState] = useState("")
    useEffect(() => {
        async function fetchState() {
            const state = await getGameState();
            if (state) setGameState(state);
        }
        fetchState();
    }, [])

    if (!gameState) return <p>Loading...</p>

  return (
    <>
      <div>
        <a href="https://vite.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
          <p>
              Received string was {gameState}
          </p>
    </>
  )
}

export default App
