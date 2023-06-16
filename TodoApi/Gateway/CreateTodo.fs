module Gateway.CreateTodo

open TodoApi.Driver.CreateTodo
open TodoApi.Port.CreateTodo
open TodoApi.Http.Response

type CreateTodoGateway = { driver: CreateTodoDriver }

let Create (gateway: CreateTodoGateway) : Create =
    fun title -> 
        let task = gateway.driver.Create title
        task |> Async.AwaitTask |> Async.RunSynchronously
