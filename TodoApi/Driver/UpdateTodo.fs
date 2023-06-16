module TodoApi.Driver.UpdateTodo

open System.Threading.Tasks
open MySql.Data.MySqlClient
open Dapper
open TodoApi.Http.Response

type UpdateTodoDriver = 
    abstract member Update: int -> option<string> -> option<bool> -> Task<Result<MessageResponse, string>>

type MySqlUpdateTodoDriver(connection: MySqlConnection) =
    interface UpdateTodoDriver with
        member this.Update(id) (titleOpt) (doneOpt) =
            async {
                let baseQuery = "UPDATE todos SET "
                let titleQuery, titleParams =
                    match titleOpt with
                    | Some title when title <> "" ->
                        "title = @Title", [ "@Title", box title ]
                    | Some _ -> // title is null or an empty string
                        "", []
                    | None ->
                        "", []
                let doneQuery, doneParams =
                    match doneOpt with
                    | Some done_ ->
                        "done = @Done", [ "@Done", box done_ ]
                    | None ->
                        "", []
                let query = baseQuery + titleQuery + (if titleQuery <> "" && doneQuery <> "" then ", " else "") + doneQuery + " WHERE id = @Id"
                let params_ = new DynamicParameters()
                params_.AddDynamicParams(dict(titleParams @ doneParams @ [ "@Id", box id ]))
                let! result = connection.ExecuteAsync(query, params_) |> Async.AwaitTask
                if result = 0 then 
                    return Result.Error "Failed to update todo"
                else
                    return Result.Ok { message = "ok" }
            } |> Async.StartAsTask

let createMySqlUpdateTodoDriver () =
    let connection = Database.createDbConnection()
    MySqlUpdateTodoDriver(connection)