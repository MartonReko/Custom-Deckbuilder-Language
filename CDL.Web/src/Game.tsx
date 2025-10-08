import { useState } from "react";
import { GameApi, MoveDto } from "../generated-sources/openapi";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
//

export function Game({ api }: { api: GameApi }) {

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

    function showStatus() {
        return <div>
            <h1 className="text-4xl font-extrabold m-8">
                Game
            </h1 >
            <button className="btn" onClick={() => reset()}> RESET </button>
            <br />
            <label>Player name: {status?.name}</label>
            <br />
            <label>Player health: {status?.health}</label>
            <br />
            <label>Currently in {status?.currentState}</label>
            <br />
        </div>;
    }

    switch (status.currentState) {
        case "COMBAT":
            return <div>
                {showStatus()}
                {combat?.enemies.map((enemy) => <div key={enemy.id}>
                    <label>{`Id: ${enemy.id}`}</label>
                    <br />
                    <label>{`Name: ${enemy.name}`}</label>
                    <br />
                    <label>{`Health: ${enemy.health}`}</label>
                    <br />
                </div>)}
            </div>;
        case "MAP":
            return <div>
                {showStatus()}
                <h2>{`Current stage: ${map?.stageName}`}</h2>
                {map?.nodes.map((x) => <div key={x.id}>
                    {x.level === 99 ? (<>
                        <button className="btn" onClick={() => move.mutate(x.id)}>{`${x.name}`}</button>
                    </>) : (<>
                        <br />
                        <label className="m-6">{`${x.level}`}</label><button className="btn" onClick={() => move.mutate(x.id)}>{`${x.name}`}</button>
                    </>)}
                </div>)}
            </div>;
        case "DEATH":
        case "REWARD":
        case "WIN":
    }
}
