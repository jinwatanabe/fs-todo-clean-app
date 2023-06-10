module Usecases.GetTodos

open TodoApi.Port
open TodoApi.Domain

type GetTodosDeps =
  { GetAll: Todos.GetAll }

type TodosUsecase(deps: GetTodosDeps) =
  member this.GetAll() =
    deps.GetAll()

let getAllTodos (deps: GetTodosDeps) : Result<Todo array, string> =
  TodosUsecase(deps).GetAll()