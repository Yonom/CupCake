using CupCake.Core.Log;

namespace CupCake.Server.Output
{
    public interface IOutputSyntaxProvider
    {
        string Parse(LogEventArgs e);
    }
}