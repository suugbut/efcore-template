namespace MyConfiguration;

public class TheConfiguration
{
    public string KeyString { get; set; } = default!;
    public int KeyInteger { get; set; }
    public bool KeyBoolean { get; set; }

    public override string ToString()
    {
        System.Text.StringBuilder sb = new();
        sb.Append($"KeyString: {KeyString}, ");
        sb.Append($"KeyInteger: {KeyInteger}, ");
        sb.Append($"KeyBoolean: {KeyBoolean}.");
        return sb.ToString();
    }
}