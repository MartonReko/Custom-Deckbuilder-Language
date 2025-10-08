import { useState } from "react";
import { GameApi, MoveDto } from "../generated-sources/openapi";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
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

    const queryClient = useQueryClient()

    const move = useMutation({
        mutationFn: (guid: string) => { return api.move(guid); },
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
        console.log(`Card ${cardId} was used on ${targetId}`);
    }
    function endTurn() {
        //api.endTurn()
    }

    function showStatus() {
        return <div>
            <h1 className="text-4xl font-extrabold m-8">
                Game
            </h1 >
            <button className="btn m-2" onClick={() => reset()}> RESET </button>
            <br />
            <button className="btn m-2" onClick={() => endTurn()}> End turn </button>
            <br />
            <label>Player name: {status?.name}</label>
            <br />
            <label>Player health: {status?.health}</label>
            <br />
            <label>Currently in {status?.currentState}</label>
            <br />
            <br />
            {status?.deck.map((card) =>
                <span className="m-2">{`[${card.name} - ${card.id.substring(0, 4)}]`}
                    <button className="btn" onClick={() => useCard(card.id, selected)}>Use</button>
                </span>
            )}
            <br />
            <br />
        </div>;
    }
    switch (status.currentState) {
        case "COMBAT":
            return <div>
                {showStatus()}
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
                {combat?.enemies.map((enemy) => <div key={enemy.id}>
                    <label>{`Id: ${enemy.id}`}</label>
                    <br />
                    <label>{`Name: ${enemy.name}`}</label>
                    <br />
                    <label>{`Health: ${enemy.health}`}</label>
                    <br />
                </div>)
                }
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
        case "REWARD":
        case "WIN":
    }
}
