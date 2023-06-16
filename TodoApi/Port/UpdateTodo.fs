module TodoApi.Port.UpdateTodo

open TodoApi.Domain
open TodoApi.Http.Response

type Update = TodoId -> TodoTitle -> TodoDone option -> Result<MessageResponse, string>