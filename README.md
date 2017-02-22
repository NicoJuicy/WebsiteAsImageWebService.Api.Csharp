# WebsiteAsImageWebService.Api.Csharp
An api for WebsiteAsImageWebService in dotnet core, written in c#

## Dependencies

Have https://github.com/NicoJuicy/WebsiteAsImageWebService running somewhere ( a selfhosted nodejs website screenshot grabber)

## howto use 

Nuget: WebsiteAsImageWebService.Api --> https://www.nuget.org/packages/WebsiteAsImageWebService.Api/

```csharp
  //This will store the stream to a file
  //The api fetches the file as a memorystream, no filename is given by the server
  //Copied from the test unit, which is not async ;)
  
  var client = new WebsiteAsImageWebService.Api.WebsiteAsImageClient("localhost:8080");
  var stream = client.GetScreenshot("http://www.google.com").Result;
  using (var fileStream = File.Create("e:/testimage.png"))
  {
    stream.CopyTo(fileStream);
  }
