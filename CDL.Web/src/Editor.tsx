import { useEffect, useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { GameApi } from "../generated-sources/openapi";

const temporaryCDL: string =
    `// Game

Game{
    Name: MyGame;
    Stages: [Stage1, Stage2];
    Player: MyCharacter;
}

// Character

Character MyCharacter{
    Health: 150;
    Deck: [3x Strike, 2x Heal];
}

// Stages

Stage Stage1{
    Length: 2;
    Max-width: 3;
    Min-width: 1;
    FillWith: [EasyNode1, EasyNode2];
    MustContain: [EasyNode2];
    EndsWith: BossNode;
}

Stage Stage2{
    Length: 3;
    Max-width: 4;
    Min-width: 2;
    FillWith: [NormalNode];
    MustContain: [2x EliteNode];
    EndsWith: BossNode;
}

// Nodes

Node EasyNode1{
    Enemies: [1x EasyEnemy];
    Rewards: [
		3x common with chance 90%,
		1x rare with chance 10%
    ];
}

Node EasyNode2{
    Enemies: [2x EasyEnemy];
    Rewards: [
		3x common with chance 90%,
		1x rare with chance 10%
    ];
}

Node NormalNode{
    Enemies: [3x EasyEnemy];
    Rewards: [
		3x common with chance 65%,
		2x rare with chance 25%
    ];
}

Node EliteNode{
    Enemies: [1x  EliteEnemy];
    Rewards: [
        3x rare with chance 70%,
        2x common with chance 30% 
    ];
}

Node BossNode{
    Enemies: [1x BossEnemy];
    Rewards: [
        3x rare with chance 100%
    ];
}

// Enemies

Enemy EasyEnemy{
    Health: 10;
    Actions: [BasicEnemyAttack player];
}

Enemy EliteEnemy{
    Health: 50;
    Actions: [BasicEnemyAttack player,StatusAttack player];
}

Enemy BossEnemy{
    Health: 100;
    Actions: [BossAttack player, 2x StatusAttack player, BossBuff self];
}

// Cards

Card Bash{
    Cost: 2;
    Rarity: rare;
    ValidTargets: [enemy];
    Apply: [5x BasicAttack, 2x Weakness];
}

Card Strike{
    Cost: 1;
    Rarity: common;
    ValidTargets: [enemy];
    Apply: [BasicAttack];
}
Card Heal{
    Cost: 1;
    Rarity: rare;
    ValidTargets: [player];
    Apply: [HealEffect];
}

Card Poison{
    Cost: 2;
    Rarity: common;
    ValidTargets: [player, enemy];
    Apply: [2x PoisonEffect, Weakness];
}

// Enemy Actions

EnemyAction BasicEnemyAttack{
    Apply: [2x BasicAttack];
}

EnemyAction BossAttack{
    Apply: [BossDamageEffect, 2x PoisonEffect];
}

EnemyAction StatusAttack{
    Apply: [CurseEffect, PoisonEffect];
}

EnemyAction BossBuff{
    Apply: [BossBuffEffect];
}

// Effects

int baseDamage = 10;

Effect BossDamageEffect{
    Deal baseDamage * 2 damage instantly;
}

Effect BossBuffEffect{
    Outgoing damage is 1.25x;
    Incoming damage is 0.75x;
}

Effect CurseEffect{
    Outgoing damage is 0.75x;
    Incoming damage is 1.2x;
}

Effect PoisonEffect{
    Deal 3 damage end of turn;
}

Effect BasicAttack{
    Deal baseDamage damage instantly;
}

Effect Weakness{
    Outgoing damage is 0.75x;
}

Effect HealEffect{
    Deal -1 * baseDamage damage instantly;
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
        onError: (error) => {
            queryClient.fetchQuery({ queryKey: ['cdl'] });
            setResponse("Failed to parse code.");
            console.log(error);
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
