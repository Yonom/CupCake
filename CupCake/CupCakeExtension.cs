using System;
using BotBits;

namespace CupCake
{
    public sealed class CupCakeExtension : Extension<CupCakeExtension>
    {
        protected override void Initialize(BotBitsClient client, object args)
        {
            var cupCakeArgs = (CupCakeArgs)args;
            CupCakeClient
                .Of(client)
                .Initialize(cupCakeArgs);
        }

        [Obsolete("Use the other overload with a CupCakeArgs parameter.", true)]
        public static new void LoadInto(BotBitsClient client)
        {
            throw new NotSupportedException();
        }

        public static void LoadInto(BotBitsClient client, CupCakeArgs args)
        {
            LoadInto(client, (object)args);
        }
    }
}