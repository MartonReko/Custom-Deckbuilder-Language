import { useState } from "react";
import { ErrorReturnDto, GameApi } from "../generated-sources/openapi";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { data } from "react-router";
import { send } from "vite";

const temporaryCDL: string = `Game{
    Name: MyGame;
    Stages: [stage1, stage2];
    Player: MyCharacter;
}

Character MyCharacter{
    Health: 150;
    EffectEveryTurn: [smallHeal];
    Deck: [3x strike, 1x heal];
}

Effect smallHeal{
    Deal -1 damage instantly;
}

Stage stage1{
    Length: 2;
    Max-width: 5;
    Min-width: 2;
    FillWith: [easyNode];
    MustContain: [1x eliteNode, 2x normalNode];
    EndsWith: bossNode;
}

Stage stage2{
    Length: 2;
    Max-width: 10;
    Min-width: 5;
    FillWith: [easyNode];
    MustContain: [5x eliteNode, 10x normalNode];
    EndsWith: bossNode;
}

Node eliteNode{
    Enemies: [1x  eliteEnemy];
    Rewards: [3x rare with chance 100%];
}

Node easyNode{
    Enemies: [2x easyEnemy];
    Rewards: [
		3x common with chance 80%,
		2x rare with chance 20%
    ];
}

Node normalNode{
    Enemies: [3x easyEnemy];
    Rewards: [
		3x common with chance 50%,
		2x rare with chance 50%
    ];
}

Node bossNode{
    Enemies: [1x bossEnemy];
    Rewards: [
        3x rare with chance 100%
    ];
}

Enemy easyEnemy{
    Health: 10;
    Actions: [basicEnemyAttack player];
}

Enemy enemy_hard{
    Health: 30;
    Actions: [basicEnemyAttack player];
}

Enemy eliteEnemy{
    Health: 50;
    Actions: [basicEnemyAttack player,statusAttack player];
}

Enemy bossEnemy{
    Health: 100;
    Actions: [bossAttack player, 2x statusAttack player, bossBuff self];
}

EnemyAction bossAttack{
    Apply: [bossDamageEffect, poisonEffect];
}

EnemyAction statusAttack{
    Apply: [curseEffect, poisonEffect];
}

EnemyAction bossBuff{
    Apply: [bossBuffEffect];
}

Effect bossDamageEffect{
    Deal 20 damage instantly;
}

Effect bossBuffEffect{
    Outgoing damage is 1.25x;
    Incoming damage is 0.75x;
}

EnemyAction basicEnemyAttack{
    Apply: [2x basicAttack, poisonEffect];
}

Effect curseEffect{
    Outgoing damage is 0.75x;
    Incoming damage is 1.2x;
}

Card superCard{
    Rarity: rare;
    ValidTargets: [enemy];
    Apply: [10x basicAttack, 5x weakness, 2x poisonEffect];
}

Effect poisonEffect{
    Deal 3 damage end of turn;
}

Effect basicAttack{
    Deal baseDamage * 2 damage instantly;
}

Effect weakness{
    Outgoing damage is 0.75x;
}

int baseDamage = 5;

Card strike{
    Rarity: common;
    ValidTargets: [enemy];
    Apply: [basicAttack];
}
Card heal{
    Rarity: rare;
    ValidTargets: [player];
    Apply: [smallHeal];
}

Card poison{
    Rarity: common;
    ValidTargets: [player, enemy];
    Apply: [2x poisonEffect, weakness];
}
`;

export function Editor({ api }: { api: GameApi }) {

    const [response, setResponse] = useState<ErrorReturnDto>({ codeErrors: [], message: '' });
    const [code, setCode] = useState(temporaryCDL);

    const queryClient = useQueryClient()
    function reset() {
        api.reset().then(() => queryClient.refetchQueries())
    }

    //const { isPending: cdlPending, isError: cdlErrored, data: cdlMessage, error: cdlError } = useQuery({
    //    queryKey: ['cdlCode'],
    //    queryFn: async () => {
    //        const data = await api.readCDL({
    //            headers: {
    //                'Content-Type': 'text/plain'
    //            }
    //        }); return data.data
    //    },
    //    enabled: false,
    //})


    const sendCode = useMutation({
        mutationFn: (code: string) => {
            return api.readCDL({
                data: code,
                headers: {
                    "Content-Type": 'text/plain'
                }
            });
        },
        onSuccess: (data) => {
            console.log(data.data.message);
            console.log(data.data.codeErrors);
            //setResponse(data.data);
        },
    })
    //  function sendButton() {
    //      api.readCDL({
    //          data: code,
    //          headers: {
    //              'Content-Type': 'text/plain'
    //          }
    //      }).then((result) => {
    //          setResponse(result.data);
    //          console.log(result.data);
    //      }).catch((e) => {
    //          console.error(e);
    //          setResponse(e.response.data.message)
    //      });
    //  }


    return (
        <div className=" h-full w-full flex flex-col  p-16">
            <div>
                <h1 className="text-6xl font-bold basis-1/10">
                    CDL Editor
                </h1>
                <label>
                    Response: {response.message}
                    <br />
                    {response.codeErrors?.map((error) =>
                        <div>
                            {error}
                        </div>
                    )}
                </label>
            </div>
            <button className="text-white bg-purple-700" onClick={() => reset()}> RESET </button>
            <br />
            <label className="h-full basis-9/10 p-8">
                CDL code input: <textarea defaultValue={code} onChange={e => setCode(e.target.value)} className="h-full w-full bg-gray-600 rounded align-top p-2" name="codeField" />
                <button className="btn" onClick={() => sendCode.mutate(code)}>Send</button>
            </label>
        </div>
    )
}
