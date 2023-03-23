open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.DependencyInjection
open System.Threading.Tasks

let app =
    let builder = WebApplication.CreateBuilder()
    builder.Services.AddEndpointsApiExplorer().AddSwaggerGen() |> ignore
    let app = builder.Build()
    app.UseSwagger().UseSwaggerUI() |> ignore
    app

/// A helper function to convert F# lambdas to an .NET Func
let handler f = System.Func<_> f
let handlerP f = System.Func<_,_> f

type ObjectResponse = { Message : string }

// app.MapGet("/object", System.Func<Unit,ObjectResponse> (fun () -> { Message = "Hello, World as a JSON object!" })) |> ignore



app.MapGet("/text", handler (fun () -> "Hello, World as a string!")) |> ignore
app.MapGet("/hello/{name}", System.Func<string, _> (fun (name:string) -> $"Greeting, {name.ToUpper()}!")) |> ignore

type Input = { Name : string }
app.MapPost("/hello", handlerP (fun (person:Input) -> task { return person.Name.ToUpper() })) |> ignore

app.Run()