module Gateway.CreateTodo

open TodoApi.Driver.CreateTodo
open TodoApi.Port.CreateTodo
open TodoApi.Domain

type CreateTodoGateway = { driver: CreateTodoDriver }

let Create (gateway: CreateTodoGateway) : Create =
    fun title -> 
        let (TodoTitle title) = title
        let task = gateway.driver.Create title
        task |> Async.AwaitTask |> Async.RunSynchronously
