module Usecases.GetTodos

open TodoApi.Port

type GetTodosDeps =
  { GetAll: Todos.GetAll }

type TodosUsecase(deps: GetTodosDeps) =
  member this.GetAll() =
    deps.GetAll()

let getAllTodos (deps: GetTodosDeps) =
  TodosUsecase(deps).GetAll()