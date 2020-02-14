module BerlinClock

open FParsec

let toBerlinClockTime2 hours minutes seconds =
    seconds

let toBerlinClockTime (julianTime : string) =
    let parser = pipe3 pint32 (pstring ":" >>. pint32) (pstring ":" >>. pint32) toBerlinClockTime2
    let parseResult = run parser julianTime
    match parseResult with
    | Success(result, _, _)   -> result
    | Failure(_, _, _) -> -1

