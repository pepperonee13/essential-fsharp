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

let fakeDataReader : DataReader =
    fun _ ->
        seq {
            "CustomerId|Email|Eligible|Registered|DateRegistered|Discount"
            "John|john@test.com|1|1|2015-01-23|0.1"
            "Mary|mary@test.com|1|1|2018-12-12|0.1"
            "Richard|richard@nottest.com|0|1|2016-03-23|0.0"
            "Sarah||0|0||"
        }
        |> Ok

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

let parseCustomer data =
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
let importFromString = import fakeDataReader

path |> importFromString
