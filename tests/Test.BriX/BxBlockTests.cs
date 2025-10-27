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
using System.Xml.Linq;
using Xunit;

namespace BriX.Test
{
    public sealed class BxBlockTests
    {
        [Fact]
        public void WritesBlockName()
        {
            var media = new XmlMedia();

            new BxBlock("mein-block", new BxProp("stockwerk", "16"))
                .Print(media);

            Assert.Equal(
                "<mein-block><stockwerk>16</stockwerk></mein-block>",
                media.Content().ToString(SaveOptions.DisableFormatting)
            );
        }

        [Fact]
        public void PutsBlockInBlock()
        {
            var media = new XmlMedia();

            new BxBlock("mein-block", new BxBlock("dein-block"))
                .Print(media);

            Assert.Equal(
                "<mein-block><dein-block /></mein-block>",
                media.Content().ToString(SaveOptions.DisableFormatting)
            );
        }

        [Fact]
        public void CascadeBlocks()
        {
            var media = new JsonMedia();
            var res =
                new BxBlock("editor",
                    new BxBlock("body",
                        new BxBlock("scene",
                            new BxBlock("display",
                                new BxBlock("entity",
                                    new BxProp("handle", "robot"),
                                    new BxProp("stateEntityType", "machine")
                                )
                            )
                        )
                    )
                ).Print(media);

            Assert.Equal(
                media.Content().ToString(),
                res.ToString()
            );
        }
    }
}
