namespace Orenes.Services.Interfaces
{
    public interface ISecurityService
    {

        string GenerarToken(string userName, string userType);
    }
}
