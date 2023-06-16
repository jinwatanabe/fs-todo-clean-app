module TodoApi.Driver.UpdateTodo

type UpdateTodoDriver = { Update: int -> string -> bool -> Async<Result<TodoApi.Http.Response.MessageResponse, string>> }