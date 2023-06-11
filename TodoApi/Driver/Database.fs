module TodoApi.Driver.Database

open MySql.Data.MySqlClient
open System

let createConnection (connectionString: string) =
    let connection = new MySqlConnection(connectionString)
    connection.Open()
    connection

let getConnectionStringFromEnvironment () =
    let host = Environment.GetEnvironmentVariable("DB_HOST") // 環境変数からホスト名を取得
    let port = Environment.GetEnvironmentVariable("DB_PORT") // 環境変数からポート番号を取得
    let dbName = Environment.GetEnvironmentVariable("DB_NAME") // 環境変数からデータベース名を取得
    let user = Environment.GetEnvironmentVariable("DB_USER") // 環境変数からユーザー名を取得
    let password = Environment.GetEnvironmentVariable("DB_PASSWORD") // 環境変数からパスワードを取得
    $"Server={host};Port={port};Database={dbName};Uid={user};Pwd={password};SslMode=None"

let createDbConnection () =
    let connectionString = getConnectionStringFromEnvironment ()
    createConnection connectionString