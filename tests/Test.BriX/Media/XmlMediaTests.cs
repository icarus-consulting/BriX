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

using System;
using System.Xml.Linq;
using Xunit;
using Yaapii.Xml;

namespace BriX.Media.Test
{
    public sealed class XmlMediaTests
    {
        [Fact]
        public void CreatesPropertyInBlock()
        {
            var media = new XmlMedia();

            media.Block("root")
                .Prop("key");

            Assert.Equal(
                "1",
                new XMLCursor(media.Content()).Values("count(/root/key)")[0]
            );
        }

        [Fact]
        public void RejectsPuttingPropertyToArray()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new XmlMedia()
                    .Array("root", "item")
                    .Prop("key")
                    .Put("lock")
            );
        }

        [Fact]
        public void RejectsPuttingPropertyToRoot()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new XmlMedia()
                    .Prop("key")
                    .Put("lock")
            );
        }

        [Fact]
        public void CreatesBlockInRoot()
        {
            var media = new XmlMedia();
            media.Block("root")
                .Prop("key")
                .Put("value");

            Assert.Contains(
                "value",
                new XMLCursor(media.Content()).Values("/root/key/text()")
            );
        }

        [Fact]
        public void RejectsSecondBlockInRoot()
        {
            var media = new XmlMedia();
            media.Block("root");

            Assert.Throws<InvalidOperationException>(() =>
                media.Block("root2")
            );
        }

        [Fact]
        public void CreatesBlockInProp()
        {
            var media = new XmlMedia();
            media.Block("root")
                .Prop("my-block")
                .Block("contents");

            Assert.Contains(
                "1",
                new XMLCursor(media.Content())
                    .Values("count(/root/my-block/contents)")
            );
        }

        [Fact]
        public void CreatesBlockInArray()
        {
            var media = new XmlMedia();
            media.Array("array", "item")
                .Block("item")
                .Prop("prop")
                .Put("eller");

            Assert.Equal(
                "<array><item><prop>eller</prop></item></array>",
                media.Content().ToString(SaveOptions.DisableFormatting)
            );
        }

        [Fact]
        public void RejectsBlockInArrayWithDifferentName()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new XmlMedia()
                    .Array("array", "item")
                    .Block("other-name")
            );
        }

        [Fact]
        public void BuildsBlockInBlock()
        {
            var media = new XmlMedia();
            media.Block("root")
                .Block("contents");
            Assert.Equal(
                "<root><contents /></root>",
                media.Content().ToString(SaveOptions.DisableFormatting)
            );
        }

        [Fact]
        public void CreatesArrayAtRoot()
        {
            var media = new XmlMedia();
            media.Array("root", "key");

            Assert.Equal(
                "<root />",
                media.Content().ToString()
            );
        }

        [Fact]
        public void CreatesArrayInBlock()
        {
            var media = new XmlMedia();

            media
                .Block("root")
                .Array("keys", "key");

            Assert.Equal(
                "<root><keys /></root>",
                media.Content().ToString(System.Xml.Linq.SaveOptions.DisableFormatting)
            );
        }

        [Fact]
        public void CreatesArrayInArray()
        {
            var media = new XmlMedia();

            media
                .Array("keys", "key")
                .Array("subarray", "subkey");

            Assert.Equal(
                "<keys><subarray /></keys>",
                media.Content().ToString(System.Xml.Linq.SaveOptions.DisableFormatting)
            );
        }

        [Fact]
        public void RejectsArrayInProp()
        {
            var media = new XmlMedia();

            Assert.Throws<InvalidOperationException>(() =>
                media
                .Block("root")
                .Prop("erty")
                .Array("items", "item")
            );
        }

        [Fact]
        public void PutsValueToProp()
        {
            var media = new XmlMedia();

            media.Block("root")
                .Prop("key")
                .Put("lock");

            Assert.Equal(
                "lock",
                new XMLCursor(media.Content()).Values("/root/key/text()")[0]
            );
        }

        [Fact]
        public void PutsValueToArray()
        {
            var media = new XmlMedia();

            media
                .Array("items", "item")
                .Put("ei");

            Assert.Contains(
                "ei",
                new XMLCursor(media.Content()).Values("/items/item/text()")[0]
            );
        }

        [Fact]
        public void RejectsValueInBlock()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new XmlMedia()
                    .Block("root")
                    .Put("lock")
            );
        }

        [Fact]
        public void RejectsDuplicateKeyForProp()
        {
            IMedia<XNode> media = new XmlMedia();

            var block = media.Block("root");
            block
                .Prop("key")
                .Put("lock");

            Assert.Throws<InvalidOperationException>(() =>
                block.Prop("key")
            );
        }

        [Fact]
        public void RejectsDuplicateKeyForBlock()
        {
            IMedia<XNode> media = new XmlMedia();

            media.Block("key");

            Assert.Throws<InvalidOperationException>(() =>
                media.Block("key")
            );
        }

        [Fact]
        public void RejectsDuplicateKeyForArray()
        {
            IMedia<XNode> media = new XmlMedia();

            media.Array("array", "item");

            Assert.Throws<InvalidOperationException>(() =>
                media.Array("array", "item")
            );
        }

        [Fact]
        public void RejectsEmptyBlockAsRoot()
        {
            IMedia<XNode> media = new XmlMedia();
            Assert.Contains("You are trying to make a block without a name",
                Assert.Throws<InvalidOperationException>(() =>
                    media.Block("")
                ).Message
            );
        }

        [Fact]
        public void EscapesXmlChars()
        {
            var media = new XmlMedia();
            media.Block("root")
                .Prop("key")
                .Put("<root><key>value</key></root>");

            Assert.Contains(
                "&lt;root&gt;&lt;key&gt;value&lt;/key&gt;&lt;/root&gt;",
                media.Content().ToString()
            );
        }
    }
}
