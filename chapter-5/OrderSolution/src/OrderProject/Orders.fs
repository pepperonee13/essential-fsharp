namespace OrderProject

// Questions:
// - selecting parts of the code and run it in FSI

type Item = {
    ProductId : int
    Quantity : int
}

type Order = {
    Id : int
    Items : Item list
}

module Domain =

    let sort items =
        items 
        |> List.sortBy (fun i -> i.ProductId)

    let recalculate items = 
        items
        |> List.groupBy (fun i -> i.ProductId)
        |> List.map (fun (id, items) -> 
            {ProductId = id; Quantity = items |> List.sumBy (fun i -> i.Quantity)})

    let addItem item order =
        let items = 
            item::order.Items
            |> recalculate
            |> sort
        {order with Items = items}

    let addItems newItems order =
        let items =
            newItems @ order.Items
            |> recalculate
            |> sort
        {order with Items = items}

    // let order = { Id = 1; Items = [ { ProductId = 1; Quantity = 1 } ] }
    // let newItemExistingProduct = { ProductId = 1; Quantity = 1 }
    // let newItemNewProduct = { ProductId = 2; Quantity = 2 }

    // addItem newItemNewProduct order = 
    //     { Id = 1; Items = [ { ProductId = 1; Quantity = 1}; { ProductId = 2; Quantity = 2 } ] }

    // addItem newItemExistingProduct order =  
    //     { Id = 1; Items = [ { ProductId = 1; Quantity = 2 } ] }