module Tests

open System
open Xunit
open Swensen.Unquote
open FsCheck

// double backticks allow us to put spaces in names, which makes the test names clear / more like english / better when you see the test output
[<Fact>]
let ``My test`` () =
    Assert.True(true)

// unquote library allows me to write tests like this, without the assert statement
[<Fact>]
let ``2 + 2 is 4``() = 
   let result = 2 + 2
   result =! 4

let add(number1:int, number2:int) = 
    number1 + number2

// property based testing will test with a lot of different numbers
[<Fact>]
let ``Adding two numbers returns sum of the numbers``() = 
   let checkadd(number1, number2) =
    add(number1, number2) = number1 + number2

   Check.QuickThrowOnFailure checkadd
