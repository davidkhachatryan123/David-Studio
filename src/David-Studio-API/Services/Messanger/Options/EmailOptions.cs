namespace Messanger.Options
{
    public class EmailOptions
    {
        public string Prefix { get; set; } = null!;

        public string FromForIdentity { get; set; } = null!;
        public string FromForAnswer { get; set; } = null!;
    }
}
