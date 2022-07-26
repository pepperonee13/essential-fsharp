open System

type ILogger = 
    abstract member Info : string -> unit
    abstract member Error : string -> unit

//standard interface implementation
type Logger() =
    interface ILogger with
        member _.Info(message) = printf "Info: %s" message
        member _.Error(message) = printf "Error: %s" message


//object expression

let logger = {
    new ILogger with
        member _.Info(msg) = printfn "Info: %s" msg
        member _.Error(msg) = printfn "Error: %s" msg
}

type MyClass(logger:ILogger) = 
    let mutable count = 0

    member _.DoSomething input =
        logger.Info $"Processing {input}"
        count <- count + 1
        ()

    member _.Count = count

let myClass = MyClass(logger)

[1..100]
|> List.iter myClass.DoSomething

printfn "%i" myClass.Count

let doSomethingElse (logger:ILogger) input =
    logger.Info $"Processing {input}"
    ()

doSomethingElse { 
    new ILogger with
        member _.Info(msg) = printfn "Info: %s" msg
        member _.Error(msg) = printfn "Error: %s" msg
    } "MyData"

