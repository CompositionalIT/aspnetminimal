open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection

let builder =
    let builder = WebApplication.CreateBuilder()
    builder.Services.AddEndpointsApiExplorer().AddSwaggerGen() |> ignore
    builder

type Message = { Message : string }

let app = builder.Build()

do
    let handler f = System.Func<HttpContext, _> f
    app.MapGet("/object", handler (fun ctx -> { Message = "Hello, World as a JSON object!" })) |> ignore
    app.MapGet("/text", handler (fun ctx -> "Hello, World as a string!")) |> ignore

do
    app.UseSwagger().UseSwaggerUI() |> ignore

app.Run()