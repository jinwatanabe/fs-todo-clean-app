module TodoApi.Driver.Todo

open System.Threading.Tasks
open TodoApi.Domain

type TodoDriver =
    abstract member GetById : id:int -> Task<Todo>