using Jaeger.Propagation;
using Xunit;

namespace Jaeger.Tests.Propagation
{
    public class W3CTextMapCodecTests
    {
        W3CTextMapCodec w3cCodec = new W3CTextMapCodec();

        [Fact]
        public void TestInject()
        {
            var textMap = new TestTextMap();
            
            w3cCodec.Inject(new SpanContext(new TraceId(282752961257817910), new SpanId(67667974448284343), new SpanId(1), SpanContextFlags.Sampled), textMap);
            Assert.True(textMap.ContainsKey(W3CTextMapCodec.TraceParentName));
            Assert.Equal("00-03ec8a51f6f64736-00f067aa0ba902b7-01", textMap.Get(W3CTextMapCodec.TraceParentName));
            Assert.True(textMap.ContainsKey(W3CTextMapCodec.TraceStateName));
            Assert.Equal("jaeger=3ec8a51f6f64736:f067aa0ba902b7:1:1", textMap.Get(W3CTextMapCodec.TraceStateName));

            w3cCodec.Inject(new SpanContext(new TraceId(5924597723497592834), new SpanId(46368982374674), new SpanId(1), SpanContextFlags.None), textMap);
            Assert.True(textMap.ContainsKey(W3CTextMapCodec.TraceParentName));
            Assert.Equal("00-5238663d5a297802-00002a2c1eb91912-00", textMap.Get(W3CTextMapCodec.TraceParentName));
            Assert.True(textMap.ContainsKey(W3CTextMapCodec.TraceParentName));
            Assert.Equal("jaeger=5238663d5a297802:2a2c1eb91912:1:0", textMap.Get(W3CTextMapCodec.TraceStateName));

            w3cCodec.Inject(new SpanContext(new TraceId(938049759070498572), new SpanId(7498273649875), new SpanId(1), SpanContextFlags.Debug), textMap);
            Assert.True(textMap.ContainsKey(W3CTextMapCodec.TraceParentName));
            Assert.Equal("00-0d049f492f09af0c-000006d1d3eff4d3-02", textMap.Get(W3CTextMapCodec.TraceParentName));
            Assert.True(textMap.ContainsKey(W3CTextMapCodec.TraceParentName));
            Assert.Equal("jaeger=d049f492f09af0c:6d1d3eff4d3:1:2", textMap.Get(W3CTextMapCodec.TraceStateName));
        }

        [Fact]
        public void TestExtract()
        {
            var carrier = new TestTextMap();

            // test
            carrier.Set("tracestate", "jaeger=3ec8a51f6f64736:f067aa0ba902b7:1:1");

            var spanContext = w3cCodec.Extract(carrier);

            Assert.Equal(new TraceId(282752961257817910), spanContext.TraceId);
            Assert.Equal(new SpanId(67667974448284343), spanContext.SpanId);
            Assert.True(spanContext.IsSampled);

            // test
            carrier.Set("tracestate", "jaeger=5238663d5a297802:2a2c1eb91912:1:0");

            spanContext = w3cCodec.Extract(carrier);

            Assert.Equal(new TraceId(5924597723497592834), spanContext.TraceId);
            Assert.Equal(new SpanId(46368982374674), spanContext.SpanId);
            Assert.False(spanContext.IsSampled);

            // test
            carrier.Set("tracestate", "jaeger=d049f492f09af0c:6d1d3eff4d3:1:2");

            spanContext = w3cCodec.Extract(carrier);

            Assert.Equal(new TraceId(938049759070498572), spanContext.TraceId);
            Assert.Equal(new SpanId(7498273649875), spanContext.SpanId);
            Assert.True(spanContext.IsDebug);
        }
    }
}
