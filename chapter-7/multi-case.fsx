//multi-case active patterns
type Score = int * int

let addPointsForCorrectScore (score:Score*Score*int) =
    let predicted, actual ,points = score
    match predicted,actual,points with
    | (h,a),(h',a'),_ when h = h' && a = a' -> (predicted, actual, points + 300)
    | _ -> score

let addPointsForCorrectResult (score:Score*Score*int) =
    let predicted, actual ,points = score
    match predicted,actual,points with
    | (h,a),(h',a'),_ when h > a && h' > a' -> (predicted, actual, points + 100)
    | (h,a),(h',a'),_ when h < a && h' < a' -> (predicted, actual, points + 100)
    | (h,a),(h',a'),_ when h = a && h' = a' -> (predicted, actual, points + 100)
    | _ -> score

let addPointsPerHomeGoal (score:Score*Score*int) =
    let predicted, actual ,points = score
    match predicted,actual,points with
    | (h,a),(h',a'),_ when h < h' -> (predicted, actual, points + h*15)
    | (h,a),(h',a'),_ when h' < h -> (predicted, actual, points + h'*15)
    | (h,a),(h',a'),_ when h' = h -> (predicted, actual, points + h'*15)
    | _ -> score

let addPointsPerAwayGoal (score:Score*Score*int) =
    let predicted, actual ,points = score
    match predicted,actual,points with
    | (h,a),(h',a'),_ when a < a' -> (predicted, actual, points + a*20)
    | (h,a),(h',a'),_ when a' < a -> (predicted, actual, points + a'*20)
    | (h,a),(h',a'),_ when a' = a -> (predicted, actual, points + a'*20)
    | _ -> score



let calculateScore (score:Score*Score*int) =
    let predicted, actual, expected = score

    let (_,_,points) = 
        (predicted, actual, 0)
        |> addPointsForCorrectScore
        |> addPointsForCorrectResult
        |> addPointsPerHomeGoal
        |> addPointsPerAwayGoal
    (points,expected)

let assertScore score =
    let actual, expected = score
    if actual = expected then true else false

let scores = [
    (0,0),(0,0),400;
    (3,2),(3,2),485;
    (5,1),(4,3),180;
    (2,1),(0,7),20;
    (2,2),(3,3),170;
]

let calculatedResults =
    scores 
    |> List.map calculateScore
    |> List.map assertScore