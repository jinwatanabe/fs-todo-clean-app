namespace TodoApi.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.AspNetCore.Http

module Systems =

    type PingResponse = {pong: bool}

    let ping (): IResult =
        Results.Ok {pong = true}