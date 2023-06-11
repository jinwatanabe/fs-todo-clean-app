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
open FSharp.SystemTextJson
open TodoApi.Domain

module Program =
    open System.Text.Json
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddControllers()

        let app = builder.Build()

        app.UseHttpsRedirection()

        // app.MapControllers()

        app.MapGet("/v1/systems/ping", Func<IResult> (fun _ -> Systems.ping()))

        app.MapGet("/v1/todos",
            Func<IResult>(fun _ ->
                let driver = TodoApi.Driver.Todos.createMySqlTodoDriver()
                let gateway = { Gateway.Todos.TodosGateway.driver = driver}
                let getTodosDeps: Usecase.GetTodos.GetTodosDeps = {
                    GetAll =
                        Gateway.Todos.GetAll(gateway)
                }

                let todos = Usecase.GetTodos.getAllTodos getTodosDeps
                match todos with
                | Ok todos -> 
                    let todosJson =
                        todos
                        |> Array.map (fun todo -> 
                            let (TodoId id, TodoTitle title , TodoDone done_) = (todo.Id, todo.Title, todo.Done)
                            {| Id = id; Title = title; Done = done_ |}
                        )
                    let options = JsonSerializerOptions()
                    let todosJsonString = JsonSerializer.Serialize(todosJson, options)
                    Results.Ok(JsonDocument.Parse(todosJsonString))
                | Error err ->
                    Results.Problem(err)
            ))


        app.Run()

        exitCode
