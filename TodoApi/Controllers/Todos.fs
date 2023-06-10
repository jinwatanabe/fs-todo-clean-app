namespace TodoApi.Controllers

open Microsoft.AspNetCore.Mvc
open TodoApi.Port.Todos
open Microsoft.AspNetCore.Http

type TodosController(getAll: GetAll) =
    let getAllAction() =
        let result = getAll()
        match result with
        | Ok todos -> Results.Ok(todos)
        | Error message -> Results.Problem(message)

    member this.GetTodos() =
        getAllAction()

[<ApiController>]
[<Route("v1/todos")>]
type TodosApiController(getAll: GetAll) =
    inherit TodosController(getAll)

    member this.GetAllTodos() =
        this.GetTodos()