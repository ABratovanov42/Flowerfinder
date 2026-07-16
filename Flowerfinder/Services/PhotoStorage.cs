using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Flowerfinder.Services
{
    /// <summary>
    /// Saves uploaded photos under wwwroot/images/uploads — resized to a
    /// sensible web size, re-encoded as JPEG, rotated the right way up,
    /// and stripped of camera metadata (which can include GPS location).
    /// </summary>
    public class PhotoStorage
    {
        public const long MaxBytes = 10 * 1024 * 1024;
        public static readonly string[] AllowedTypes = { "image/jpeg", "image/png", "image/webp" };
        private const int MaxEdge = 1400;

        private readonly IWebHostEnvironment _env;

        public PhotoStorage(IWebHostEnvironment env) => _env = env;

        /// <summary>Quick pre-checks for an upload; the message is user-facing.</summary>
        public static string? Problem(IFormFile? photo)
        {
            if (photo == null || photo.Length == 0) return "Choose a photo first.";
            if (photo.Length > MaxBytes) return "That photo is over 10 MB — try a smaller one.";
            if (!AllowedTypes.Contains(photo.ContentType, StringComparer.OrdinalIgnoreCase))
                return "That doesn't look like a photo — JPEG, PNG or WebP, please.";
            return null;
        }

        /// <summary>
        /// Saves the image and returns its web path ("/images/uploads/….jpg"),
        /// or null when the data isn't a decodable image.
        /// </summary>
        public async Task<string?> SaveAsync(Stream stream, CancellationToken ct = default)
        {
            Image image;
            try
            {
                image = await Image.LoadAsync(stream, ct);
            }
            catch (Exception ex) when (ex is UnknownImageFormatException or InvalidImageContentException)
            {
                return null;
            }

            using (image)
            {
                image.Mutate(x =>
                {
                    x.AutoOrient(); // honour the camera's rotation flag before we drop metadata
                    if (image.Width > MaxEdge || image.Height > MaxEdge)
                        x.Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size(MaxEdge, MaxEdge) });
                });
                image.Metadata.ExifProfile = null;
                image.Metadata.XmpProfile = null;

                var dir = Path.Combine(_env.WebRootPath, "images", "uploads");
                Directory.CreateDirectory(dir);
                var name = Guid.NewGuid().ToString("N") + ".jpg";
                await image.SaveAsync(Path.Combine(dir, name), new JpegEncoder { Quality = 82 }, ct);
                return "/images/uploads/" + name;
            }
        }

        /// <summary>Removes a previously uploaded photo (never touches seed images).</summary>
        public void Delete(string? webPath)
        {
            if (string.IsNullOrWhiteSpace(webPath) || !webPath.StartsWith("/images/uploads/")) return;
            var file = Path.Combine(_env.WebRootPath, "images", "uploads", Path.GetFileName(webPath));
            if (File.Exists(file)) File.Delete(file);
        }
    }
}
