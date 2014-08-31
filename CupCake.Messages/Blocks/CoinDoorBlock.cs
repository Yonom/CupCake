using System;

namespace CupCake.Messages.Blocks
{
    public enum CoinDoorBlock
    {
        [Obsolete("Use CoinDoorBlock.CoinDoor instead.")]
        BlockDoorCoinDoor = Block.CoinDoor,
        [Obsolete("Use CoinDoorBlock.CoinGate instead.")]
        BlockGateCoinGate = Block.CoinGate,

        CoinDoor = Block.CoinDoor,
        CoinGate = Block.CoinGate
    }
}