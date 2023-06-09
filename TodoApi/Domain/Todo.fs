namespace TodoApi.Domain

type TodoId = TodoId of int
type TodoTitle = TodoTitle of string
type TodoDone = TodoDone of bool

type Todo =
    { Id: TodoId
      Title: TodoTitle
      Done: TodoDone}
