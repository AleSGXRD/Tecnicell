namespace Tecnicell.Server.Models.ViewModel
{
    public class ImageViewModel
    {
        public string Imagecode { get; set; } = null!;

        public string? Name { get; set; }

        public byte[]? File { get; set; }
    }
}
