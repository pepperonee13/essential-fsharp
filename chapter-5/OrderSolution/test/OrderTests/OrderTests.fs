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

module ``Remove product from order`` =
    [<Fact>]
    let ``given empty order should not have effect`` () =
        let order = { Id = 1; Items = []}

        let updated = order |> removeProduct 1

        updated |> should equal order

    [<Fact>]
    let ``given order has product`` () =
        let order = { Id = 1; Items = [ {ProductId = 1; Quantity = 1} ]}
        
        let updated = order |> removeProduct 1

        updated |> should equal { Id = 1; Items = []}

    [<Fact>]
    let ``given order does not have product`` () =
        let order = { Id = 1; Items = [ {ProductId = 1; Quantity = 1} ]}
        
        let updated = order |> removeProduct 2

        updated |> should equal { Id = 1; Items = [{ProductId = 1; Quantity = 1}]}

module ``Reduce item quantity`` =
    [<Fact>]
    let ``so that quantity should still remain`` () =
        let order = { Id = 1; Items = [ {ProductId = 1; Quantity = 2} ]}

        let updated = order |> reduceItem 1 1

        updated |> should equal { Id = 1; Items = [{ProductId = 1; Quantity = 1}]}

    [<Fact>]
    let ``so that all units are removed`` () =
        let order = { Id = 1; Items = [ {ProductId = 1; Quantity = 5} ]}
        let expected = {Id = 1; Items = []}

        let updated = order |> reduceItem 1 5

        updated |> should equal {Id = 1; Items = []}

    [<Fact>]
    let ``when product does not exist`` () =
        let order = { Id = 1; Items = [ {ProductId = 1; Quantity = 5} ]}
        let expected = { Id = 1; Items = [ {ProductId = 1; Quantity = 5} ]}

        let updated = order |> reduceItem 2 5

        updated |> should equal expected

module ``Clear items`` =
    [<Fact>]
    let ``on empty order`` () =
        let order = { Id = 1; Items = []}

        let updated = order |> clearItems

        updated |> should equal {Id=1;Items = []}

    [<Fact>]
    let ``on order with items`` () =
        let order = { Id = 1; Items = [ {ProductId = 1; Quantity = 5} ]}

        let updated = order |> clearItems

        updated |> should equal {Id = 1; Items = []}
