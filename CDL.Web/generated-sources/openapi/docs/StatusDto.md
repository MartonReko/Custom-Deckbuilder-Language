# StatusDto


## Properties

Name | Type | Description | Notes
------------ | ------------- | ------------- | -------------
**name** | **string** |  | [default to undefined]
**health** | **number** |  | [default to undefined]
**currentNode** | **string** |  | [default to undefined]
**currentState** | [**PlayerStates**](PlayerStates.md) |  | [default to undefined]
**deck** | [**Array&lt;CardDto&gt;**](CardDto.md) |  | [default to undefined]

## Example

```typescript
import { StatusDto } from 'restClient';

const instance: StatusDto = {
    name,
    health,
    currentNode,
    currentState,
    deck,
};
```

[[Back to Model list]](../README.md#documentation-for-models) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to README]](../README.md)
