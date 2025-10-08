## restClient@6.9.0

This generator creates TypeScript/JavaScript client that utilizes [axios](https://github.com/axios/axios). The generated Node module can be used in the following environments:

Environment
* Node.js
* Webpack
* Browserify

Language level
* ES5 - you must have a Promises/A+ library installed
* ES6

Module system
* CommonJS
* ES6 module system

It can be used in both TypeScript and JavaScript. In TypeScript, the definition will be automatically resolved via `package.json`. ([Reference](https://www.typescriptlang.org/docs/handbook/declaration-files/consumption.html))

### Building

To build and compile the typescript sources to javascript use:
```
npm install
npm run build
```

### Publishing

First build the package then run `npm publish`

### Consuming

navigate to the folder of your consuming project and run one of the following commands.

_published:_

```
npm install restClient@6.9.0 --save
```

_unPublished (not recommended):_

```
npm install PATH_TO_GENERATED_PACKAGE --save
```

### Documentation for API Endpoints

All URIs are relative to *http://localhost*

Class | Method | HTTP request | Description
------------ | ------------- | ------------- | -------------
*CDLGameApi* | [**rootGet**](docs/CDLGameApi.md#rootget) | **GET** / | 
*GameApi* | [**combat**](docs/GameApi.md#combat) | **GET** /Game/combat | 
*GameApi* | [**getGameState**](docs/GameApi.md#getgamestate) | **GET** /Game/status | 
*GameApi* | [**map**](docs/GameApi.md#map) | **GET** /Game/map | 
*GameApi* | [**move**](docs/GameApi.md#move) | **POST** /Game/move | 
*GameApi* | [**playCard**](docs/GameApi.md#playcard) | **POST** /Game/playCard | 
*GameApi* | [**readCDL**](docs/GameApi.md#readcdl) | **POST** /Game/readcdl | 
*GameApi* | [**reset**](docs/GameApi.md#reset) | **POST** /Game/reset | 
*GameApi* | [**reward**](docs/GameApi.md#reward) | **GET** /Game/reward | 


### Documentation For Models

 - [CardDto](docs/CardDto.md)
 - [CombatDto](docs/CombatDto.md)
 - [EnemyDto](docs/EnemyDto.md)
 - [MapDto](docs/MapDto.md)
 - [MoveDto](docs/MoveDto.md)
 - [NodeDto](docs/NodeDto.md)
 - [PlayCardDto](docs/PlayCardDto.md)
 - [PlayerStates](docs/PlayerStates.md)
 - [RewardDto](docs/RewardDto.md)
 - [StatusDto](docs/StatusDto.md)


<a id="documentation-for-authorization"></a>
## Documentation For Authorization

Endpoints do not require authorization.

