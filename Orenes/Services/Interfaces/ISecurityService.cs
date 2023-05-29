namespace Orenes.Services.Interfaces
{
    public interface ISecurityService
    {

        string GenerarToken(string userName, string userType);

        public string Encriptar(string clave);

        public bool Desencriptar(string claveEncriptada, string clave);
    }
}
