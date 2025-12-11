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
    /// Multiple contents.
    /// </summary>
    public sealed class BxChain : IBrix
    {
        private readonly IEnumerable<IBrix> printables;

        /// <summary>
        /// Multiple contents.
        /// </summary>
        public BxChain(params IBrix[] more) : this(
            new ManyOf<IBrix>(more)
        )
        { }

        /// <summary>
        /// Multiple contents.
        /// </summary>
        public BxChain(IBrix printable, IEnumerable<IBrix> printables) : this(
            new Joined<IBrix>(
                new ManyOf<IBrix>(printable),
                printables
            )
        )
        { }

        /// <summary>
        /// Multiple contents.
        /// </summary>
        public BxChain(IEnumerable<IBrix> printables)
        {
            this.printables = printables;
        }

        public T Print<T>(IMedia<T> media)
        {
            foreach (var printable in this.printables)
            {
                printable.Print(media);
            }
            return media.Content();
        }
    }
}
