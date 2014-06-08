using CupCake.Core.Log;

namespace CupCake.HostAPI.IO
{
    public interface IIOSyntaxProvider
    {
        string ParseOutput(LogEventArgs e);
        string ParseInput(InputEvent e);
    }
}