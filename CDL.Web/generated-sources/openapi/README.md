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
*GameApi* | [**getGameState**](docs/GameApi.md#getgamestate) | **GET** /Game/status | 
*GameApi* | [**map**](docs/GameApi.md#map) | **GET** /Game/map | 
*GameApi* | [**readCDL**](docs/GameApi.md#readcdl) | **POST** /Game | 


### Documentation For Models

 - [MapDto](docs/MapDto.md)
 - [NodeDto](docs/NodeDto.md)
 - [PlayerStates](docs/PlayerStates.md)
 - [StatusDto](docs/StatusDto.md)


<a id="documentation-for-authorization"></a>
## Documentation For Authorization

Endpoints do not require authorization.

