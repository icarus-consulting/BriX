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
using System.Globalization;
using Yaapii.Atoms;

namespace BriX
{
    /// <summary>
    /// A property which has a name and a value.
    /// </summary>
    public sealed class BxProp : IBrix
    {
        private readonly string name;
        private readonly Func<string> value;

        /// <summary>
        /// A property which has a name and a value.
        /// </summary>
        public BxProp(string name, long value) : this(name, () => value.ToString(CultureInfo.InvariantCulture))
        { }

        /// <summary>
        /// A property which has a name and a value.
        /// </summary>
        public BxProp(string name, long value, IFormatProvider culture) : this(name, () => value.ToString(culture))
        { }

        /// <summary>
        /// A property which has a name and a value.
        /// </summary>
        public BxProp(string name, float value) : this(name, () => value.ToString(CultureInfo.InvariantCulture))
        { }

        /// <summary>
        /// A property which has a name and a value.
        /// </summary>
        public BxProp(string name, float value, IFormatProvider culture) : this(name, () => value.ToString(culture))
        { }

        /// <summary>
        /// A property which has a name and a value.
        /// </summary>
        public BxProp(string name, double value) : this(name, () => value.ToString(CultureInfo.InvariantCulture))
        { }

        /// <summary>
        /// A property which has a name and a value.
        /// </summary>
        public BxProp(string name, double value, IFormatProvider culture) : this(name, () => value.ToString(culture))
        { }

        /// <summary>
        /// A property which has a name and a value.
        /// </summary>
        public BxProp(string name, int value) : this(name, () => value.ToString(CultureInfo.InvariantCulture))
        { }

        /// <summary>
        /// A property which has a name and a value.
        /// </summary>
        public BxProp(string name, int value, IFormatProvider culture) : this(name, () => value.ToString(culture))
        { }

        /// <summary>
        /// A property which has a name and a value.
        /// </summary>
        public BxProp(string name, INumber value) : this(name, () => value.AsDouble().ToString())
        { }

        /// <summary>
        /// A property which has a name and a value.
        /// </summary>
        public BxProp(string name, IScalar<string> value) : this(name, () => value.Value())
        { }

        /// <summary>
        /// A property which has a name and a value.
        /// </summary>
        public BxProp(string name, IText value) : this(name, () => value.AsString())
        { }

        /// <summary>
        /// A property which has a name and a value.
        /// </summary>
        public BxProp(string name, string value) : this(name, () => value)
        { }

        /// <summary>
        /// A property which has a name and a value.
        /// </summary>
        public BxProp(string name, bool value) : this(name, () => value.ToString(CultureInfo.InvariantCulture))
        { }

        /// <summary>
        /// A property which has a name and a value.
        /// </summary>
        public BxProp(string name, Func<string> value)
        {
            this.name = name;
            this.value = value;
        }

        public T Print<T>(IMedia<T> media)
        {
            var prop = media.Prop(this.name);
            prop.Put(this.value());
            return media.Content();
        }

        public override string ToString()
        {
            return $"PROP({this.name}:{this.value})";
        }
    }
}
