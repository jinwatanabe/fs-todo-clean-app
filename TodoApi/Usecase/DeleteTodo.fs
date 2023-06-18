module Usecase.DeleteTodo

open TodoApi.Port.DeleteTodo
open TodoApi.Domain
open TodoApi.Http.Response

type DeleteTodoDeps =
    { Delete: Delete }

type TodoDeleteUsecase(deps: DeleteTodoDeps) =
    member this.Delete(id: TodoId) =
        deps.Delete(id)

let deleteTodo (deps: DeleteTodoDeps) (id: TodoId) : Result<MessageResponse, string> =
    TodoDeleteUsecase(deps).Delete(id)