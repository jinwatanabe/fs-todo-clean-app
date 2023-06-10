namespace TodoApi.Driver.Todos

open System.Threading.Tasks

type Todo = { Id : int; Title : string; Done : bool }
type TodosDriver = { GetAll : unit -> Task<Todo list> }
