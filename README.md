# Berlin Clock Kata in F#

## Constraints

I was using a new language (F#), and was mostly adhering to [Functional Calisthenics](https://codurance.com/2017/10/12/functional-calisthenics/).

## Solution

The files to look at are:

 - [BerlinClock.fs](BerlinClock.fs)
 - [test-kata-2019-cedd/Tests.fs](test-kata-2019-cedd/Tests.fs)

The bulk of the solution is as follows, and the whole thing is under 100 lines long. F# has a reputation for being concise, 
which seems to be deserved.

```f#
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
```

## Thoughts

I have done this problem before, which is basically cheating. However I am relatively pleased with the solution, which is simple, concise and didn't take long to write. It also communicates most of its workings in one small function and is easy to understand. Overall it adheres well to our [cost effective coding guidelines](https://medium.com/res-software-team/making-decisions-at-the-right-level-56dffb7362c).

I would have like to have used QuickCheck in the tests, but I didn't get time. This would have required me to repeat the calculation code in the tests for each row type, which I and the code quality books think is a good thing, as it means the tests communicate their intent, and it avoids the [Hard Coded Test Data](http://xunitpatterns.com/Obscure%20Test.html#Hard-Coded%20Test%20Data) code smell. However there are other opinions.

Having to declare functions before using them is annoying, and forces me to order the code in the opposite way to that recommended by code quality books. I imagine there is a way round this, but I didn't find it.

The double backtick notation in F# allows your function names to have spaces in, which is good for making the output of tests easier to read.
