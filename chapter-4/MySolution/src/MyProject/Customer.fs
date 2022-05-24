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

    let createCustomer customerId =
        { CustomerId = customerId; IsRegistered = true; IsEligible = false }

    let tryCreateCustomer customerId (customer:Customer option) =
        try
            match customer with
            | Some _ -> raise (exn $"Customer '{customerId}' already exists")
            | None -> Ok (createCustomer customerId)
        with
        | ex -> Error ex


    let upgradeCustomer_withProblems customerId =
        customerId
        |> Db.tryGetCustomer
        |> convertToEligible
        |> Db.saveCustomer

    let v_next customerId =
        let getCustomerResult = Db.tryGetCustomer customerId    
        let converted = convertToEligible getCustomerResult
        let saved = Db.saveCustomer converted
        saved


    let upgradeCustomer_v1 customerId =
        let getCustomerResult = Db.tryGetCustomer customerId

        //QUESTION: why do we return Result and not Option?
        let converted = 
            match getCustomerResult with
            | Ok customerOption ->
                match customerOption with
                | Some c -> Some (convertToEligible c)
                | None -> None
                |>  Ok
            | Error ex -> Error ex

        let saved = 
            match converted with
            | Ok c ->
                match c with
                | Some c -> Db.saveCustomer c
                | None -> Ok ()
            | Error ex -> Error ex
        saved

    let upgradeCustomer_v2 customerId =
        let getCustomerResult = Db.tryGetCustomer customerId

        let converted = 
            match getCustomerResult with
            | Ok customerOption ->
                match customerOption with
                | Some c -> Some (convertToEligible c)
                | None -> None
                |>  Ok
            | Error ex -> Error ex

        let saved = 
            match converted with
            | Ok c -> trySaveCustomer c
            | Error ex -> Error ex
        saved

    let upgradeCustomer_v3 customerId =
        let getCustomerResult = Db.tryGetCustomer customerId

        let converted = 
            match getCustomerResult with
            | Ok c -> c |> Option.map convertToEligible |> Ok
            | Error ex -> Error ex

        let saved = 
            match converted with
            | Ok c -> trySaveCustomer c
            | Error ex -> Error ex
        saved

    let upgradeCustomer customerId =
        customerId
        |> Db.tryGetCustomer
        |> Result.map (Option.map convertToEligible)
        |> Result.bind trySaveCustomer

    let registerCustomer customerId =
        customerId
        |> Db.tryGetCustomer
        |> Result.bind (tryCreateCustomer customerId)
        |> Result.bind Db.saveCustomer
