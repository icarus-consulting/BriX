using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace BriX.Media
{
    /// <summary>
    /// a media which secures xml content
    /// by removing the "\0" characters
    /// </summary>
    public sealed class SecureXmlMedia : IMedia<XNode>
    {
        private readonly IMedia<XNode> origin;

        /// <summary>
        /// a media which secures xml content
        /// by removing "\0" characters
        /// </summary>
        public SecureXmlMedia(IMedia<XNode> origin)
        {
            this.origin = origin;
        }

        /// <summary>
        /// returns the original array
        /// </summary>
        public IMedia<XNode> Array(string arrayName, string itemName)
        {
            return new SecureXmlMedia(this.origin.Array(arrayName, itemName));
        }

        /// <summary>
        /// returns the original block
        /// </summary>
        public IMedia<XNode> Block(string name)
        {
            return new SecureXmlMedia(this.origin.Block(name));
        }

        /// <summary>
        /// returns the original content
        /// </summary>
        public XNode Content()
        {
            return this.origin.Content();
        }

        /// <summary>
        /// returns the original prop
        /// </summary>
        public IMedia<XNode> Prop(string name)
        {
            return new SecureXmlMedia(this.origin.Prop(name));
        }

        /// <summary>
        /// returns secure xml media without "\0" characters
        /// </summary>
        public IMedia<XNode> Put(string value)
        {
            var validValue = //Regex.Replace(value, @"\0", string.Empty);
                new string(value.Where(c => XmlConvert.IsXmlChar(c)).ToArray());
            return new SecureXmlMedia(
                this.origin.Put(
                    validValue
                //value
                )
            );
        }
    }
}
