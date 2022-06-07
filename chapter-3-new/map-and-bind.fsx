open Microsoft.FSharp.Core

type Customer =
    { Id: int
      IsVip: bool
      Credit: decimal }

let getPurchases customer =
    //imagine this talks to the DB
    try
        let purchases =
            if customer.Id % 2 = 0 then
                120M
            else
                80M

        Ok (customer, purchases)
    with
    | ex -> Error ex

let tryPromoteVip purchases =
    let (customer, amount) = purchases

    if amount > 100M then
        { customer with IsVip = true }
    else
        customer

let increaseCreditIfVip customer =
    try
        let increase = if customer.IsVip then 100M else 50M
        Ok { customer with Credit = customer.Credit + increase }
    with
    | ex -> Error ex

let map (tryPromoteVip:Customer*decimal -> Customer) (result:Result<Customer*decimal, exn>) : Result<Customer,exn> =
    match result with
    | Ok x -> Ok (tryPromoteVip x)
    | Error ex -> Error ex

let mapGeneric (f:'a -> 'b) (result:Result<'a, 'c>) : Result<'b,'c> =
    match result with
    | Ok x -> Ok (f x)
    | Error ex -> Error ex


let upgradeCustomerPiped customer = 
    customer
    |> getPurchases
    |> Result.map tryPromoteVip
    |> Result.bind increaseCreditIfVip

let sut = upgradeCustomerPiped

let customerVip = { Id = 1; IsVip = true; Credit = 0M }
let customerStandard = { Id = 2; IsVip = false; Credit = 100M; }

let assertVip = sut customerVip = Ok { Id = customerVip.Id; IsVip = true; Credit = 100M; }
let assertStandard = sut customerStandard = Ok { Id = customerStandard.Id; IsVip = true; Credit = 200M; }
let assertVipToStandard = sut { customerStandard with Id = 3; Credit = 50M; } = Ok { Id = 3; IsVip = false; Credit = 100M }
