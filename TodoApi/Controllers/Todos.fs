namespace TodoApi.Controllers

open Microsoft.AspNetCore.Http
open System.Text.Json
open TodoApi.Domain


type TodosController() =
    member this.getAll (getTodosDeps: Usecase.GetTodos.GetTodosDeps) =
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