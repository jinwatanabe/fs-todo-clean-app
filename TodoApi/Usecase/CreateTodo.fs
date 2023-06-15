module Usecase.CreateTodo

open TodoApi.Port.Todo
open TodoApi.Domain
open TodoApi.Http.Response


type CreateTodoDeps =
    { Create: Create }

type TodoCreateUsecase(deps: CreateTodoDeps) =
    member this.Create(title: TodoTitle) =
        deps.Create(title)

let createTodo (deps: CreateTodoDeps) (title: TodoTitle) : Result<CreateResponse, string> =
    TodoCreateUsecase(deps).Create(title)