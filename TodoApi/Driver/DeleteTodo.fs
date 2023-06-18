module TodoApi.Driver.DeleteTodo

open System.Threading.Tasks
open MySql.Data.MySqlClient
open Dapper
open TodoApi.Http.Response

type DeleteTodoDriver =
    abstract member Delete: int -> Task<Result<MessageResponse, string>>

type MySqlDeleteTodoDriver(connection: MySqlConnection) =
    interface DeleteTodoDriver with
        member this.Delete(id) =
            async {
                let query = "DELETE FROM todos WHERE id = @Id"
                let params_ = new DynamicParameters()
                params_.Add("@Id", id)
                let! result = connection.ExecuteAsync(query, params_) |> Async.AwaitTask
                if result = 0 then 
                    return Result.Error "Failed to delete todo"
                else
                    return Result.Ok { message = "ok" }
            } |> Async.StartAsTask

let createMySqlDeleteTodoDriver () =
    let connection = Database.createDbConnection()
    MySqlDeleteTodoDriver(connection)