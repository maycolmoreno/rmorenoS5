using rmorenoS5.Model;
using rmorenoS5.Repositorio;

namespace rmorenoS5.Views
{
    public partial class vPersona : ContentPage
    {
        private readonly PersonRepository _repo;

        public vPersona()
        {
            InitializeComponent();
            _repo = new PersonRepository();
        }

        private void OnAgregarClicked(object? sender, EventArgs e)
        {
            string nombre = NombreEntry.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(nombre))
            {
                DisplayAlert("Error", "Ingresa un nombre.", "OK");
                return;
            }

            _repo.AgregarPersona(new Persona { Nombre = nombre });
            NombreEntry.Text = string.Empty;
            CargarPersonas();
        }

        private void OnListarClicked(object? sender, EventArgs e)
        {
            CargarPersonas();
        }

        private async void OnEliminarClicked(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is Persona persona)
            {
                bool confirmar = await DisplayAlert("Eliminar", $"¿Eliminar a {persona.Nombre}?", "Sí", "No");
                if (confirmar)
                {
                    _repo.EliminarPersona(persona);
                    CargarPersonas();
                }
            }
        }

        private void CargarPersonas()
        {
            PersonasCollection.ItemsSource = _repo.GetAllperson();
        }
    }
}
