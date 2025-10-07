# GameApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**combat**](#combat) | **GET** /Game/combat | |
|[**getGameState**](#getgamestate) | **GET** /Game/status | |
|[**map**](#map) | **GET** /Game/map | |
|[**move**](#move) | **POST** /Game/move | |
|[**readCDL**](#readcdl) | **POST** /Game/readcdl | |
|[**reset**](#reset) | **POST** /Game/reset | |

# **combat**
> CombatDto combat()


### Example

```typescript
import {
    GameApi,
    Configuration
} from 'restClient';

const configuration = new Configuration();
const apiInstance = new GameApi(configuration);

const { status, data } = await apiInstance.combat();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**CombatDto**

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

# **getGameState**
> StatusDto getGameState()


### Example

```typescript
import {
    GameApi,
    Configuration
} from 'restClient';

const configuration = new Configuration();
const apiInstance = new GameApi(configuration);

const { status, data } = await apiInstance.getGameState();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**StatusDto**

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

# **map**
> MapDto map()


### Example

```typescript
import {
    GameApi,
    Configuration
} from 'restClient';

const configuration = new Configuration();
const apiInstance = new GameApi(configuration);

const { status, data } = await apiInstance.map();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**MapDto**

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

# **move**
> MoveDto move(body)


### Example

```typescript
import {
    GameApi,
    Configuration
} from 'restClient';

const configuration = new Configuration();
const apiInstance = new GameApi(configuration);

let body: string; //

const { status, data } = await apiInstance.move(
    body
);
```

### Parameters

|Name | Type | Description  | Notes|
|------------- | ------------- | ------------- | -------------|
| **body** | **string**|  | |


### Return type

**MoveDto**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json, text/json, application/*+json
 - **Accept**: text/plain, application/json, text/json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **readCDL**
> string readCDL()


### Example

```typescript
import {
    GameApi,
    Configuration
} from 'restClient';

const configuration = new Configuration();
const apiInstance = new GameApi(configuration);

const { status, data } = await apiInstance.readCDL();
```

### Parameters
This endpoint does not have any parameters.


### Return type

**string**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

# **reset**
> reset()


### Example

```typescript
import {
    GameApi,
    Configuration
} from 'restClient';

const configuration = new Configuration();
const apiInstance = new GameApi(configuration);

const { status, data } = await apiInstance.reset();
```

### Parameters
This endpoint does not have any parameters.


### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
|**200** | OK |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

