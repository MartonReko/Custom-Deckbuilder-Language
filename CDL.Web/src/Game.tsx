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


    // function move(id: string) {
    //     api.move(id).then((result) => { console.log(result.data); }).catch((e) => { console.log(e.response.data) });
    // }

    function reset() {
        api.reset().then()
    }

    function showStatus() {
        return <div>
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
    //    return (
    //        <>
    //            <h1>
    //                Game
    //                <button className="btn" onClick={() => reset()}> RESET </button>
    //            </h1 >
    //            <div>
    //                <br />
    //            </div>
    //        </>
    //    )
}
//export function Combat({ api }: { api: GameApi }) {
//    const { isPending, isError, data: combat, error } = useQuery({
//        queryKey: ['combat'],
//        queryFn: async () => { const data = await api.combat(); return data.data },
//    })
//
//    if (isPending) {
//        return <span>Loading combat...</span>
//    }
//    if (isError) {
//        return <span>Error: {error.message}</span>
//    }
//    return <div>
//        {combat.enemies.map((enemy) => <div key={enemy.id}>
//            <label>{`Id: ${enemy.id}`}</label>
//            <br />
//            <label>{`Name: ${enemy.name}`}</label>
//            <br />
//            <label>{`Health: ${enemy.health}`}</label>
//            <br />
//            <br />
//            <Deck api={api} />
//        </div>)}
//    </div>;
//}
//export function Map({ api }: { api: GameApi }) {
//    const { isPending, isError, data, error } = useQuery({
//        queryKey: ['map'],
//        queryFn: async () => { const data = await api.map(); return data.data },
//    })
//
//    if (isPending) {
//        return <span>Loading map...</span>
//    }
//    if (isError) {
//        return <span>Error: {error.message}</span>
//    }
//    return <div>
//        <h2>{`Current stage: ${data.stageName}`}</h2>
//        {data.nodes.map((node) => <div key={node.id}>
//            <label>{`Id: ${node.id}`}</label>
//            <br />
//            <label>{`Name: ${node.name}`}</label>
//            <br />
//            <label>{`Level: ${node.level}`}</label>
//            <br />
//        </div>)}
//    </div>;
//}
//export function Deck({ api }: { api: GameApi }) {
//    return <>Deck will be here</>;
//}
//export function StatusWindow({ api }: { api: GameApi }) {
//    const { isPending, isError, data, error } = useQuery({
//        queryKey: ['state'],
//        // TODO: Find a fix dor data.data :(
//        queryFn: async () => { const data = await api.getGameState(); return data.data },
//    })
//
//    if (isPending) {
//        return <span>Loading...</span>
//    }
//    if (isError) {
//        return <span>Error: {error.message}</span>
//    }
//    return <div>
//        <label>Player name: {data.name}</label>
//        <br />
//        <label>Player health: {data.health}</label>
//        <br />
//        <label>Currently in {data.currentState}</label>
//        <br />
//    </div>;
//}
