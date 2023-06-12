namespace Usecases

open NUnit.Framework

open TodoApi.Domain
open Usecase.GetTodo

[<TestFixture>]
module GetTodoTest =
        [<Test>]
        let ``Idに紐づくTodoを取得できる`` () =

            let id = TodoId 1

            let getById : TodoId -> Result<Todo, string> =
                fun id ->
                    Ok {Id = TodoId 1; Title = TodoTitle "title1"; Done = TodoDone false}

            let deps =
                { GetById = getById }          

            match getByIdTodo deps id with
            | Ok todo -> Assert.AreEqual(TodoId 1, todo.Id)
            | _ -> Assert.Fail()