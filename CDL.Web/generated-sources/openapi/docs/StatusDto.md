# StatusDto


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**name** | **string** |  | [default to undefined]
**playerId** | **string** |  | [default to undefined]
**health** | **number** |  | [default to undefined]
**currentNode** | **string** |  | [default to undefined]
**currentLevel** | **number** |  | [default to undefined]
**currentState** | [**PlayerStates**](PlayerStates.md) |  | [default to undefined]
**deck** | [**Array&lt;CardDto&gt;**](CardDto.md) |  | [default to undefined]
**effects** | **Array&lt;any&gt;** |  | [default to undefined]

## Example

```typescript
import { StatusDto } from 'restClient';

const instance: StatusDto = {
    name,
    playerId,
    health,
    currentNode,
    currentLevel,
    currentState,
    deck,
    effects,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
