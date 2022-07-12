let (|IsDivisableBy|_|) divisor i =
    if i % divisor = 0 then Some () else None

let calculate i = 
    if i % 3 = 0 && i % 5 = 0 then "FizzBuzz"
    elif i % 3 = 0 then "Fizz"
    elif i % 5 = 0 then "Buzz"
    else i |> string

let calculateWithPatternMatch i = 
    match (i%3, i%5) with
    | 0,0 -> "FizzBuzz"
    | 0,_ -> "Fizz"
    | _,0-> "Buzz"
    | _ -> i |> string

let calculateWithPatternMatchV2 i = 
    match (i%3 = 0, i%5 = 0) with
    | true,true -> "FizzBuzz"
    | true,_ -> "Fizz"
    | _,true-> "Buzz"
    | _ -> i |> string

let calculateWithActivePattern i = 
    match i with
    | IsDivisableBy 3 & IsDivisableBy 5 -> "FizzBuzz"
    | IsDivisableBy 3 -> "Fizz"
    | IsDivisableBy 5 -> "Buzz"
    | _ -> i |> string

let activeCalculate = calculateWithActivePattern

let result = [1..15] |> List.map activeCalculate
 