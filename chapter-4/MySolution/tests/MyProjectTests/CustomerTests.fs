namespace MyProjectTests

open Xunit
open FsUnit
open MyProject
open MyProject.Domain


module ``Convert customer to eligible`` =

    let sourceCustomer =
        { CustomerId = "John"; IsRegistered = true; IsEligible = true }

    [<Fact>]
    let ``should succeed if not currently eligible`` () =
        let customer = { sourceCustomer with IsEligible = false }
        let converted = convertToEligible customer
        converted |> should equal sourceCustomer

    [<Fact>]
    let ``should return eligible customer unchanged`` () =
        let upgraded = convertToEligible sourceCustomer
        upgraded |> should equal sourceCustomer

    