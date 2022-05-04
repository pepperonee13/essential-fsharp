open System

let now () = DateTime.UtcNow

let log msg = 
    ()

log "test"
now ()
now

let fixedNow = DateTime.UtcNow

fixedNow 

