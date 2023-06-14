namespace TodoApi.Controllers

open Microsoft.AspNetCore.Http
open System.Text.Json
open TodoApi.Domain

type todoJson = {
    Id: int
    Title: string
    Done: bool
}

type TodoController() =
    member this.getById (getByIdTodoDeps: Usecase.GetTodo.GetTodoDeps) (id: TodoId) =
        let getByIdFunc = Usecase.GetTodo.getByIdTodo getByIdTodoDeps 
        let todo = getByIdFunc id
        match todo with
        | Ok todo ->
            let todoJson =
                let (TodoId id, TodoTitle title, TodoDone done_) = (todo.Id, todo.Title, todo.Done)
                { Id = id; Title = title; Done = done_}
            let options = JsonSerializerOptions()
            let todoJsonString = JsonSerializer.Serialize(todoJson, options)
            Results.Ok(JsonDocument.Parse(todoJsonString))
        | Error err ->
            Results.Problem(err)