import { useEffect, useState } from "react";
import { GameApi, MapDto, StatusDto } from "../generated-sources/openapi";


export function Game({ api }: { api: GameApi }) {
    const [status, setStatus] = useState<StatusDto>();
    //const [mapDisplayHelper, setMapDisplayHelper] = useState<Number>(-1);
    const [map, setMap] = useState<MapDto>();
    const [ok, setOk] = useState(false);
    const [msg, setMsg] = useState("");
    useEffect(() => {
        getAndUpdateState();
    }, []);

    function getAndUpdateState() {
        api.getGameState().then(response => {
            const test = response.data;
            console.log(test.currentNode);
            console.log(test.name);
            console.log(test.health);
            setStatus(response.data);
            setOk(true);
        }).catch((e) => {
            console.error(e);
            setMsg(e.response.data)
            setOk(false);
        });
        api.map().then(response => {
            setMap(response.data);
            setOk(true);
        }).catch((e) => {
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

    return (
        <>
            <h1>
                Game
            </h1>
            {ok == false ? <label>Error: {msg}</label> : <label>
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
                {map?.stageName}
                {map?.nodes.map((x) => <>
                    {x.level === mapDisplayHelper ? (<>
                        <button className="btn" onClick={() => move(x.id)}>{` - ${x.name}`}</button>
                    </>) : (<>
                        <br />
                        <button className="btn" onClick={() => move(x.id)}>{`${x.level} - ${x.name}`}</button>
                        {updateDH(x.level)}
                    </>)}
                </>)}
            </label>
            }
        </>
    )
}
