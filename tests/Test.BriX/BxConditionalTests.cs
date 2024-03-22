//MIT License

//Copyright (c) 2022 ICARUS Consulting GmbH

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using BriX.Media;
using Xunit;

namespace BriX.Test
{
    public sealed class BxConditionalTests
    {
        [Fact]
        public void PrintsIfConditionMatched()
        {
            var media = new XmlMedia().Block("root");
            new BxConditional(() => true,
                () => new BxProp("Matched", "true")
            ).Print(media);
            Assert.Equal(
                "<root><Matched>true</Matched></root>",
                media.Content().ToString(System.Xml.Linq.SaveOptions.DisableFormatting)
            );
        }

        [Fact]
        public void DoesntPrintIfConditionNotMatched()
        {
            var media = new XmlMedia().Block("root");
            new BxConditional(() => false,
                () => new BxProp("Matched", "true")
            ).Print(media);
            Assert.Equal(
                "<root />",
                media.Content().ToString(System.Xml.Linq.SaveOptions.DisableFormatting)
            );
        }

        [Fact]
        public void PrintsAlternativeIfConditionNotMatched()
        {
            var media = new XmlMedia().Block("root");
            new BxConditional(() => false,
                () => new BxProp("Matched", "true"),
                () => new BxProp("Matched", "false")
            ).Print(media);
            Assert.Equal(
                "<root><Matched>false</Matched></root>",
                media.Content().ToString(System.Xml.Linq.SaveOptions.DisableFormatting)
            );
        }
    }
}
