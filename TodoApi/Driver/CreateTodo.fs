module TodoApi.Driver.CreateTodo

open System.Threading.Tasks
open MySql.Data.MySqlClient
open Dapper
open TodoApi.Http.Response
open TodoApi.Domain

type CreateTodoDriver =
    abstract member Create: TodoTitle -> Task<CreateResponse>