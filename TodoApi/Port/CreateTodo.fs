module TodoApi.Port.CreateTodo

open TodoApi.Domain
open TodoApi.Http.Response

type Create = TodoTitle -> Result<MessageResponse, string>