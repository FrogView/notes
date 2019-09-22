[C# Bitwise and Bit Shift Operators](https://www.programiz.com/csharp-programming/bitwise-operators)

##### RestSharp example 
``` csharp
var client = new RestClient(BaseUrl);
var request = new RestRequest($"Get?id={id}", Method.POST);
request.AddHeader("key", key);


request.AddParameter("application/json", "data", ParameterType.RequestBody);

// easy async support
var response = await client.ExecuteTaskAsync(request);
```
