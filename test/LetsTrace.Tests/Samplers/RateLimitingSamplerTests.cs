using System;
using System.Collections.Generic;
using LetsTrace.Samplers;
using LetsTrace.Util;
using NSubstitute;
using Xunit;

namespace LetsTrace.Tests.Samplers
{
    public class RateLimitingSamplerTests
    {
        [Fact]
        public void RateLimitingSampler_Constructor_ThrowsIfRateLimiterIsNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new RateLimitingSampler(1.0, null));
            Assert.Equal("rateLimiter", ex.ParamName);
        }

        [Fact]
        public void RateLimitingSampler_UsesTheRateLimiter()
        {
            var maxTracesPerSecond = 3.6;
            var expectedTags = new Dictionary<string, Field> {
                { Constants.SamplerTypeTagKey, new Field<string> { Value = Constants.SamplerTypeRateLimiting } },
                { Constants.SamplerParamTagKey, new Field<double> { Value = maxTracesPerSecond } }
            };
            var rateLimiter = Substitute.For<IRateLimiter>();
            double calledWith = 0;
            var shouldReturn = false;
            rateLimiter.CheckCredit(Arg.Any<double>()).Returns(x => {
                calledWith = (double)x[0];
                return shouldReturn;
            });
            
            var sampler = new RateLimitingSampler(maxTracesPerSecond, rateLimiter);
            var isSampled = sampler.IsSampled(new TraceId(), "op");

            Assert.False(isSampled.Sampled);
            Assert.Equal(1.0, calledWith);

            shouldReturn = true;
            isSampled = sampler.IsSampled(new TraceId(), "op");

            Assert.True(isSampled.Sampled);
            Assert.Equal(1.0, calledWith);
            Assert.Equal(expectedTags, isSampled.Tags);
        }
    }
}