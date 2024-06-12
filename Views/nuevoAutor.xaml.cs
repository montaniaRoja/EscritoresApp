namespace EscritoresApp.Views;
using System.Collections.ObjectModel;

public partial class nuevoAutor : ContentPage
{
    //Clase que permite guardar objetos que estan vinculados a un elemento de interface
    ObservableCollection<string> countries;

    Controllers.AutorController controller;
    FileResult photo; //Para tomar foto

    string nacionalidad;
    public nuevoAutor()
    {
        InitializeComponent();

        controller = new Controllers.AutorController();

        countries = new ObservableCollection<string>
            {
                "Argentina",
                "Belize",
                "Costa Rica",
                "El Salvador",
                "Guatemala",
                "Honduras",
                "Nicaragua",
                "Panama",
                "Mexico",
                "Colombia"
        };

        countryPicker.ItemsSource = countries;
    }

    public nuevoAutor(Controllers.AutorController dbPath)
    {
        InitializeComponent();
        controller = dbPath;

        countries = new ObservableCollection<string>
            {
                "Argentina",
                "Belize",
                "Costa Rica",
                "El Salvador",
                "Guatemala",
                "Honduras",
                "Nicaragua",
                "Panama",
                "Mexico",
                "Colombia"
            };

        countryPicker.ItemsSource = countries;
    }

    public async Task<string?> GetImg64()
    {
        if (photo != null)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Stream stream = await photo.OpenReadAsync())
                {
                    await stream.CopyToAsync(ms);
                    byte[] data = ms.ToArray();
                    string base64 = Convert.ToBase64String(data);
                    return base64;
                }
            }
        }
        return null;
    }


    public byte[]? GetImageArray()
    {
        if (photo != null)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Stream stream = photo.OpenReadAsync().Result;
                stream.CopyTo(ms);
                byte[] data = ms.ToArray();



                return data;
            }
        }
        return null;
    }

    private void countryPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        nacionalidad = countryPicker.SelectedItem as string;
    }

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        string Nombres = txtNombres.Text;

        if (string.IsNullOrEmpty(Nombres))
        {
            await DisplayAlert("Error", "Por favor ingrese el nombre del autor", "OK");
            return;
        }
        else if (string.IsNullOrEmpty(nacionalidad))
        {
            await DisplayAlert("Error", "Por favor seleccione la nacionalidad del autor", "OK");
            return;
        }

        // Esperar el resultado de GetImg64
        string fotoBase64 = await GetImg64();

        var autor = new Models.Autor
        {
            Nombres = txtNombres.Text,
            Nacionalidad = nacionalidad,
            Foto = fotoBase64
        };

        try
        {
            if (controller != null)
            {
                if (await controller.storeAutor(autor) > 0)
                {
                    await DisplayAlert("Aviso", "Registro Ingresado con Éxito!", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrió un Error", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un Error: {ex.Message}", "OK");
        }
    }


    private void btnRegresar_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void btnfoto_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Capturar la foto usando MediaPicker
            photo = await MediaPicker.CapturePhotoAsync();

            if (photo != null)
            {
                string photoPath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using (Stream sourcePhoto = await photo.OpenReadAsync())
                using (FileStream streamLocal = File.OpenWrite(photoPath))
                {
                    await sourcePhoto.CopyToAsync(streamLocal); // Guardar la foto localmente

                    // Mostrar la foto en el Image
                    imgFoto.Source = ImageSource.FromFile(photoPath);
                }
            }
        }
        catch (FeatureNotSupportedException fnsEx)
        {
            // Cuando la característica no es soportada en el dispositivo
            await DisplayAlert("Error", "Captura de fotos no soportada en este dispositivo.", "OK");
        }
        catch (PermissionException pEx)
        {
            // Cuando los permisos no son concedidos
            await DisplayAlert("Error", "Permisos de cámara no concedidos.", "OK");
        }
        catch (Exception ex)
        {
            // Manejo de cualquier otra excepción
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }


}