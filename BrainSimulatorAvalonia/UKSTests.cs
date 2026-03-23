using System;
using System.Collections.Generic;
using UKS;
using Xunit;

namespace BrainSimulatorAvalonia.Tests
{
    public class UKSTests
    {
        [Fact]
        public void AddThing_CreatesNewThing()
        {
            var uks = new UKS.UKS(true);
            var thing = uks.AddThing("TestThing", null);
            Assert.NotNull(thing);
            Assert.Equal("TestThing", thing.Label);
        }

        [Fact]
        public void AddStatement_CreatesRelationship()
        {
            var uks = new UKS.UKS(true);
            var source = uks.AddThing("Source", null);
            var relType = uks.AddThing("is-a", null);
            var target = uks.AddThing("Target", null);
            var rel = uks.AddStatement(source, relType, target);
            Assert.NotNull(rel);
            Assert.Equal(source, rel.source);
            Assert.Equal(relType, rel.reltype);
            Assert.Equal(target, rel.target);
        }

        [Fact]
        public void Labeled_ReturnsCorrectThing()
        {
            var uks = new UKS.UKS(true);
            uks.AddThing("FindMe", null);
            var found = uks.Labeled("FindMe");
            Assert.NotNull(found);
            Assert.Equal("FindMe", found.Label);
        }
    }
}
