import { useState } from "react";
import { GameApi } from "../generated-sources/openapi";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import playerImg from './assets/tempPlayer.png';
import enemyImg from './assets/tempEnemy.png';
//

export function Game({ api }: { api: GameApi }) {

    const [selected, setSelected] = useState('')

    const { isPending: statusPending, isError: statusErrored, data: status, error: statusError } = useQuery({
        queryKey: ['status'],
        queryFn: async () => { const data = await api.getGameState(); return data.data },
    })

    const { isPending: combatPending, isError: combatErrored, data: combat, error: combatError } = useQuery({
        queryKey: ['combat'],
        queryFn: async () => { const data = await api.combat(); return data.data },
        enabled: !!status && status.currentState === "COMBAT",
    })

    const { isPending: mapPending, isError: mapErrored, data: map, error: mapError } = useQuery({
        queryKey: ['map'],
        queryFn: async () => { const data = await api.map(); return data.data },
        enabled: !!status && status.currentState === "MAP",
    })

    const { isPending: rewardPending, isError: rewardErrored, data: reward, error: rewardError } = useQuery({
        queryKey: ['reward'],
        queryFn: async () => { const data = await api.reward(); return data.data },
        enabled: !!status && status.currentState === "REWARD",
    })

    const queryClient = useQueryClient()

    const move = useMutation({
        mutationFn: (guid: string) => { return api.move(guid); },
        onSuccess: () => {
            queryClient.refetchQueries({ queryKey: ['status'] });
        },
    })
    const endTurn = useMutation({
        mutationFn: () => { return api.endTurn(); },
        onSuccess: () => {
            queryClient.refetchQueries({ queryKey: ['status'] });
        },
    })
    if (statusPending) {
        return <span>Loading combat...</span>
    }
    if (statusErrored) {
        return <span>Error: {statusError.message}</span>
    }
    function reset() {
        api.reset().then(() => queryClient.refetchQueries())
    }
    function useCard(cardId: string, targetId: string) {
        api.playCard({ cardId, targetId }).then(() => {
            queryClient.refetchQueries()
            console.log(`Card ${cardId} was used on ${targetId}`)
        }
        );
    }

    function showStatus() {
        return <div className="">
            <button className="text-white" onClick={() => reset()}> RESET </button>
            <br />
            <button className="text-white" onClick={() => (endTurn.mutate())}> End turn </button>
            <br />
            <label>Currently in {status?.currentState}</label>
            <br />
            <br />
            {status?.deck.map((card) =>
                <span className="m-2">{`[${card.name}]`}
                    <button className="text-white" onClick={() => useCard(card.id, selected)}>Use</button>
                </span>
            )}
            <br />
            <br />
        </div>;
    }
    function subScreen() {
        switch (status?.currentState) {
            case "COMBAT":
                return <div className="grid grid-cols-2 p-4  text-[24px] font-bold bg-[url(./assets/tempBg.png)] bg-center bg-no-repeat bg-cover grow text-black" >
                    <div className="rows-2">
                        <div className="h-140 overflow-auto p-8">
                            {showStatus()}
                            <label>{`Energy: ${combat?.energy}`}</label>
                        </div>
                        <div>
                            <img src={playerImg} className="h-60" />
                            <br />
                            <label>{status?.name}</label>
                            <br />
                            <label>HP: {status?.health}</label>
                        </div>
                    </div>
                    <div className="rows-2">
                        <div className="h-140">
                            <label>Target:</label>
                            <select value={selected} onChange={e => setSelected(e.target.value)}>
                                <option value={status?.playerId}>Player</option>
                                {combat?.enemies.map(e => <option value={e.id}>
                                    {`${e.name} - ${e.id.substring(0, 4)}`}
                                </option>
                                )}
                            </select>
                            <br />
                            <br />
                        </div>
                        <div className="flex flex-row">
                            {combat?.enemies.map((enemy) => <div key={enemy.id}>
                                <img src={enemyImg} className="h-60" />
                                <label>{`Id: ${enemy.id.substring(0, 4)}`}</label>
                                <br />
                                <label>{`Name: ${enemy.name}`}</label>
                                <br />
                                <label>{`Health: ${enemy.health}`}</label>
                            </div>)
                            }
                        </div>
                    </div>
                </div >;
            case "MAP":
                return <div>
                    {showStatus()}
                    <h2>{`Current stage: ${map?.stageName}`}</h2>
                    {map?.nodes.map((x) => <div key={x.id}>
                        <button className="btn" onClick={() => move.mutate(x.id)}>{`${x.level} - ${x.name}`}</button>
                        <br />
                    </div>)}
                </div>;
            case "DEATH":
                return <div>
                    You have died. Game over.
                </div>;
            case "REWARD":
                if (rewardPending) {
                    return <span>Loading combat...</span>
                }
                if (rewardErrored) {
                    return <span>Error: {rewardError?.message}</span>
                }
                return <div>
                    {reward?.cards.map(c => <div>
                        <button className="btn">{`${c.name} - ${c.id.substring(0, 4)}`}</button>
                    </div>)}
                </div>;
            case "WIN":
                return <div>
                    You have won. Congratulations.
                </div>;
        }
    }
    return <div className="grow flex">
        {subScreen()}
    </div>
}
