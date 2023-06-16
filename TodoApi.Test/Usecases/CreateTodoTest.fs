namespace Usecases

open NUnit.Framework
open TodoApi.Domain
open Usecase.CreateTodo
open TodoApi.Http.Response

[<TestFixture>]
module CreateTodoTest =
    [<Test>]
    
    let ``Todoを作成できる`` () =
        
        let title = TodoTitle "title1"

        let create : TodoTitle -> Result<MessageResponse, string> =
            fun title ->
                Ok { message = "ok" }

        let deps =
            { Create = create } 

        match createTodo deps title with
        | Ok response -> Assert.AreEqual(response.message, "ok")
        | _ -> Assert.Fail()
    