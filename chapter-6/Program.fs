open System.IO

type Customer = {
    CustomerId : string
    Email : string
    IsEligible : string
    IsRegistered : string
    DateRegistered : string
    Discount: string
}

let readFile path =
    try
        seq {
            use reader = new StreamReader(File.OpenRead(path));
            while not reader.EndOfStream do
                reader.ReadLine()
        }
        |> Ok
    with
    | ex -> Error ex

let parseLine (line:string) : Customer option =
    match line.Split('|') with
    | [|customerId; email; eligible; registered; dateRegistered; discount|] ->
        Some {
            CustomerId = customerId
            Email = email
            IsEligible = eligible
            IsRegistered = registered
            DateRegistered = dateRegistered
            Discount = discount
        }
    | _ -> None

let parseCustomer (data:string seq) =
    data
    |> Seq.skip 1
    |> Seq.map parseLine
    |> Seq.choose id 


let import path =
    match path |> readFile with
    | Ok r -> r |> parseCustomer |>  Seq.iter (fun x -> printfn "%A" x)
    | Error ex -> printfn "Error: %A" ex.Message

Path.Combine(__SOURCE_DIRECTORY__,"resources", "customers.csv")
|> import 
