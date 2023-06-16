module Usecase.UpdateTodo

open TodoApi.Port.UpdateTodo
open TodoApi.Domain
open TodoApi.Http.Response

type UpdateTodoDeps =
    { Update: Update }

type TodoUpdateUsecase(deps: UpdateTodoDeps) =
    member this.Update(id: TodoId, title: TodoTitle, done_: TodoDone) =
        deps.Update(id)(title)(done_)

let updateTodo (deps: UpdateTodoDeps) (id: TodoId) (title: TodoTitle) (done_: TodoDone) : Result<MessageResponse, string> =
    TodoUpdateUsecase(deps).Update(id, title, done_)
