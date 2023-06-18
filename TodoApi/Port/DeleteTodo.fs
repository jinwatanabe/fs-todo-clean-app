module TodoApi.Port.DeleteTodo

open TodoApi.Domain
open TodoApi.Http.Response

type Delete = TodoId -> Result<MessageResponse, string>