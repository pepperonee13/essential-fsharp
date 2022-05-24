namespace MyProject

type Customer = {
    CustomerId : string
    IsEligible : bool
    IsRegistered: bool
}

module Db = 

    let tryGetCustomer customerId =
        try
            [
                {CustomerId = "John"; IsRegistered = true; IsEligible = true}
                {CustomerId = "Mary"; IsRegistered = true; IsEligible = true}
                {CustomerId = "Richard"; IsRegistered = true; IsEligible = false}
                {CustomerId = "Sarah"; IsRegistered = false; IsEligible = false}
            ]
            |> List.tryFind (fun c -> c.CustomerId = customerId)
            |> Ok
        with
        | ex -> Error ex


    let saveCustomer (customer:Customer) =
        try
            Ok ()
        with
        | ex -> Error ex


module Domain =

    let convertToEligible customer =
        if not customer.IsEligible then { customer with IsEligible = true}
        else customer

    let trySaveCustomer customer =
        match customer with
        | Some c -> Db.saveCustomer c
        | None -> Ok ()

    let upgradeCustomer customerId =
        customerId
        |> Db.tryGetCustomer
        |> Result.map (Option.map convertToEligible)
        |> Result.bind trySaveCustomer
