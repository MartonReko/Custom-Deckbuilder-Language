import { useEffect, useState } from "react";
import { CombatDto, GameApi, MapDto, StatusDto } from "../generated-sources/openapi";


export function Game({ api }: { api: GameApi }) {
    const [status, setStatus] = useState<StatusDto>();
    //const [mapDisplayHelper, setMapDisplayHelper] = useState<Number>(-1);
    const [map, setMap] = useState<MapDto>();
    const [combat, setCombat] = useState<CombatDto>();
    const [ok, setOk] = useState(false);
    const [msg, setMsg] = useState("");
    useEffect(() => {
        getAndUpdateState();
    }, []);

    function getAndUpdateState() {
        api.getGameState().then(response => {
            setStatus(response.data);
            setOk(true);
        }).catch((e) => {
            console.error("Returned with error " + e);
            setMsg(e.response.data)
            setOk(false);
        });
    }
    let mapDisplayHelper: Number = -1;

    function updateDH(num: Number) {
        mapDisplayHelper = num
    };

    function move(id: string) {
        api.move(id).then((result) => { console.log(result.data); getAndUpdateState(); }).catch((e) => { console.log(e.response.data) });
    }

    function decideMode() {
        if (status == null) return <> Waiting for answer </>;
        switch (status?.currentState) {
            case "COMBAT":
                api.combat().then(response => {
                    setCombat(response.data);
                    setOk(true);
                }).catch((e) => {
                    setMsg(e.response.data)
                    setOk(false);
                });
                return <>
                    {combat?.enemies.map((enemy) => <div key={enemy.id}>
                        <text>{`Id: ${enemy.id}`}</text>
                        <br />
                        <text>{`Name: ${enemy.name}`}</text>
                        <br />
                        <text>{`Health: ${enemy.health}`}</text>
                        <br />
                        <br />
                    </div>)}
                </>;
            case "MAP":
                api.map().then(response => {
                    setMap(response.data);
                    setOk(true);
                }).catch((e) => {
                    setMsg(e.response.data)
                    setOk(false);
                });
                return <>
                    {map?.stageName}
                    {map?.nodes.map((x) => <span key={x.id}>
                        {x.level === mapDisplayHelper ? (<>
                            <button className="btn" onClick={() => move(x.id)} >{`${x.name}`}</button>
                        </>) : (<>
                            <br />
                            <label className="m-6">{`${x.level}`}</label><button className="btn" onClick={() => move(x.id)} >{`${x.name}`}</button>
                            {updateDH(x.level)}
                        </>)}
                    </span>)}
                </>;
            default:
                return <> Oopsie! </>;
        }
    }

    function reset() {
        api.reset().then(() => getAndUpdateState())
    }

    return (
        <>
            <h1>
                Game
                <button className="btn" onClick={() => reset()}> RESET </button>
            </h1 >
            {ok === false ? <label>Error: {msg}</label> : <div>
                Fetched status:
                <br />
                {status?.name}
                <br />
                {status?.health}
                <br />
                {status?.currentNode}
                <br />
                {status?.currentState}
                <br />
                <br />
                <br />
                <br />
                {decideMode()}
            </div>
            }
        </>
    )
}
