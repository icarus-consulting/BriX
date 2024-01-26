using System.Diagnostics;
using Xunit;
using Yaapii.Xml;

namespace BriX.Media.Test
{
    public sealed class SecureXmlMediaTests
    {
        [Fact]
        public void EscapesXmlChars()
        {
            var media = new SecureXmlMedia(new RebuildMedia());
            media.Block("root")
                .Prop("key")
                .Put("<root><key>value</key></root>");

            Assert.Contains(
                "&lt;root&gt;&lt;key&gt;value&lt;/key&gt;&lt;/root&gt;",
                media.Content().ToString()
            );
        }

        [Fact]
        public void RemovesInvalidChars()
        {
            var media = new SecureXmlMedia(new RebuildMedia());
            media.Block("root")
                .Prop("key")
                .Put($"invalid char: '\0'");

            Assert.Equal(
                "invalid char: ''",
                new XMLCursor(
                    media.Content()
                ).Values("/root/key/text()")[0]
            );
        }

        // TODO: clean up
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        public void PerformanceTest_InvalidChars(int expected)
        {
            var media = new SecureXmlMedia(new RebuildMedia());
            var sw = new Stopwatch();
            sw.Start();
            media.Block("root")
                .Prop("key")
                .Put($"invalid char: '{this.manyChars}'");
            sw.Stop();
            var elapsed = sw.Elapsed.TotalMilliseconds;
            var content = media.Content().ToString();
            //Assert.Equal(
            //    "invalid char: ''",
            //    new XMLCursor(
            //        media.Content()
            //    ).Values("/root/key/text()")[0]
            //);
            Assert.Equal(expected, elapsed);
        }
        public SecureXmlMediaTests()
        {
            this.manyChars = string.Empty;
            for (int i = 0; i < 100000; i++)
            {
                manyChars += "\0";
                //manyChars += "0";
            }
        }
        private readonly string manyChars;
    }
}
