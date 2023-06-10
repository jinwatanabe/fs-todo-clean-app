namespace TodoApi

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open TodoApi.Port.Todos
open Usecases.GetTodos
open Gateway.Todos
open TodoApi.Driver.Todos

type Startup() =
    member this.ConfigureServices(services: IServiceCollection) =
        // Usecase の依存関係を登録
        services.AddSingleton<GetTodosDeps>(fun serviceProvider -> { GetAll = getAllTodos }) |> ignore

        // Driver の依存関係を登録
        services.AddSingleton<TodosDriver>(fun serviceProvider -> createMySqlTodoDriver())

        // Gateway の依存関係を登録
        services.AddSingleton<TodosGateway>() |> ignore

        // Controller の依存関係を登録
        services.AddControllers().AddControllersAsServices() |> ignore

    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        // ミドルウェアの構成などの設定

        // ルーティングの設定
        app.UseRouting() |> ignore

        // コントローラーのエンドポイントのマッピング
        app.UseEndpoints(fun endpoints ->
            endpoints.MapControllers() |> ignore
        ) |> ignore

// アプリケーションの起動
let configureApp (app: IApplicationBuilder) (env: IWebHostEnvironment) =
    let startup = Startup()
    startup.Configure(app, env)

WebHost.CreateDefaultBuilder()
    .UseStartup<Startup>()
    .Configure(configureApp)
    .Build()
    .Run()