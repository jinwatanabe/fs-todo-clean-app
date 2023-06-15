module TodoApi.Port.Todo

open TodoApi.Domain
open TodoApi.Http.Response

type GetById = TodoId -> Result<Todo, string>
type Create = TodoTitle -> Result<CreateResponse, string>
