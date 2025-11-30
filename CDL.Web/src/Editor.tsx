import { useEffect, useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { GameApi } from "../generated-sources/openapi";

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
    Length: 3;
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

Card superCard{
    Cost: 3;
    Rarity: rare;
    ValidTargets: [enemy];
    Apply: [10x basicAttack, 5x weakness, 2x poisonEffect];
}

Card strike{
    Cost: 1;
    Rarity: common;
    ValidTargets: [enemy];
    Apply: [basicAttack];
}
Card heal{
    Cost: 1;
    Rarity: rare;
    ValidTargets: [player];
    Apply: [smallHeal];
}

Card poison{
    Cost: 2;
    Rarity: common;
    ValidTargets: [player, enemy];
    Apply: [2x poisonEffect, weakness];
}
`;

export function Editor({ api }: { api: GameApi }) {

    const [response, setResponse] = useState('');
    const [code, setCode] = useState(localStorage.getItem('code') || temporaryCDL);
    const [errorsAreOld, setErrorsAreOld] = useState(false);

    useEffect(() => {
        localStorage.setItem('code', code);
    }, [code])

    const queryClient = useQueryClient()

    const { isPending: _cdlPending, isError: _cdlErrored, data: cdlMessage, error: _cdlError } = useQuery({
        queryKey: ['cdl'],
        queryFn: async () => {
            const data = await api.codeErrors();
            setErrorsAreOld(false);
            return data.data;
        },
        enabled: false,
    })

    const sendCode = useMutation({
        mutationFn: (code: string) => {
            return api.readCDL({
                data: code,
                headers: {
                    "Content-Type": 'text/plain'
                }
            });
        },
        onSuccess: () => {
            setResponse("Succesfully parsed code.");
            setErrorsAreOld(true);
        },
        onError: () => {
            queryClient.fetchQuery({ queryKey: ['cdl'] });
            setResponse("Failed to parse code.");
        }
    })
    function ResetCode() {
        setCode(temporaryCDL);
    }
    function Errors() {
        if (!errorsAreOld) {
            return <div>
                {cdlMessage?.errors.map((error) => {
                    return <div>
                        {error.errorMessage}
                        <br />
                    </div>
                }
                )}
            </div>
        }
        else {
        }
    }

    return (
        <div className=" h-full w-full flex flex-col  p-16">
            <div>
                <h1 className="text-6xl font-bold basis-1/10">
                    CDL Editor
                </h1>
                <label>
                    {response}
                    <br />
                    <br />
                    <Errors />
                </label>
            </div >
            <br />
            <label className="h-full basis-9/10 p-8">
                CDL code input: <textarea value={code} onChange={e => setCode(e.target.value)} className="h-full w-full bg-gray-600 rounded align-top p-2" name="codeField" />
                <button className="btn" onClick={() => sendCode.mutate(code)}>Send</button>
                <button className="btn" onClick={() => ResetCode()}>Reset</button>
            </label>
        </div >
    )
}
