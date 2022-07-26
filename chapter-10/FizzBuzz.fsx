type IFizzBuzz =
    abstract member Calculate : int -> string

type NoopFizzBuzz() =
    interface IFizzBuzz with
        member _.Calculate(value) = string value

type FizzBuzz(mapping) =
    let calculate n =
        mapping
        |> List.map (fun (v,s) -> if n % v = 0 then s else "")
        |> List.reduce (+)
        |> fun s -> if s <> "" then s else string n

    interface IFizzBuzz with
        member _.Calculate(value) = calculate value

let fizzBuzzFactory name : IFizzBuzz =
    if name = "first" then FizzBuzz([(3,"Fizz");(5,"Buzz")]) else NoopFizzBuzz()

let doFizzBuzz =
    //with factory
    // let fizzBuzz = fizzBuzzFactory "first_2"
    //with upcasting
    let fizzBuzz = FizzBuzz([(3,"Fizz");(5,"Buzz")]) :> IFizzBuzz
    [1..15]
    |> List.map fizzBuzz.Calculate