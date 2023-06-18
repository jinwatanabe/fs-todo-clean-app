module Gateway.DeleteTodo

open TodoApi.Driver.DeleteTodo
open TodoApi.Port.DeleteTodo
open TodoApi.Domain

type DeleteTodoGateway = { driver: DeleteTodoDriver }

let Delete (gateway: DeleteTodoGateway) : Delete =
    fun id -> 
        let (TodoId id) = id
        let task = gateway.driver.Delete id
        task |> Async.AwaitTask |> Async.RunSynchronously