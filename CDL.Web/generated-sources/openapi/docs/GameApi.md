# GameApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**gameTestGameStateGet**](#gametestgamestateget) | **GET** /Game/Test/GameState | |
|[**gameTestGetStateGet**](#gametestgetstateget) | **GET** /Game/Test/GetState | |
|[**gameTestMoveToNodePost**](#gametestmovetonodepost) | **POST** /Game/Test/MoveToNode | |

# **gameTestGameStateGet**
> GameServiceDto gameTestGameStateGet()


### Example

```typescript
import {
    GameApi,
    Configuration
} from 'restClient';

const configuration = new Configuration();
const apiInstance = new GameApi(configuration);

const { status, data } = await apiInstance.gameTestGameStateGet();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**GameServiceDto**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **gameTestGetStateGet**
> IGameDto gameTestGetStateGet()


### Example

```typescript
import {
    GameApi,
    Configuration
} from 'restClient';

const configuration = new Configuration();
const apiInstance = new GameApi(configuration);

const { status, data } = await apiInstance.gameTestGetStateGet();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**IGameDto**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **gameTestMoveToNodePost**
> gameTestMoveToNodePost(moveResponse)


### Example

```typescript
import {
    GameApi,
    Configuration,
    MoveResponse
} from 'restClient';

const configuration = new Configuration();
const apiInstance = new GameApi(configuration);

let moveResponse: MoveResponse; //

const { status, data } = await apiInstance.gameTestMoveToNodePost(
    moveResponse
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **moveResponse** | **MoveResponse**|  | |


### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

