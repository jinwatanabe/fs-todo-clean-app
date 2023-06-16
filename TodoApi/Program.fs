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

        app.MapGet("/v1/systems/ping", Func<IResult> (fun _ -> Systems.ping()))

        app.MapGet("/v1/todos",
            Func<IResult>(fun _ ->
                let driver = Driver.Todos.createMySqlTodosDriver()
                let gateway = { Gateway.Todos.TodosGateway.driver = driver}
                let getTodosDeps: Usecase.GetTodos.GetTodosDeps = {
                    GetAll =
                        Gateway.Todos.GetAll(gateway)
                }
                let todosController = TodosController()
                todosController.getAll getTodosDeps
            ))

        app.MapGet("v1/todos/{id:int}", 
            Func<HttpContext, int, IResult>(fun context id -> 
                let driver = Driver.Todo.createMySqlTodoDriver()
                let gateway = { Gateway.Todo.TodoGateway.driver = driver }
                let getByIdTodoDeps: Usecase.GetTodo.GetTodoDeps = {
                    GetById =
                        Gateway.Todo.GetById(gateway)
                }
                let todoController = TodoController()
                todoController.getById getByIdTodoDeps (TodoId id)
            ))
        
        app.MapPost("/v1/todos",
            Func<HttpContext, IResult>(fun context ->
                let driver = Driver.CreateTodo.createMySqlCreateTodoDriver()
                let gateway = { Gateway.CreateTodo.driver = driver }
                let createTodoDeps: Usecase.CreateTodo.CreateTodoDeps = {
                    Create =
                        Gateway.CreateTodo.Create(gateway)
                }
                let todoController = CreateTodoController()
                todoController.create createTodoDeps context
            ))
        
        app.MapPut("/v1/todos/{id:int}",
            Func<HttpContext, int, IResult>(fun context id ->
                let driver = Driver.UpdateTodo.createMySqlUpdateTodoDriver()
                let gateway = { Gateway.UpdateTodo.driver = driver }
                let updateTodoDeps: Usecase.UpdateTodo.UpdateTodoDeps = {
                    Update =
                        Gateway.UpdateTodo.Update(gateway)
                }
                let todoController = UpdateTodoController()
                todoController.update updateTodoDeps context id
            ))

        app.Run()

        exitCode
