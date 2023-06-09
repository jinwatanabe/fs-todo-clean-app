module TodoApi.Controllers.Todos

open Microsoft.AspNetCore.Http

type Todo = { id: string; name: string; isComplete: bool }

let todos = [
    { id = "1"; name = "Item 1"; isComplete = true }
    { id = "2"; name = "Item 2"; isComplete = false }
    { id = "3"; name = "Item 3"; isComplete = true }
]

let getAllTodos (): IResult =
  Results.Ok todos