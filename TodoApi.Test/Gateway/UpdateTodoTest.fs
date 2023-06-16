namespace TodoApi.Test.Gateway

open NUnit.Framework
open TodoApi.Domain
open TodoApi.Driver.UpdateTodo
open Gateway.UpdateTodo
open TodoApi.Http.Response

[<TestFixture>]
module UpdateTodoTest =
    [<Test>]
    let ``Idに紐づくTodoを更新する`` () =
        let updateFunc = fun (id: int) (title: string option) (done_: bool option) ->
            async {
                return Ok { message = "ok" }
            }
            |> Async.StartAsTask

        let mockDriver = { new UpdateTodoDriver with
            member this.Update (id) (title) (done_) = updateFunc id title done_
        }

        let id = TodoId 1
        let title = TodoTitle "title1"
        let done_ = Some(TodoDone true)

        let todoGateway = { driver = mockDriver }
        let updateFunc = Update todoGateway
        let result = updateFunc id title done_

        match result with
        | Ok result -> Assert.AreEqual("ok", result.message)
        | _ -> Assert.Fail()
