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
