open Microsoft.FSharp.Core
type Customer =
    { Id: int
      IsVip: bool
      Credit: decimal }

let getPurchases customer =
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

let upgradeCustomerComposed =
    getPurchases
    >> Result.map tryPromoteVip
    >> Result.bind increaseCreditIfVip


//QUESTION: how to use map and bind in these styles?

// let updateCustomerNested customer = increaseCreditIfVip(tryPromoteVip(getPurchases customer))

// let updateCustomerProcedural customer = 
//     let customerWithPurchases = getPurchases customer
//     let promotedCustomer = tryPromoteVip customerWithPurchases
//     let increasedCreditCustomer = increaseCreditIfVip promotedCustomer
//     increasedCreditCustomer

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
