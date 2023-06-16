namespace Usecases

open NUnit.Framework
open Usecase.UpdateTodo
open TodoApi.Domain
open TodoApi.Http.Response

[<TestFixture>]
module UpdateCreateTest =
    [<Test>]

    let ``Idに紐づくTodoを更新する`` () =
        let id = TodoId 1
        let title = TodoTitle "title1"
        let done_ = TodoDone true

        let update : TodoId -> TodoTitle -> TodoDone -> Result<MessageResponse, string> =
            fun id title done_ ->
                Ok { message = "ok" }

        let deps =
            { Update = update }

        match updateTodo deps id title done_ with
        | Ok response -> Assert.AreEqual(response.message, "ok")
        | _ -> Assert.Fail()