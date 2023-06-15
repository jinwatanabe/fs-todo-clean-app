module Gateway.CreateTodo

open TodoApi.Driver.CreateTodo
open TodoApi.Port.CreateTodo
open TodoApi.Http.Response

type CreateTodoGateway = { driver: CreateTodoDriver }

let Create (gateway: CreateTodoGateway) : Create =
    fun title ->
        try
            let task = gateway.driver.Create title
            let async = task |> Async.AwaitTask
            let result = Async.RunSynchronously async
            Result.Ok result
        with
        | ex -> Result.Error ex.Message