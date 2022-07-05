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
