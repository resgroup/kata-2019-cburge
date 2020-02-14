module BerlinClock

open FParsec
open System.Linq
open System

type BerlinClockTime = {
    Seconds:string; 
    FiveHours:string;
    SingleHours:string;
    FiveMinutes:string;
    SingleMinutes:string;
}

// Functions have to be defined higher up in the file
// than others which use them, which is an annoyance,
// and against most coding guidelines
// Probably there is a way of sorting this out
let rowLights lightCharacter lightsOn lightsInRow =
    String.Join("", Enumerable.Repeat(lightCharacter, lightsOn)) 
    + String.Join("", Enumerable.Repeat('O', lightsInRow - lightsOn))

let rowLightsFromTimeUnit lightCharacter timeUnitPerLight berlinTimeUnit timeUnitLimit =
    let lightsInRow = (timeUnitLimit - 1) / timeUnitPerLight
    let lightsOn = berlinTimeUnit / timeUnitPerLight
    
    rowLights lightCharacter lightsOn lightsInRow

let remainderRowLights lightCharacter timeUnitPerLight berlinTimeUnit =
    let lightsInRow = timeUnitPerLight - 1
    let lightsOn = berlinTimeUnit % timeUnitPerLight
    
    rowLights lightCharacter lightsOn lightsInRow


let fromJulianTimeComponents hours minutes seconds =
    { 
        Seconds = if (seconds % 2) = 1 then "O" else "Y";
        FiveHours = rowLightsFromTimeUnit "R" 5 hours 24
        SingleHours = remainderRowLights "R" 5 hours
        FiveMinutes = rowLightsFromTimeUnit "Y" 5 minutes 60
        SingleMinutes = remainderRowLights "Y" 5 minutes
    }

let fromJulianTime (julianTime : string) =
    let parser = pipe3 pint32 (pstring ":" >>. pint32) (pstring ":" >>. pint32) fromJulianTimeComponents
    let parseResult = run parser julianTime
    match parseResult with
    | Success(result, _, _)   -> result
    | Failure(errorMessage, _, _) -> { 
        Seconds = errorMessage; 
        FiveHours = errorMessage; 
        SingleHours = errorMessage; 
        FiveMinutes = errorMessage;
        SingleMinutes = errorMessage;
      }

