module BerlinClock

open FParsec
open System.Linq
open System

type BerlinClockTime = {
    Seconds:string
    FiveHours:string
    SingleHours:string
    FiveMinutes:string
    SingleMinutes:string
}

// Functions have to be defined higher up in the file
// than others which use them, which is an annoyance,
// and against most coding guidelines
// Probably there is a way of sorting this out
let rowLights lightCharacter lightOffCharacter lightsOn lightsInRow =
    String.Join("", Enumerable.Repeat(lightCharacter, lightsOn)) 
    + String.Join("", Enumerable.Repeat(lightOffCharacter, lightsInRow - lightsOn))

let parentRowLights lightCharacter lightOffCharacter julianTimeUnitPerLight julianTimeUnit juliantTimeUnitLimit =
    let lightsInRow = (juliantTimeUnitLimit - 1) / julianTimeUnitPerLight
    let lightsOn = julianTimeUnit / julianTimeUnitPerLight
    
    rowLights lightCharacter lightOffCharacter lightsOn lightsInRow

let remainderRowLights lightCharacter lightOffCharacter julianTimeUnitPerLight julianTimeUnit =
    let lightsInRow = julianTimeUnitPerLight - 1
    let lightsOn = julianTimeUnit % julianTimeUnitPerLight
    
    rowLights lightCharacter lightOffCharacter lightsOn lightsInRow


let fromJulianTimeComponents julianHours julianMinutes julianSeconds =
    let lightOffCharacter = "O"
    let secondsCharacter = "Y"
    let hoursCharacter = "R"
    let minutesCharacter = "Y"
    let hoursInDay = 24
    let fiveHours = 5
    let minutesInHour = 60
    let fiveMinutes = 5
    { 
        Seconds = 
            if (julianSeconds % 2) = 1 then lightOffCharacter else secondsCharacter
        FiveHours = 
            parentRowLights 
                hoursCharacter 
                lightOffCharacter 
                fiveHours 
                julianHours 
                hoursInDay
        SingleHours = 
            remainderRowLights 
                hoursCharacter 
                lightOffCharacter 
                fiveHours 
                julianHours
        FiveMinutes = 
            parentRowLights 
                minutesCharacter 
                lightOffCharacter 
                fiveMinutes 
                julianMinutes 
                minutesInHour
        SingleMinutes = 
            remainderRowLights 
                minutesCharacter 
                lightOffCharacter 
                fiveMinutes 
                julianMinutes
    }

let fromJulianTime (julianTime : string) =
    let parser = pipe3 pint32 (pstring ":" >>. pint32) (pstring ":" >>. pint32) fromJulianTimeComponents
    let parseResult = run parser julianTime
    match parseResult with
    | Success(result, _, _)   -> result
    | Failure(errorMessage, _, _) -> { 
        Seconds = errorMessage
        FiveHours = errorMessage
        SingleHours = errorMessage
        FiveMinutes = errorMessage
        SingleMinutes = errorMessage
      }

