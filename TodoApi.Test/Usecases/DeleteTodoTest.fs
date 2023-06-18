namespace Usecase

open NUnit.Framework
open TodoApi.Domain
open Usecase.DeleteTodo
open TodoApi.Http.Response

[<TestFixture>]
module DeleteTodoTest =
    [<Test>]

        let ``IDに紐づくTodoを削除できる`` () =
                
                let id = TodoId 1

                let delete : TodoId -> Result<MessageResponse, string> =
                        fun id ->
                                Ok { message = "ok" }

                let deps =
                        { Delete = delete } 

                match deleteTodo deps id with
                | Ok response -> Assert.AreEqual(response.message, "ok")
                | _ -> Assert.Fail()
        