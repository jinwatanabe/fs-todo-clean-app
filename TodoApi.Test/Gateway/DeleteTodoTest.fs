namespace TodoApi.Test.Gateway

open NUnit.Framework
open TodoApi.Domain
open TodoApi.Driver.DeleteTodo
open Gateway.DeleteTodo
open TodoApi.Http.Response

[<TestFixture>]
module DeleteTodoTest =
    [<Test>]
    let ``IDに紐づくTodoを削除できる`` () =
        let mockDriver =
            { new DeleteTodoDriver with
                member this.Delete(id) =
                    async {
                        return Ok { message = "ok"}
                    }
                    |> Async.StartAsTask
            }
        
        let id = TodoId 1
        let todoGateway = { driver = mockDriver }
        let deleteFunc = Delete todoGateway
        let result = deleteFunc id

        match result with
        | Ok result -> Assert.AreEqual("ok", result.message)
        | _ -> Assert.Fail()  