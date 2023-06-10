module Gateway.Todos

open TodoApi.Port.Todos
open TodoApi.Driver.Todos
open TodoApi.Domain

type TodosGateway = { driver: TodosDriver }

let GetAll (gateway: TodosGateway) : GetAll =
    fun () ->
        let todosAsync =
            async {
                let! todos = gateway.driver.GetAll() |> Async.AwaitTask
                let mappedTodos =
                    todos
                    |> List.map (fun todo ->
                        {
                            Id = TodoId todo.Id
                            Title = TodoTitle todo.Title
                            Done = TodoDone todo.Done
                        }
                    )
                    |> List.toArray
                return Ok mappedTodos
            }
        Async.RunSynchronously todosAsync

