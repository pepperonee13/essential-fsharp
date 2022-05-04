type DemandReason =
    | Replacement
    | Extension

type DemandReported =
    { Id: string
      Reason: DemandReason
      NeedsApproval: bool }

type DemandEvent = DemandReported of DemandReported

type ReportReplacementDemand = { Id: string }

type Commands = ReportReplacementDemand of ReportReplacementDemand

type DemandStates =
    | DefaultState
    | ReportedState of Id: string

let handleCommand command =
    match command with
    | ReportReplacementDemand rd -> DemandReported { Id = rd.Id; Reason = Replacement; NeedsApproval = false }


let updateState state event =
    match state, event with
    | (DefaultState, DemandReported e) -> printfn "Applying demand reported %s" e.Id
    | (_, _) -> ()


//how can I pass nothing or null instead of DefaultState? should I?
let command = ReportReplacementDemand { Id = "1" }
let event = handleCommand command
let state = updateState DefaultState event
