namespace TodoApi.Controllers

open Microsoft.AspNetCore.Http
open System.Text.Json
open TodoApi.Domain

type CreateTodoController() =
    member this.create (createTodoDeps: Usecase.CreateTodo.CreateTodoDeps) (context: HttpContext) =
        let title = context.Request.Form.["title"] |> string
        let createFunc = Usecase.CreateTodo.createTodo createTodoDeps
        let result = createFunc (TodoTitle title)
        match result with
        | Ok result ->
            let options = JsonSerializerOptions()
            let resultJsonString = JsonSerializer.Serialize(result, options)
            Results.Ok(JsonDocument.Parse(resultJsonString))
        | Error err ->
            Results.Problem(err)