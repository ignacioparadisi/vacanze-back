using System.Collections.Generic;

namespace vacanze_back.VacanzeApi.LogicLayer.Command
{
    public interface CommandResult<T> : Command
    {
        T GetResult();
    }
}