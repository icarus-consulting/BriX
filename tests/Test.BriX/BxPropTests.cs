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
    public sealed class BxPropTests
    {
        [Fact]
        public void PrintsProp()
        {
            var media = new XmlMedia().Block("root");

            new BxProp("Key", "Value")
                .Print(media);

            Assert.Equal(
                "<root><Key>Value</Key></root>",
                media.Content().ToString(System.Xml.Linq.SaveOptions.DisableFormatting)
            );
        }

        [Fact]
        public void AcceptsBool()
        {
            var media = new XmlMedia().Block("root");

            new BxProp("Key", true)
                .Print(media);

            Assert.Equal(
                "<root><Key>True</Key></root>",
                media.Content().ToString(System.Xml.Linq.SaveOptions.DisableFormatting)
            );
        }
    }
}
