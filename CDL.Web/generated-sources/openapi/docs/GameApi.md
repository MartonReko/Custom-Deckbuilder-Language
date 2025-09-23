# GameApi

All URIs are relative to *http://localhost*

|Method | HTTP request | Description|
|------------- | ------------- | -------------|
|[**getGameState**](#getgamestate) | **GET** /Game | |
|[**readCDL**](#readcdl) | **POST** /Game | |

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

