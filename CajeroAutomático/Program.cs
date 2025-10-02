using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CajeroAutomático
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Instancia de la clase Usuario
            Usuario usuario = new Usuario("1234", "0001", 1000m);
            usuario.GuardarDatos();
            // Inicio de sesion del cajero
            Console.Write("Ingrese su Numero de Cuenta: ");
            string CuentaIngresada = Console.ReadLine();
            Console.Write("Ingrese su Pin: ");
            string PinIngresado = Console.ReadLine();
            // Validacion de datos cargados desde el archivo
            Usuario UsuarioIngresado = Usuario.ValidacionDeDatos(CuentaIngresada);
            // Verificacion de datos para el ingreso
            if(UsuarioIngresado != null && UsuarioIngresado.VerificacionCredenciales(CuentaIngresada, PinIngresado))
            {
                Console.WriteLine("Acceso permitido");
            }

        } 

    }
}
