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
        let optionTodo = Async.RunSynchronously todoAsync 
        match optionTodo with
        | Some todo ->
            let mappedTodo =
                {
                    Id = TodoId todo.Id
                    Title = TodoTitle todo.Title
                    Done = TodoDone todo.Done
                }
            Ok mappedTodo
        | None -> Error "Todo not found"