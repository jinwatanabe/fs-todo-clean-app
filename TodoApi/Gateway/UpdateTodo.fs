module Gateway.UpdateTodo

open TodoApi.Driver.UpdateTodo
open TodoApi.Port.UpdateTodo
open TodoApi.Domain

type UpdateTodoGateway = { driver: UpdateTodoDriver }

let Update (gateway: UpdateTodoGateway) : Update =
        fun id title done_ -> 
                let (TodoId id) = id
                let (TodoTitle title) = title
                let (TodoDone done_) = done_
                let task = gateway.driver.Update id title done_
                task |> Async.RunSynchronously