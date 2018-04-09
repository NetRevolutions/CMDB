using System;

namespace JARASOFT.CMDB.WebSite.Infraestructure
{
    [Serializable]
    public class GuidToken
    {
        private readonly Guid guid;

        /// <summary>
        /// Creates a GuidToken with a new <c>Guid</c>
        /// </summary>
        public GuidToken()
        {
            guid = Guid.NewGuid();
        }

        /// <summary>
        /// Creates a GuidToken with a defined <c>Guid</c>
        /// </summary>
        /// <param name="guid">User-provided <c>Guid</c></param>
        public GuidToken(Guid guid)
        {
            this.guid = guid;
        }

        /// <summary>
        /// Returns the ToString representation of the <c>Guid</c>
        /// </summary>
        public string Value
        {
            get { return guid.ToString(); }
        }
    }
}