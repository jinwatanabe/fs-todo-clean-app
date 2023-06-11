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
    open System.Text.Json
    let exitCode = 0

    [<EntryPoint>]
    let main args =

        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddControllers()

        let app = builder.Build()

        app.UseHttpsRedirection()

        app.MapGet("/v1/systems/ping", Func<IResult> (fun _ -> Systems.ping()))

        app.MapGet("/v1/todos",
            Func<IResult>(fun _ ->
                let driver = TodoApi.Driver.Todos.createMySqlTodoDriver()
                let gateway = { Gateway.Todos.TodosGateway.driver = driver}
                let getTodosDeps: Usecase.GetTodos.GetTodosDeps = {
                    GetAll =
                        Gateway.Todos.GetAll(gateway)
                }
                let todosController = TodosController()
                todosController.getAll getTodosDeps
            ))


        app.Run()

        exitCode
