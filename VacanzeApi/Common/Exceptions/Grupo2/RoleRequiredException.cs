using System;

namespace vacanze_back.VacanzeApi.Common.Exceptions
{
    public class RoleRequiredException : GeneralException
    {
        public RoleRequiredException() : base()
        {
        }

        public RoleRequiredException(string message) : base(message)
        {
        }
    }
}