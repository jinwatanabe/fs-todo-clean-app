module TodoApi.Port.Todos

open TodoApi.Domain

type GetAll = unit -> Result<Todo[], string>