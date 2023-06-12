module Gateway.Todo

open TodoApi.Driver.Todo
open TodoApi.Domain
open TodoApi.Port.Todo

type TodoGateway = { driver: TodoDriver }

let GetById (gateway: TodoGateway) (id: TodoId) : GetById = 
    fun (id) ->
        let todoAsync =
            async {
                let (TodoId intId) = id 
                let! todo = gateway.driver.GetById intId |> Async.AwaitTask
                let mappedTodo =
                    {
                        Id = todo.Id
                        Title = todo.Title
                        Done = todo.Done
                    }
                return Ok mappedTodo
            }
        Async.RunSynchronously todoAsync
