module Gateway.UpdateTodo

open TodoApi.Driver.UpdateTodo
open TodoApi.Port.UpdateTodo
open TodoApi.Domain

type UpdateTodoGateway = { driver: UpdateTodoDriver }

let Update (gateway: UpdateTodoGateway) : Update =
        fun id title doneOpt -> 
                let (TodoId id) = id
                let titleOption = 
                        match title with
                        | TodoTitle title -> Some title
                let doneOption = 
                        match doneOpt with
                        | Some (TodoDone done_) -> Some done_
                        | None -> None
                let task = gateway.driver.Update id titleOption doneOption
                task |> Async.AwaitTask |> Async.RunSynchronously
