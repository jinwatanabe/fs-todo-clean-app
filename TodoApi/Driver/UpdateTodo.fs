module TodoApi.Driver.UpdateTodo

open System.Threading.Tasks
open MySql.Data.MySqlClient
open Dapper
open TodoApi.Http.Response

type UpdateTodoDriver = 
    abstract member Update: int -> string -> bool -> Task<Result<MessageResponse, string>>

type MySqlUpdateTodoDriver(connection: MySqlConnection) =
    interface UpdateTodoDriver with
        member this.Update(id) (title) (done_) =
            async {
                let query = "UPDATE todos SET title = @Title, done = @Done WHERE id = @Id"
                let params_ = new DynamicParameters()
                params_.Add("@Id", id)
                params_.Add("@Title", title)
                params_.Add("@Done", done_)
                let! result = connection.ExecuteAsync(query, params_) |> Async.AwaitTask
                if result = 0 then 
                    return Result.Error "Failed to update todo"
                else
                    return Result.Ok { message = "ok" }
            } |> Async.StartAsTask