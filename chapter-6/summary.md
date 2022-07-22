# Chapter 6

- top level statement in F# NET 6 has implicit [<EntryPoint>] entry point
- __SOURCE_DIRECTORY__ built-in const
- Seq
    - like IEnumerable<T> in C#
    - has LINQ like function like List and Array
    - Seq.choose ignores None 
        - id means (fun x -> x)
- printfn "%A" -> prints readable string on records (.ToString()?)
- [||] -> array
- [] -> list
- Questions:
    - when to set function signature explicitly