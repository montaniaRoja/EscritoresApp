using System;
using System.Globalization;
using System.IO; // Asegúrate de incluir System.IO para MemoryStream

namespace EscritoresApp.Controllers
{
    public class Base64Image : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource imageSource = null;
            if (value != null)
            {
                string base64 = (string)value;
                byte[] fotobyte = System.Convert.FromBase64String(base64);
                var stream = new MemoryStream(fotobyte);

                imageSource = ImageSource.FromStream(() => stream);
            }

            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
