namespace Usecases

open NUnit.Framework

open TodoApi.Domain
open Usecases.GetTodos

[<TestFixture>]
module GetTodosTest =
        
        [<Test>]
        let ``すべてのTodoを取得できる`` () =
          let getAll : unit -> Result<Todo[], string> =
            fun () ->
                Ok
                  [|
                    {Id = TodoId 1; Title = TodoTitle "title1"; Done = TodoDone false}
                    {Id = TodoId 2; Title = TodoTitle "title2"; Done = TodoDone true}
                  |]

          let deps =
            { GetAll = getAll}
          
          match getAllTodos deps with
          | Ok todos -> Assert.AreEqual(2, todos.Length)
          | _ -> Assert.Fail()


