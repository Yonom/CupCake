using CupCake.Core.Log;

namespace CupCake.Server.IO
{
    public interface IOutputSyntaxProvider
    {
        string ParseOutput(LogEventArgs e);
        string ParseInput(InputEvent e);
    }
}