import { useEffect, useState } from "react";
import { GameApi, PlayCardDto } from "../generated-sources/openapi";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import playerImg from './assets/tempPlayer.png';
import enemyImg from './assets/tempEnemy.png';
import { OrbitProgress } from "react-loading-indicators";
//

export function Game({ api }: { api: GameApi }) {

    const [selected, setSelected] = useState('')
    //const [bDIsabled, setBDisabled] = useState(false)

    const { isPending: statusPending, isError: statusErrored, data: status, error: statusError } = useQuery({
        queryKey: ['status'],
        queryFn: async () => { const data = await api.getGameState(); return data.data },
        enabled: false,
    })

    const { isPending: combatPending, isError: _combatErrored, data: combat, error: _combatError } = useQuery({
        queryKey: ['combat'],
        queryFn: async () => { const data = await api.combat(); return data.data },
        enabled: false,
        //enabled: !!status && status.currentState === "COMBAT",
    })

    const { isPending: _mapPending, isError: _mapErrored, data: map, error: _mapError } = useQuery({
        queryKey: ['map'],
        queryFn: async () => { const data = await api.map(); return data.data },
        enabled: false,
        //enabled: !!status && status.currentState === "MAP",
    })

    const { isPending: rewardPending, isError: rewardErrored, data: reward, error: rewardError } = useQuery({
        queryKey: ['reward'],
        queryFn: async () => { const data = await api.reward(); return data.data },
        enabled: false,
        //enabled: !!status && status.currentState === "REWARD",
    })
    const queryClient = useQueryClient()
    function fetch(name: string) {
        queryClient.fetchQuery({ queryKey: [`${name}`] })
    }

    useEffect(() => {
        fetch('status');
        fetch('map');
        // Set selection default value to self (first option)
        //      setSelected(status?.playerId || '');
    });
    const move = useMutation({
        mutationFn: (guid: string) => { return api.move(guid); },
        onSuccess: () => {
            fetch('status');
            fetch('combat');
        },
    })
    const endTurn = useMutation({
        mutationFn: () => { return api.endTurn(); },
        onSuccess: () => {
            fetch('status');
            fetch('combat');
        },
    })
    const getReward = useMutation({
        mutationFn: (guid: string) => { return api.getReward(guid); },
        onSuccess: () => {
            fetch('status');
            fetch('combat');
        },
    })
    const playCard = useMutation({
        mutationFn: (msg: PlayCardDto) => {
            return api.playCard(msg);
        },
        onSuccess: () => {
            fetch('status');
            fetch('combat');
            setSelected('');
        }
    })
    if (statusPending) {
        return <div className="m-auto  flex items-center justify-center">
            <OrbitProgress color="#32cd32" size="medium" text="" textColor="" />
            Loading game...
        </div>
    }
    if (statusErrored) {
        return <span>Error: {statusError.message}</span>
    }
    function showStatus() {
        return <div className="">
            {status?.currentState == "COMBAT" && <button className="text-white bg-gray-700" onClick={() => (endTurn.mutate())}> End turn </button>}
            <br />
            <label>Currently in {status?.currentState}</label>
            <br />
            {status?.currentState == "COMBAT" && combat?.hand.map((card) =>
                <div className="m-2" >{`[${card.name}, ${card.cost}]`}
                    <button className="text-white bg-gray-700" onClick={() => playCard.mutate({ cardId: card.id, targetId: selected })}>Use</button>
                </div>
            )}
            <br />
            <br />
        </div>;
    }
    function showEdges() {
        return <div>
            {map?.nodes.map((x) => { return <div key={x.id}>{x.name} ({x.id.substring(0, 4)}): {map?.edges[x.id]?.map((z) => { return <span key={z.substring(1)}>{z.substring(0, 4)}, </span> })}</div> })}
        </div>
    }
    function subScreen() {
        switch (status?.currentState) {
            case "COMBAT":
                { fetch('status'); fetch('combat'); }
                if (statusPending || combatPending) {
                    return <div> Loading... </div>
                }
                else {
                    // bg-[url(./assets/tempBg.png)]
                    return <div className=" overflow-scroll flex grid grid-cols-2  text-[18px] font-bold  bg-center bg-no-repeat bg-cover grow text-white" >
                        <div className="rows-2">
                            <div className="h-1/2 overflow-auto">
                                {showStatus()}
                                <label>{`Energy: ${combat?.energy}`}</label>
                            </div>
                            <div>
                                <img src={playerImg} className="size-0.4" />
                                <br />
                                <label>{status?.name}</label>
                                <br />
                                <label>HP: {status?.health}</label>
                                <br />
                                {status.effects.map((effect) => <span key={effect.name}>{`${effect.name}-${effect.stack}`}</span>)}
                            </div>
                        </div>
                        <div className="rows-2">
                            <div className="h-1/2">
                                <label>Target:</label>
                                <select value={selected} onChange={e => setSelected(e.target.value)}>
                                    <option selected hidden > Select target</option>
                                    <option value={status?.playerId}>Player</option>
                                    {combat?.enemies.map(e => <option value={e.id}>
                                        {`${e.name} - ${e.id.substring(0, 4)}`}
                                    </option>
                                    )}
                                </select>
                                <br />
                                <br />
                            </div>
                            <div className="flex flex-row h-1/2">
                                {combat?.enemies.map((enemy) => <div key={enemy.id} className="ml-4">
                                    <img src={enemyImg} className="size-0.4" />
                                    <div className="overflow-scroll ">
                                        <label>{`Id: ${enemy.id.substring(0, 4)}`}</label>
                                        <br />
                                        <label>{`Name: ${enemy.name}`}</label>
                                        <br />
                                        <label>{`Health: ${enemy.health}`}</label>
                                        <br />
                                        <label>{`Effects:`}</label>
                                    </div>
                                    {enemy.effects.map((effect) => <span key={effect.name}>{`${effect.name}-${effect.stack}`}</span>)}
                                </div>)
                                }
                            </div>
                        </div>
                    </div >;
                }
            case "MAP":
                { fetch('status'); fetch('map') }
                return <div>
                    {showStatus()}
                    <h2>{`Current stage: ${map?.stageName}`}</h2>
                    <div className="grid grid-cols-2">
                        <div>
                            {map?.nodes.map((x) => {
                                var validMove: boolean = true;
                                if (status.currentNode != null && status.currentLevel != 0) {
                                    validMove = map?.edges[status.currentNode]?.includes(x.id);
                                }

                                if (x.level == status.currentLevel && validMove) {
                                    return <div key={x.id}>
                                        <button className="btn bg-gray-700" onClick={() => move.mutate(x.id)}>{`${x.name} (${x.id.substring(0, 4)})`}</button>
                                    </div>
                                }
                            })}
                        </div>
                        <div className="ml-16">
                            {showEdges()}
                        </div>
                    </div>
                </div>;
            case "DEATH":
                return <div>
                    You have died. Game over.
                </div>;
            case "REWARD":
                { fetch('reward') }
                if (rewardPending) {
                    return <span>Loading combat...</span>
                }
                if (rewardErrored) {
                    return <span>Error: {rewardError?.message}</span>
                }
                return <div>
                    {reward?.cards.map(c => <div>
                        <button className="btn bg-gray-700" onClick={() => getReward.mutate(c.id)}>{`${c.name} - ${c.id.substring(0, 4)}`}</button>
                    </div>)}
                </div>;
            case "WIN":
                return <div>
                    You have won. Congratulations.
                </div>;
        }
    }
    return <div className="grow flex pt-8 pr-8 pb-8">
        {subScreen()}
    </div>
}
