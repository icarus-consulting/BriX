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

using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace BriX
{
    /// <summary>
    /// A block which can be printed to a media.
    /// </summary>
    public sealed class BxBlock : IBrix
    {
        private readonly string name;
        private readonly IBrix content;

        /// <summary>
        /// A block which can be printed to an array.
        /// </summary>
        public BxBlock(IBrix content) : this(string.Empty, content)
        { }

        /// <summary>
        /// A block which can be printed to an array.
        /// </summary>
        public BxBlock(params IBrix[] content) : this(string.Empty, new ManyOf<IBrix>(content))
        { }

        /// <summary>
        /// A block which can be printed to an array.
        /// </summary>
        public BxBlock(IEnumerable<IBrix> content) : this(string.Empty, new BxChain(content))
        { }

        /// <summary>
        /// A block which can be printed to a media.
        /// </summary>
        public BxBlock(string name, params IBrix[] content) : this(name, new ManyOf<IBrix>(content))
        { }

        /// <summary>
        /// A block which can be printed to a media.
        /// </summary>
        public BxBlock(string name, IEnumerable<IBrix> content) : this(name, new BxChain(content))
        { }

        /// <summary>
        /// A block which can be printed to a media.
        /// </summary>
        public BxBlock(string name, IBrix content)
        {
            this.name = name;
            this.content = content;
        }

        public T Print<T>(IMedia<T> media)
        {
            this.content.Print(media.Block(this.name));
            return media.Content();
        }

        public override string ToString()
        {
            return $"BLOCK({this.name})";
        }
    }
}
