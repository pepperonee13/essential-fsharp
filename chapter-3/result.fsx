open System

let divide (x:decimal) (y:decimal) =
    try
        Ok (x/y)
    with
    | :? DivideByZeroException as ex -> Error ex


divide 10 2
divide 10 0