namespace TodoApi.Test.Gateway

open NUnit.Framework
open TodoApi.Domain
open TodoApi.Driver.CreateTodo
open Gateway.CreateTodo
open TodoApi.Http.Response

[<TestFixture>]
module CreateTodoTest =
    [<Test>]
    let ``Todoを作成できる`` () =
        let mockDriver =
            { new CreateTodoDriver with
                member this.Create(title) =
                    async {
                        return { message = "ok"}
                    }
                    |> Async.StartAsTask
            }
        
        let title = TodoTitle "First"
        let todoGateway = { driver = mockDriver }
        let createFunc = Create todoGateway
        let result = createFunc title

        match result with
        | Ok result -> Assert.AreEqual("ok", result.message)
        | _ -> Assert.Fail()