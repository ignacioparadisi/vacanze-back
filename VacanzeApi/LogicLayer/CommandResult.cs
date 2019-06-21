namespace vacanze_back.VacanzeApi.Common
{
    public interface CommandResult<T>: Command
    {
        T GetResult();
    }
}