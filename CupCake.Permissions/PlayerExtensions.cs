using CupCake.Players;

namespace CupCake.Permissions
{
    public static class PlayerExtensions
    {
        public static Group GetGroup(this Player p)
        {
            Group g;
            p.Metadata.GetMetadata("Group", out g);
            return g;
        }

        public static void SetGroup(this Player p, Group group)
        {
            p.Metadata.SetMetadata("Group", group);
        }
    }
}