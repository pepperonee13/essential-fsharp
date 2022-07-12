open System

let parse (input:string) =
    match DateTime.TryParse(input) with
    | true, v -> Some v
    | false, _ -> None


let result = parse "2021-07-12"
let noneResult = parse "im not a date"