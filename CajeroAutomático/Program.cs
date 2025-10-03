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
            Console.WriteLine("Cajero");
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
                Console.WriteLine("Bienvenido");

                bool Continuar = true;
                while (Continuar)
                {
                    // Menu de opciones del cajero
                    Console.WriteLine("\n===== MENÚ CAJERO AUTOMÁTICO =====");
                    Console.WriteLine("1. Depósito");
                    Console.WriteLine("2. Retiro");
                    Console.WriteLine("3. Consulta de Saldo");
                    Console.WriteLine("4. Consulta últimos 5 movimientos");
                    Console.WriteLine("5. Cambio de Clave");
                    Console.WriteLine("6. Salir");
                    Console.Write("Seleccione una opción: ");
                    string Opcion = Console.ReadLine();
                    switch (Opcion)
                    {
                        case "1":
                            Console.WriteLine("DEPÓSITO");
                            {

                            }
                        break;
                        case "2":
                            Console.WriteLine("");
                            break;
                        case "3":
                            Console.WriteLine("");
                            break;
                        case "4":
                            Console.WriteLine("");
                            break;
                        case "5":
                            Console.WriteLine("");
                            break;
                        case "6":
                            Console.WriteLine("");
                            break;
                    }
                }
        
            }

        } 

    }
}
