namespace BriX.Media
{
    /// <summary>
    /// a media which secures the content
    /// by removing the "\0" characters
    /// </summary>
    public sealed class SecuredMedia<T> : IMedia<T>
    {
        private readonly IMedia<T> origin;

        /// <summary>
        /// a media which secures the content
        /// by removing the "\0" characters
        /// </summary>
        public SecuredMedia(IMedia<T> origin)
        {
            this.origin = origin;
        }

        /// <summary>
        /// returns the original array
        /// </summary>
        public IMedia<T> Array(string arrayName, string itemName)
        {
            return new SecuredMedia<T>(this.origin.Array(arrayName, itemName));
        }

        /// <summary>
        /// returns the original block
        /// </summary>
        public IMedia<T> Block(string name)
        {
            return new SecuredMedia<T>(this.origin.Block(name));
        }

        /// <summary>
        /// returns the original content
        /// </summary>
        public T Content()
        {
            return this.origin.Content();
        }

        /// <summary>
        /// returns the original prop
        /// </summary>
        public IMedia<T> Prop(string name)
        {
            return new SecuredMedia<T>(this.origin.Prop(name));
        }

        /// <summary>
        /// returns secure media without "\0" characters
        /// </summary>
        public IMedia<T> Put(string value)
        {
            return new SecuredMedia<T>(
                this.origin.Put(
                    value.Replace('\0', ' ')
                )
            );
        }
    }
}
