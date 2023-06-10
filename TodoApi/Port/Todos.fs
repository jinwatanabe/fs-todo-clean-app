module TodoApi.Port.Todos

open TodoApi.Domain
open System.Threading.Tasks

type GetAll = unit -> Result<Todo[], string>