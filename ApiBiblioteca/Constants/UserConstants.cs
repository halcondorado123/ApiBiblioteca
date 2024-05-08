using ApiBiblioteca.Models;
using System.Net.Mail;

namespace ApiBiblioteca.Constants
{
    public class UserConstants
    {
        public static List<UserModel> Usuarios = new List<UserModel>()
        {
            new UserModel() { UserName = "usuarioPrueba01", Password = "admin123", EmailAdress = "usuariopruebas@gmail1.com", Rol = "Administrador", LastName = "Usuario01" , Name = "Jhonattan"},
            new UserModel() { UserName = "usuarioPrueba02", Password = "admin123", EmailAdress = "usuariopruebas@gmail2.com", Rol = "Administrador", LastName = "Usuario02" , Name = "Johanna"},

        };
    }
}
