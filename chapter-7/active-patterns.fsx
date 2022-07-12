open System

let (|ValidDate|_|) (input:string) =
    let success, value = DateTime.TryParse(input)
    if success then Some value else None

let parse (input:string) =
    match input with
    | ValidDate dt -> printfn "%A" dt
    | _ -> printfn "'%s' is not a valid date" input


let result = parse "2021-07-12"
let noneResult = parse "im not a date"