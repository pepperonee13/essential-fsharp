let items = [ for x in 1..5 do x ]


let extendedItems = 6::items

let readList items =
    match items with
    | [] -> "Empty list"
    | [head] -> $"Head: {head}"
    | head::tail -> sprintf "Head: %A and Tail: %A" head tail

let emptyList = readList []


let list1 = [1..5]
let list2 = [3..7]
let list3 = [5..10]

let joined = List.concat [list1;list2] @ list3
let threeOfThem = list1 @ list2 @ list3


let myList = [1..9]

let getEvens items =
    items
    |> List.filter (fun i -> i%2=0)


let evens = getEvens myList


let triple items =
    items
    |> List.map (fun x -> x * 3)


triple items

let quantityWithPrices = [(1,0.25M);(5,0.25M);(1,2.25M);(1,125M);(7,10.9M)]

let sum items = 
    items
    |> List.map (fun (quantity,price) -> decimal quantity * price)
    |> List.sum


let total = sum quantityWithPrices

let sum_2 items =
    items
    |> List.sumBy (fun (q,p)->decimal q * p)

let total2= sum_2 quantityWithPrices

let getTotal items =
    items
    |> List.fold (fun acc (q,p)-> acc + decimal q * p) 0M


let total_withFold = getTotal quantityWithPrices

let numbers = [1;2;3;4;5;7;6;5;4;3]

let gbResult items = 
    items
    |> List.groupBy (fun x -> x)

let uniqueItems items =
    items
    |> List.map (fun (i,_) -> i)

let unResult = 
    numbers
    |> gbResult
    |> uniqueItems


// Set will sort items if it can

