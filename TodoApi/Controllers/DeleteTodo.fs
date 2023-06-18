namespace TodoApi.Controllers

open Microsoft.AspNetCore.Http
open System.Text.Json
open TodoApi.Domain

type DeleteTodoController() =
    member this.delete (deleteTodoDeps: Usecase.DeleteTodo.DeleteTodoDeps) (context: HttpContext) =
        let id = context.Request.RouteValues.["id"] |> string |> int
        let deleteFunc = Usecase.DeleteTodo.deleteTodo deleteTodoDeps
        let result = deleteFunc (TodoId id)
        match result with
        | Ok result ->
            let options = JsonSerializerOptions()
            let resultJsonString = JsonSerializer.Serialize(result, options)
            Results.Ok(JsonDocument.Parse(resultJsonString))
        | Error err ->
            Results.Problem(err)