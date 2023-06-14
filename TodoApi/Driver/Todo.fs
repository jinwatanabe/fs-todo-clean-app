module TodoApi.Driver.Todo

open System.Threading.Tasks
open TodoApi.Domain
open MySql.Data.MySqlClient
open Dapper

type TodoJson = { Id : int; Title : string; Done : bool }
type TodoDriver =
    abstract member GetById : id:int -> Task<Option<TodoJson>>

type MySqlTodoDriver(connection: MySqlConnection) =
    interface TodoDriver with
        member this.GetById(id) =
            async {
                let query = "SELECT * FROM todos WHERE id = @Id"
                let params_ = new DynamicParameters()
                params_.Add("@Id", id)
                let! result = connection.QueryAsync<TodoJson>(query, params_) |> Async.AwaitTask
                return result |> Seq.tryHead
            } |> Async.StartAsTask


let createMySqlTodoDriver () =
    let connection = Database.createDbConnection()
    MySqlTodoDriver(connection)