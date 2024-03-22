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

namespace BriX
{
    /// <summary>
    /// Prints only if the given condition is matched.
    /// Otherwise does nothing.
    /// </summary>
    public sealed class BxConditional : BrixEnvelope
    {
        /// <summary>
        /// Prints only if the given condition is matched.
        /// Otherwise does nothing.
        /// </summary>
        public BxConditional(bool condition, Func<IBrix> consequence) : this(() => condition, consequence)
        { }

        /// <summary>
        /// Prints only if the given condition is matched.
        /// Otherwise prints the alternative.
        /// </summary>
        public BxConditional(bool condition, Func<IBrix> consequence, Func<IBrix> alternative) : this(() => condition, consequence, alternative)
        { }

        /// <summary>
        /// Prints only if the given condition is matched.
        /// Otherwise does nothing.
        /// </summary>
        public BxConditional(Func<bool> condition, Func<IBrix> consequence) : this(
            condition,
            consequence,
            () => new BxNothing()
        )
        { }

        /// <summary>
        /// Prints only if the given condition is matched.
        /// Otherwise prints the alternative.
        /// </summary>
        public BxConditional(Func<bool> condition, Func<IBrix> consequence, Func<IBrix> alternative) : base(()=>
        {
            IBrix result;
            if(condition())
            {
                result = consequence();
            }
            else
            {
                result = alternative();
            }
            return result;
        })
        { }
    }
}
