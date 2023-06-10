namespace Gateway

open NUnit.Framework
open Gateway.Todos
open TodoApi.Domain
open TodoApi.Driver.Todos
open System.Threading.Tasks

[<TestFixture>]
module TodosTest =

    [<Test>]
    let ``すべてのTodoを取得できる`` () =

        let mockDriver : TodosDriver = {
            GetAll = fun () ->
            [
                { Id = 1; Title = "First"; Done = false }
                { Id = 2; Title = "Second"; Done = true }
            ]
            |> Task.FromResult
        }

        let todosGateway = { driver = mockDriver }
        let result = GetAll todosGateway ()

        match result with
        | Ok todos ->
            Assert.AreEqual(2, todos.Length)
            let firstTodo = todos.[0]
            match firstTodo with
            | { Id = TodoId id; Title = TodoTitle title; Done = TodoDone d } ->
                Assert.AreEqual(1, id)
                Assert.AreEqual("First", title)
                Assert.AreEqual(false, d)
        | _ -> Assert.Fail()