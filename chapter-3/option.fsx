open System

let tryParseDateTime (input:string) =
    match DateTime.TryParse input with 
    | true, result -> Some result
    | _ -> None

let isDate = tryParseDateTime "2022-05-09"
let isNotDate = tryParseDateTime "Hello"