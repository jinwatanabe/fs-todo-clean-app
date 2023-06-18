module TodoApi.Driver.CreateTodo

open System.Threading.Tasks
open MySql.Data.MySqlClient
open Dapper
open TodoApi.Http.Response

type CreateTodoDriver =
    abstract member Create: string -> Task<Result<MessageResponse, string>>

type MySqlCreateTodoDriver(connection: MySqlConnection) =
    interface CreateTodoDriver with
        member this.Create(title) =
            async {
                let query = "INSERT INTO todos (title) VALUES (@Title)"
                let params_ = new DynamicParameters()
                params_.Add("@Title", title)
                let! result = connection.ExecuteAsync(query, params_) |> Async.AwaitTask
                if result = 0 then 
                    return Result.Error "Failed to create todo"
                else
                    return Result.Ok { message = "ok" }
            } |> Async.StartAsTask

let createMySqlCreateTodoDriver () =
    let connection = Database.createDbConnection()
    MySqlCreateTodoDriver(connection)