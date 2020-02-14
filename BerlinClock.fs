module BerlinClock

open FParsec

type BerlinClockTime = {
    Seconds:string; 
}

let toBerlinClockTime2 hours minutes seconds =
    { Seconds = if (seconds % 2) = 1 then "O" else "Y" } //

let toBerlinClockTime (julianTime : string) =
    let parser = pipe3 pint32 (pstring ":" >>. pint32) (pstring ":" >>. pint32) toBerlinClockTime2
    let parseResult = run parser julianTime
    match parseResult with
    | Success(result, _, _)   -> result
    | Failure(errorMessage, _, _) -> { Seconds = errorMessage }

