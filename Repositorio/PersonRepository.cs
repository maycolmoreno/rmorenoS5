using SQLite;
using rmorenoS5.Model;
using rmorenoS5.Utils;

namespace rmorenoS5.Repositorio
{
    public class PersonRepository
    {
        private SQLiteConnection _conn;
        public string StatusMessage { get; set; }

        private void Init()
        {
            if (_conn != null)
                return;

            string dbPath = FileAccessHelper.GetLocalFilePath("personas.db3");
            _conn = new SQLiteConnection(dbPath);
            _conn.CreateTable<Persona>();
        }

        public List<Persona> GetAllperson()
        {
            try
            {
                Init();
                return _conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("ERROR" + ex.Message);
            }
            return new List<Persona>();
        }

        public void AgregarPersona(Persona persona)
        {
            try
            {
                Init();
                _conn.Insert(persona);
                StatusMessage = string.Format("{0} registro(s) agregado(s) [Nombre: {1}]", 1, persona.Nombre);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("ERROR al agregar {0}. Mensaje: {1}", persona.Nombre, ex.Message);
            }
        }

        public void EliminarPersona(Persona persona)
        {
            try
            {
                Init();
                _conn.Delete(persona);
                StatusMessage = string.Format("Persona eliminada [Id: {0}]", persona.Id);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("ERROR al eliminar. Mensaje: {0}", ex.Message);
            }
        }
    }
}
