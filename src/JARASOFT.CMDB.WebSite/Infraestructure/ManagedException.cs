using System;

namespace JARASOFT.CMDB.WebSite.Infraestructure
{
    public class ManagedException : ApplicationException
    {
        public ManagedException() { }
        public ManagedException(Guid ErrorId, string Message = "We found an error in the application, please record this id: {0} to track it.")
            : base(string.Format(Message, ErrorId))
        {

        }
    }
}