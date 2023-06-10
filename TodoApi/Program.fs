namespace TodoApi

#nowarn "20"
open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.AspNetCore.Http
open TodoApi.Controllers

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddControllers()

        let app = builder.Build()

        app.UseHttpsRedirection()

        // app.MapControllers()

        app.MapGet("/v1/systems/ping", Func<IResult> (fun _ -> Systems.ping()))

        app.MapGet("/v1/todos", Func<IResult> TodosApiController(getAllTodos) |> fun controller -> controller.GetAllTodos() |> Async.AwaitTask |> Async.RunSynchronously)        

        app.Run()

        exitCode
