open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http

type Handler = System.Func<HttpContext, obj>

let builder = WebApplication.CreateBuilder()
let app = builder.Build()
app.MapGet("/", Handler (fun ctx -> {| Message = "Hello, World as a JSON object!" |})) |> ignore
app.MapGet("/text", Handler (fun ctx -> "Hello, World as a string!")) |> ignore
app.Run ()