# WebsiteAsImageWebService.Api.Csharp
An api for WebsiteAsImageWebService in dotnet core, written in c#

## Dependencies

Have https://github.com/NicoJuicy/WebsiteAsImageWebService running somewhere

## howto use

  //This will store the stream to a file
  //The api fetches the file as a memorystream, no filename is given by the server
  
  var client = new WebsiteAsImageWebService.Api.WebsiteAsImageClient("localhost:8080");
  var stream = client.GetScreenshot("http://www.google.com").Result;
  using (var fileStream = File.Create("e:/testimage.png"))
  {
    stream.CopyTo(fileStream);
  }
