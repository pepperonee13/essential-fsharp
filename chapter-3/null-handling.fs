open System

let nullObj:string = null
let nullPri = Nullable<int>()

let fromNullObj = Option.ofObj nullObj
let fromNullPri = Option.ofNullable nullPri

let toNullObj = Option.toObj fromNullObj
let toNullPri = Option.toNullable fromNullPri


let resultPM input = 
    match input with
    | Some value -> value
    | None -> "------"


let result = Option.defaultValue "------" fromNullObj

let result2 = resultPM fromNullObj

//NOTE: C# ?? operator is nicer ^^


let setDefault = Option.defaultValue "<default>"


let resultWithSetDefault = fromNullObj |> setDefault