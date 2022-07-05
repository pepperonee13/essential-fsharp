open System.IO

type Customer = {
    CustomerId : string
    Email : string
    IsEligible : string
    IsRegistered : string
    DateRegistered : string
    Discount: string
}

type DataReader = string -> Result<string seq, exn>

let readFile : DataReader =
    fun path ->
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


let output data =
    data
    |> Seq.iter (fun x -> printfn "%A" x)

let import (dataReader: DataReader) path =
    match path |> dataReader with
    | Ok data -> data |> parseCustomer |> output
    | Error ex -> printfn "Error: %A" ex.Message

let path = Path.Combine(__SOURCE_DIRECTORY__,"resources", "customers.csv")

let importFromFile = import readFile

path |> importFromFile
