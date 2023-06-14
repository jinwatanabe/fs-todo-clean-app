module TodoApi.Driver.Todos

open System.Threading.Tasks
open MySql.Data.MySqlClient
open Dapper

type TodoJson = { Id : int; Title : string; Done : bool }
type TodosDriver =
    abstract member GetAll : unit -> Task<TodoJson list>

type MySqlTodosDriver(connection: MySqlConnection) =
    interface TodosDriver with
        member this.GetAll() =
            async {
                let query = "SELECT * FROM todos"
                let! todos = connection.QueryAsync<TodoJson>(query) |> Async.AwaitTask
                return todos |> List.ofSeq
            }
            |> Async.StartAsTask

let createMySqlTodosDriver () =
    let connection = Database.createDbConnection()
    MySqlTodosDriver(connection)