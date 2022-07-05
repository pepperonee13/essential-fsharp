namespace OrderTests

open Xunit
open FsUnit
open OrderProject
open OrderProject.Domain


module ``Add item to order`` =
    [<Fact>]
    let ``to empty Order`` () =
        let myEmptyOrder = { Id = 1; Items = [] }
        let expected = { Id = 1; Items = [{ProductId = 2; Quantity = 1}] }
        
        let updated = myEmptyOrder |> addItem { ProductId = 2; Quantity = 1 }

        updated |> should equal expected

    [<Fact>]
    let ``product does not exists in Order`` () =
        let order = { Id = 1; Items = [ { ProductId = 1; Quantity = 1 } ] }
        let expected = { Id = 1; Items = [ { ProductId = 1; Quantity = 1 }; { ProductId = 2; Quantity = 2 } ] }

        let updated = order |> addItem { ProductId = 2; Quantity = 2 }

        updated |> should equal expected

    [<Fact>]
    let ``product exists in Order`` () =
        let order = {Id=1; Items = [{ProductId = 1; Quantity = 1}]}
        let expected = {Id=1;Items = [{ProductId = 1; Quantity = 2}]}

        let updated = order |> addItem {ProductId = 1; Quantity = 1}

        updated |> should equal expected

module ``Add multiple items to order`` =
    [<Fact>]
    let ``new products added to empty order`` () =
        let myEmptyOrder = { Id = 1; Items = [] }
        let newItems = [ { ProductId = 1; Quantity = 1 }; { ProductId = 2; Quantity = 5 } ]
        let expectedOrder = { Id = 1; Items = [ { ProductId = 1; Quantity = 1 }; { ProductId = 2; Quantity = 5 } ] }

        let order = myEmptyOrder |> addItems newItems

        order |> should equal expectedOrder

    [<Fact>]
    let ``given some are new some are existing`` () =
        let existingOrder = { Id = 1; Items = [{ProductId = 1; Quantity = 1}] }
        let expected = { Id = 1; Items = [{ProductId = 1; Quantity = 2}; {ProductId = 2; Quantity = 5}]}

        let order = existingOrder |> addItems [{ProductId = 1; Quantity = 1}; {ProductId = 2; Quantity = 5}]

        order |> should equal expected
