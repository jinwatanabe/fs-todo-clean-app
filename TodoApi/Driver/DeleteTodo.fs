module TodoApi.Driver.DeleteTodo

open System.Threading.Tasks
open MySql.Data.MySqlClient
open Dapper
open TodoApi.Http.Response

type DeleteTodoDriver =
        abstract member Delete: int -> Task<Result<MessageResponse, string>>