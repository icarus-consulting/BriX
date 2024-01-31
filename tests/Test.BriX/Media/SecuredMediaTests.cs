using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using Xunit;
using Yaapii.Xml;

namespace BriX.Media.Test
{
    public sealed class SecuredMediaTests
    {
        [Fact]
        public void EscapesXmlChars()
        {
            var media = new SecuredMedia<XNode>(new RebuildMedia());
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
            var media = new SecuredMedia<XNode>(new RebuildMedia());
            media.Block("root")
                .Prop("key")
                .Put($"invalid char: '\0'");

            Assert.Equal(
                "invalid char: ' '",
                new XMLCursor(
                    media.Content()
                ).Values("/root/key/text()")[0]
            );
        }

        [Fact(Skip = "Use this test to check performance")]
        public void PerformsWell()
        {
            var manyChars = string.Empty;
            for (int i = 0; i < 100000; i++)
            {
                //manyChars += "\0";
                manyChars += "0";
            }

            var media =
                new SecuredMedia<XNode>(
                    new RebuildMedia()
                ).Block("root")
                .Prop("key");

            var msArray = new List<double>();
            for (int i = 0; i < 10; i++)
            {
                var sw = new Stopwatch();
                sw.Start();
                media.Put($"invalid char: '{manyChars}'");
                sw.Stop();
                msArray.Add(sw.Elapsed.TotalMilliseconds);
            }
            Assert.All(msArray, ms => Assert.True(ms < 0.01));
        }
    }
}
