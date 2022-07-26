# Chapter 9 - Single Case Discriminiated Union

## type abbreviations

- simple aliases
- problem: nothing protects us to provide a value with different/wrong semantics with the same underlying type

Problems:
- no compile error after private constructor step
    code.fsx(223,43): error FS0039: The type 'Decimal' does not define the field, constructor or member 'Value'.
    Reason: did not work with deconstructed parameter (Spend spend) only with (spend:Spend)

sprintf function
    prints to a string intead of to the console

Chapter 8 Part homework
    private ctor for record does not seem to work


//why can I still access the private ctor?
```fsharp
let x = Spend 2000.0M
```

