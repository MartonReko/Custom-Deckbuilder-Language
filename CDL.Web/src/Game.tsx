import { useEffect, useState } from "react";
import { GameApi, StatusDto } from "../generated-sources/openapi";


export function Game({ api }: { api: GameApi }) {
    const [status, setStatus] = useState<StatusDto>();
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
    }, []);
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
            </label>
            }
        </>
    )
}
