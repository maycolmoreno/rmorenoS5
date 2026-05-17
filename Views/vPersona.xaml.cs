using rmorenoS5.Model;
using rmorenoS5.Repositorio;

namespace rmorenoS5.Views
{
    public partial class vPersona : ContentPage
    {
        private readonly PersonRepository _repo;
        private int _idEditando = -1;

        public vPersona()
        {
            InitializeComponent();
            _repo = new PersonRepository();
        }

        private async void OnAgregarClicked(object? sender, EventArgs e)
        {
            string nombre = NombreEntry.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(nombre))
            {
                await DisplayAlertAsync("Error", "Ingresa un nombre.", "OK");
                return;
            }

            _repo.AgregarPersona(new Persona { Nombre = nombre });
            NombreEntry.Text = string.Empty;
            CargarPersonas();
        }

        private void OnEditarClicked(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is Persona persona)
            {
                _idEditando = persona.Id;
                NombreEntry.Text = persona.Nombre;
                BtnActualizar.IsVisible = true;
            }
        }

        private async void OnActualizarClicked(object? sender, EventArgs e)
        {
            string nombre = NombreEntry.Text?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(nombre))
            {
                await DisplayAlertAsync("Error", "Ingresa un nombre.", "OK");
                return;
            }

            _repo.EditarPersona(new Persona { Id = _idEditando, Nombre = nombre });
            NombreEntry.Text = string.Empty;
            BtnActualizar.IsVisible = false;
            _idEditando = -1;
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
