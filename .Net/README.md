[C# Bitwise and Bit Shift Operators](https://www.programiz.com/csharp-programming/bitwise-operators)

##### RestSharp example 
``` csharp
var client = new RestClient(BaseUrl);
var request = new RestRequest($"GetStrictWER_Batch?locale={locale}", Method.POST);
request.AddHeader("x-functions-key", Secret);

string recoData = GetRecoData(utteranceResults, recognitionType);

request.AddParameter("application/json", recoData, ParameterType.RequestBody);

// easy async support
var response = await client.ExecuteTaskAsync(request);
```
