import { useEffect, useState } from "react";
import { GameApi, MapDto, StatusDto } from "../generated-sources/openapi";


export function Game({ api }: { api: GameApi }) {
    const [status, setStatus] = useState<StatusDto>();
    //const [mapDisplayHelper, setMapDisplayHelper] = useState<Number>(-1);
    const [map, setMap] = useState<MapDto>();
    const [ok, setOk] = useState(false);
    const [msg, setMsg] = useState("");
    useEffect(() => {
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
    }, []);

    let mapDisplayHelper: Number = -1;

    function updateDH(num: Number) {
        mapDisplayHelper = num
    };

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
                        {` - ${x.name}`}
                    </>) : (<>
                        <br />
                        {`${x.level} - ${x.name}`}
                        {updateDH(x.level)}
                    </>)}
                </>)}
            </label>
            }
        </>
    )
}
