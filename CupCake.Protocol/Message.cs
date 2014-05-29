namespace CupCake.Protocol
{
    public enum Message
    {
        Authentication = 0x00,
        SetData = 0x10,
        Input = 0x20,
        Output = 0x21,
        Title = 0x22,
        WrongAuth = 0xF0,
    }
}
