using Org.BouncyCastle.Crypto.Macs;

namespace MTGDeckBuilder.Services.DeckServices
{
    public enum SortOption
    {
        NameAsc, NameDesc
    }
    public static class EnumUtil
    {
        public static List<(int id, string name)> ToList<T>() where T : struct, Enum =>
            Enum.GetValues<T>()
            .Select((v, k) => (k, v.ToString())).ToList();
    }
}
