//partial application
type LogLevel = 
    | Error
    | Warning 
    | Info

let log level message = 
    printfn "[%A]: %s" level message
    ()

let logError = log Error
log Error "this is an error"
logError "this is another error"

logError "log and ignore" |> ignore