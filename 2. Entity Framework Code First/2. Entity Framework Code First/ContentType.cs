namespace _2.Entity_Framework_Code_First
{
    using System.ComponentModel;

    public enum ContentType
    {
        Application,
        [Description("Pdf or application")] Pdf,
        Zip
    }
}
