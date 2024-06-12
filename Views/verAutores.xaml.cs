using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace EscritoresApp.Views
{
    public partial class verAutores : ContentPage
    {
        private Controllers.AutorController AutorController;
        private List<Models.Autor> autores;

        public verAutores()
        {
            InitializeComponent();
            AutorController = new Controllers.AutorController();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Obtiene la lista de autores de la base de datos
            autores = await AutorController.getListAutor();

            // Coloca la lista en el CollectionView
            collectionView.ItemsSource = autores;
        }

        private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            BuscarAutores(searchBar.Text);
        }

        private void BuscarAutores(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                collectionView.ItemsSource = autores;
            }
            else
            {
                var results = autores
                    .Where(author => author.Nombres?.ToLowerInvariant().Contains(query.ToLowerInvariant()) == true ||
                                     author.Nacionalidad?.ToLowerInvariant().Contains(query.ToLowerInvariant()) == true)
                    .ToList();

                collectionView.ItemsSource = results;
            }
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                collectionView.ItemsSource = autores;
            }
            else
            {
                BuscarAutores(e.NewTextValue);
            }
        }

        private void btnRegresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
