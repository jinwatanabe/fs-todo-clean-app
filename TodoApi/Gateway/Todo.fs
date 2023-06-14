module Gateway.Todo

open TodoApi.Driver.Todo
open TodoApi.Domain
open TodoApi.Port.Todo

type TodoGateway = { driver: TodoDriver }

let GetById (gateway: TodoGateway) : GetById = 
    fun id ->
        let (TodoId intId) = id 
        let todoTask = gateway.driver.GetById intId
        let todoAsync = todoTask |> Async.AwaitTask 
        let todo = Async.RunSynchronously todoAsync 
        let mappedTodo =
            {
                Id = todo.Id
                Title = todo.Title
                Done = todo.Done
            }
        Ok mappedTodo