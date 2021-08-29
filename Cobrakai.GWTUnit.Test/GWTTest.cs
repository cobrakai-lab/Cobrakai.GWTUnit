using System;
using System.Collections.Generic;
using FluentAssertions;
using static Cobrakai.GWTUnit.GWTUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cobrakai.GWTUnit.Test
{
    [TestClass]
    public class GWTTest 
    {
    
        [TestMethod]
        public void ShouldBeAbleToTestFunctions()
        {
            Given(() =>
            {
                int a = 1;
                int b = 2;
                Func<int, int, int> noodleMaker = (x, y) => x + y;

                return (noodleMaker, flour: a, water: b);
            })
            .When(ctx => ctx.noodleMaker(ctx.flour, ctx.water))
            .Then(actualResult =>
            {
                Should("have delicious noodle", () =>
                {
                    actualResult.Should().Be(3);
                });
            });
        }
    
        [TestMethod]
        public void ShouldBeAbleToTestActions()
        {
            Given(() =>
            {
                List<string> dwarfs = new List<string>() { "Sneezy", "Bashful", "Sleepy", "Happy", "Grumpy", "Doc", "Dopey" };
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
    }
}