namespace TodoApi.Controllers

open Microsoft.AspNetCore.Http
open System.Text.Json
open TodoApi.Domain

type UpdateTodoController() =
    member this.update (updateTodoDeps: Usecase.UpdateTodo.UpdateTodoDeps) (context: HttpContext) (id: int) =
        let title = context.Request.Form.["title"] |> string |> TodoTitle
        let done_ = 
            match context.Request.Form.TryGetValue("done") with
            | true, value ->
                let success, parsedValue = System.Boolean.TryParse(value.ToString())
                if success then
                    Some (TodoDone parsedValue)
                else
                    None
            | false, _ -> None

        let id = TodoId id

        let updateFunc = Usecase.UpdateTodo.updateTodo updateTodoDeps
        let result = updateFunc id title done_

        match result with
        | Ok result ->
            let options = JsonSerializerOptions()
            let resultJsonString = JsonSerializer.Serialize(result, options)
            Results.Ok(JsonDocument.Parse(resultJsonString))
        | Error err ->
            Results.Problem(err)
