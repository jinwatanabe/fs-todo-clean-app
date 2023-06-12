namespace TodoApi.Test.Gateway

open NUnit.Framework
open TodoApi.Domain
open TodoApi.Driver.Todo
open Gateway.Todo

[<TestFixture>]
module TodoTest =
    [<Test>]
    let ``IDに紐づくTodoを取得できる`` () =
        let mockDriver =
            { new TodoDriver with
                member this.GetById(id) =
                    async {
                      let todo = { Id = TodoId 1; Title = TodoTitle "First"; Done = TodoDone false }
                      return todo
                    }
                    |> Async.StartAsTask
            }
        
        let id = TodoId 1
        let todoGateway = { driver = mockDriver }
        let result = GetById todoGateway id

        match result with
        | Ok todo ->
            match todo with
            | { Id = TodoId id; Title = TodoTitle title; Done = TodoDone d } ->
                Assert.AreEqual(1, id)
                Assert.AreEqual("First", title)
                Assert.AreEqual(false, d)
        | _ -> Assert.Fail()