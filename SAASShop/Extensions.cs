using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SAASShop.Pages
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            };

            session.SetString(key, JsonConvert.SerializeObject(value, settings));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }

    public static class EnumExtensions
    {
        public static string GetEnumDescription<T>(this T enumValue) where T : struct
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(description);

            if (fieldInfo != null)
            {
                if (Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    description = attribute.Description;
                }
            }

            return description;
        }
    }

    public class ImageManager
    {
        public static bool IsImageValid(IFormFile photo)
        {
            if (photo == null || photo.Length < 0)
                return false;

            if (photo.Length > 512000 * 10)
                return false;

            string fileName = photo.FileName.ToLower();

            if (fileName.LastIndexOf(".jpeg") <= 0 && fileName.LastIndexOf(".jpg") <= 0 &&
                fileName.LastIndexOf(".png") <= 0 && fileName.LastIndexOf(".bmp") <= 0)
                return false;

            return true;
        }

        public static byte[] GetImageBytes(IFormFile photoFile)
        {
            try
            {
                using (var memStream = new MemoryStream())
                {
                    const int ConstWidth = 100;
                    const int ConstHeight = 150;

                    int desiredWidth = ConstWidth;
                    int desiredHeight = ConstHeight;

                    photoFile.OpenReadStream().CopyTo(memStream);
                    byte[] photoArray = memStream.ToArray();

                    var webimg = Image.Load(photoArray);

                    int imgWidth = webimg.Width;
                    int imgHeight = webimg.Height;

                    if (imgWidth <= desiredWidth && imgHeight <= desiredHeight)
                    {
                        desiredWidth = imgWidth;
                        desiredHeight = imgHeight;
                    }
                    else
                    {
                        var newWidth = imgWidth * desiredHeight / imgHeight;
                        if (newWidth > desiredWidth)
                        {
                            // Resize with width instead
                            desiredHeight = imgHeight * desiredWidth / imgWidth;
                            newWidth = desiredWidth;
                        }

                        desiredWidth = newWidth;
                    }

                    webimg.Mutate(x => x.Resize(desiredWidth, desiredHeight));

                    MemoryStream desStream = new MemoryStream();
                    webimg.Save(desStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
                    byte[] finalBits = desStream.ToArray();
                    desStream.Dispose();

                    return finalBits;
                }
            }
            catch { }

            return null;
        }
    }

    public static class Extensions
    {
        public static string DisplayImage(this bool value)
        {
            if (value)
            {
                return "/images/Checked.jpg";
            }
            else
            {
                return "/images/UnChecked.jpg";
            }
        }

        public static string GetEnumCategory<T>(this T enumValue) where T : struct
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(description);

            if (fieldInfo != null)
            {
                if (Attribute.GetCustomAttribute(fieldInfo, typeof(CategoryAttribute)) is CategoryAttribute attribute)
                {
                    description = attribute.Category;
                }
            }

            return description;
        }
    }
}