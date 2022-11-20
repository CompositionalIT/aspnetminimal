open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection

let app =
    let builder = WebApplication.CreateBuilder()
    builder.Services.AddEndpointsApiExplorer().AddSwaggerGen() |> ignore
    let app = builder.Build()
    app.UseSwagger().UseSwaggerUI() |> ignore
    app

/// A helper function to convert F# lambdas to an .NET Func
let handler f = System.Func<HttpContext, _> f

app.MapGet("/object", handler (fun ctx -> {| Message = "Hello, World as a JSON object!" |})) |> ignore
app.MapGet("/text", handler (fun ctx -> "Hello, World as a string!")) |> ignore

app.Run()