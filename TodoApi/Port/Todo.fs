module TodoApi.Port.Todo

open TodoApi.Domain

type GetById = TodoId -> Result<Todo, string>
