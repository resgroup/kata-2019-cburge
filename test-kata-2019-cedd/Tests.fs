module Tests

open System
open Xunit
open Swensen.Unquote
open FsCheck
open BerlinClock

[<Fact>]
let ``Seconds light is off for 12:40:01``() = 
   (BerlinClock.fromJulianTime "12:40:01").Seconds =! "O"

[<Fact>]
// this test was failing for a while, because I had changed
// the function name, but not the value in the test. Using
// a parameterised test would improve this
let ``Seconds light is on for 10:30:10``() = 
   (BerlinClock.fromJulianTime "10:30:10").Seconds =! "Y"

[<Fact>]
let ``3 Five-Hour lights are on for 16:00:00``() = 
   (BerlinClock.fromJulianTime "16:00:00").FiveHours =! "RRRO"

[<Fact>]
let ``2 One-Hour lights are on for 07:00:00``() = 
   (BerlinClock.fromJulianTime "07:00:00").SingleHours =! "RROO"

[<Fact>]
let ``4 Five-Minute lights are on for 07:23:00``() = 
   (BerlinClock.fromJulianTime "07:23:00").FiveMinutes =! "YYYYOOOOOOO"

[<Fact>]
let ``1 One-Minute lights is on for 07:31:00``() = 
   (BerlinClock.fromJulianTime "07:31:00").SingleMinutes =! "YOOO"









// double backticks allow us to put spaces in names, which makes the test names clear / more like english / better when you see the test output
[<Fact>]
let ``My test`` () =
    Assert.True(true)

// unquote library allows me to write tests like this, without the assert statement
[<Fact>]
let ``2 + 2 is 4``() = 
   let result = 2 + 2
   result =! 4


let add number1 number2 = 
    number1 + number2

// property based testing will test with a lot of different numbers
[<Fact>]
let ``Adding two numbers returns sum of the numbers``() = 
   let checkadd number1 number2 =
    add number1 number2 = number1 + number2

   Check.QuickThrowOnFailure checkadd
