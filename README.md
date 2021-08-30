# Given.. When.. Then..

A very light-weight library to help you enforce a Given-When-Then structure in your unit test. 

[![NuGet version (Cobrakai.GWTUnit)](https://img.shields.io/nuget/v/Cobrakai.GWTUnit.svg?style=flat-square)](https://www.nuget.org/packages/Cobrakai.GWTUnit/)


Let's jump right into two quick demos, one for testing a function(that returns something), one for testing an action(that returns nothing but modifies something). That should cover most cases and get you started!

Only thing you need to import:
```csharp
using static Cobrakai.GWTUnit.GWTUnit;
```

Then for testing a function:


```csharp
[TestMethod]
public void ShouldBeAbleToTestFunctions()
{
    Given(() =>
    {
        // You can do all your test preparations here, such as create test inputs for different scenarios
        int ingredient1= 1;
        int ingredient2 = 2;

        //For this demo, let's assume this noodleMaker is what we want to test.
        Func<int, int, int> noodleMaker = (x, y) => x + y;

        // Simply return a tuple that includes all variales relevant to the actual testing and verification.
        // You can even use named values in tuples here 
        return (noodleMaker, flour: ingredient1, water: ingredient2);
    })
    .When(ctx =>
    {
        // You do the actual testing call in the When block.
        // You have access to all your setup variables from the ctx variable here.
        // You do not have to return or assign the test returned value if you are testing a function.
        // Because it will be captured and available for you in the Then block next.
        ctx.noodleMaker(ctx.flour, ctx.water);
    })
    .Then((ctx, actualResult)=>
    {
        // Here, once again you have access to the context that you set up in the Given block. 
        // Because your tested function may not be a pure function and modified inputs, and you may want to verify its effects on those inputs.
        // You also get access to the returned value from your tested function so you can do validations.
        Should("have delicious noodle", () =>
        {
            actualResult.Should().Be(3);
        });
    });
}

```

Here is a demo on testing a method that does not return anything. Pretty much similar, so no comments. But do notice that in the Then block you only have access to Contex from Given, but no results from When.
```csharp
[TestMethod]
public void ShouldBeAbleToTestActions()
{
    Given(() =>
    {
        List<string> dwarfs = new List<string>() { "Sneezy", "Bashful", "Sleepy", "Happy", "Grumpy", "Doc", "Dopey" };

        //Let this be the method we test here.
        Action<List<string>> DoDarkMagic = (creatures) => { creatures.RemoveAt(0); };

        return (DoDarkMagic, dwarfs);
    })
    .When(_ => _.DoDarkMagic(_.dwarfs))
    .Then(_ =>
    {
        Should("have less dwarfs", () =>
        {
            _.dwarfs.Count.Should().Be(6);
        });

        Should("missing Sneezy", () =>
        {
            _.dwarfs.Should().NotContain("Sneezy");
        });
    });
}
```