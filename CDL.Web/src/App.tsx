import { useEffect, useState } from 'react'
import axios from 'axios'
import './App.css'

const API_BASE = "http://localhost:5035"

 async function getGameState() {
     try {
         const response = await axios.get(`${API_BASE}/Test/GameState`)
         return response.data
     } catch (error) {
         console.error("Error fetching state:", error);
         return null;
     }
}

async function moveToNode(idx: number) {
    const moveResponse: MoveResponse ={
        Index : idx
    };
    console.warn("ASD");
    const response = await axios.post(`${API_BASE}/Test/MoveToNode`, moveResponse)
    return response.data
}
interface MoveResponse {
    Index: number;
}
interface StageMap {
    nodesByLevel: string[][];
}
interface Node {
    name: string;
}
interface GameState {
    playerState: number;
    map: StageMap;
    currentNode: Node;
}
function App() {
    const [gameState, setGameState] = useState<GameState | null>(null)
    const [reload, setReload] = useState(0)
    async function fetchState() {
        const state = await getGameState();
        if (state) setGameState(state);
    }
    useEffect(() => {
        fetchState();
    }, [gameState,reload])

    if (!gameState) return <p>Loading...</p>
    async function moveAndUpdate(idx:number) {
        moveToNode(idx); setReload(prev => prev + 1)
    }
  return (
    <>
          <p>
              PlayerState: {gameState.playerState}
              <br></br>
              CurrentNode: {gameState.currentNode.name || "Empty"}
              <br></br>
              StageMap:
              {gameState.map.nodesByLevel.map((nodes, idx) => (
                  <p>
                      Level { idx}
                      {nodes.map((node, nodeIdx) => (
                          <p onClick={() => moveAndUpdate(nodeIdx)}>-- {node}-{nodeIdx}</p>
                      ))}
                  </p>
              ))}
          </p>
    </>
  )
}
//Map: {gameState.map.nodesByLevel.map(name => (<p>{name}</p>))}
export default App
