//multi-case active patterns
type Score = int * int

let (|CorrectScore|_|) (expected:Score, actual:Score) =
    if expected = actual then Some () else None

let (|HomeWin|AwayWin|Draw|) (score:Score) =
    match score with
    | (h,a) when h = a -> Draw
    | (h,a) when h > a -> HomeWin
    | _ -> AwayWin

let (|CorrectResult|_|) (expected:Score, actual:Score) =
    match (expected, actual) with
    | (HomeWin, HomeWin) -> Some ()
    | (AwayWin, AwayWin) -> Some ()
    | (Draw, Draw) -> Some ()
    | _ -> None

let addPointsForCorrectScoreAndResult (score:Score*Score) =
    match score with
    | CorrectScore -> 400 //must include points for correct result as well
    | CorrectResult -> 100
    | _ -> 0


let addPointsPerGoal (score:Score*Score) =
    let expected, actual = score
    let (h,a) = expected
    let (h',a') = actual

    let home = [h;h'] |> List.min
    let away = [a;a'] |> List.min
    (home * 15) + (away * 20)


let calculatePoints (score:Score*Score) =
    let predicted, actual = score

    let calculations = [
        addPointsForCorrectScoreAndResult (predicted,actual);
        addPointsPerGoal (predicted,actual);
    ]
    
    calculations
    |> List.sum

//TESTS

let assertScore (score:int*int) =
    let actual, expected = score
    if actual = expected then true else false

let scores = [
    (0,0),(0,0),400;
    (3,2),(3,2),485;
    (5,1),(4,3),180;
    (2,1),(0,7),20;
    (2,2),(3,3),170;
]

scores 
|> List.map (fun (predicted,actual,expectedPoints) -> (calculatePoints (predicted,actual), expectedPoints))
|> List.map assertScore