module Usecase.GetTodo

open TodoApi.Port.Todo
open TodoApi.Domain

type GetTodoDeps =
  { GetById: GetById }

type TodoUsecase(deps: GetTodoDeps) =
    member this.GetById(id: TodoId) =
        deps.GetById(id)

let getByIdTodo (deps: GetTodoDeps) (id: TodoId) : Result<Todo, string> =
    TodoUsecase(deps).GetById(id)