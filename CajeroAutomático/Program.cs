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
            Usuario usuario = new Usuario("1234", "0001", 0m);
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
                            Console.WriteLine("\n=== DEPÓSITO ===");
                            Console.Write("Ingrese la cantidad a depositar: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal montoDeposito))
                            {
                                if (UsuarioIngresado.Deposito(montoDeposito))
                                {
                                    Console.WriteLine("Depósito realizado exitosamente.");
                                }
                                else
                                {
                                    Console.WriteLine("Error: La cantidad debe ser mayor a cero.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error: Ingrese un monto válido.");
                            }
                            break;

                        case "2":
                            Console.WriteLine("\n=== RETIRO ===");
                            Console.Write("Ingrese la cantidad a retirar: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal montoRetiro))
                            {
                                if (UsuarioIngresado.Retiro(montoRetiro))
                                {
                                    Console.WriteLine("Retiro realizado exitosamente.");
                                }
                                else
                                {
                                    Console.WriteLine("Error: Fondos insuficientes o monto inválido.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error: Ingrese un monto válido.");
                            }
                            break;

                        case "3":
                            Console.WriteLine("\n=== CONSULTA DE SALDO ===");
                            UsuarioIngresado.ConsultarSaldo();
                            break;

                        case "4":
                            Console.WriteLine("\n=== ÚLTIMOS 5 MOVIMIENTOS ===");
                            var movimientos = UsuarioIngresado.UltimasMovimientos();
                            if (movimientos.Count > 0)
                            {
                                foreach (var movimiento in movimientos)
                                {
                                    Console.WriteLine(movimiento.ToString());
                                }
                            }
                            else
                            {
                                Console.WriteLine("No hay movimientos registrados.");
                            }
                            break;

                        case "5":
                            Console.WriteLine("\n=== CAMBIO DE CLAVE ===");
                            Console.Write("Ingrese su PIN actual: ");
                            string pinActual = Console.ReadLine();
                            Console.Write("Ingrese su nuevo PIN (4-8 caracteres): ");
                            string nuevoPin = Console.ReadLine();

                            if (UsuarioIngresado.CambiarPin(pinActual, nuevoPin))
                            {
                                Console.WriteLine("Clave cambiada exitosamente.");
                            }
                            else
                            {
                                Console.WriteLine("Error: PIN actual incorrecto o nuevo PIN inválido.");
                            }
                            break;

                        case "6":
                            Console.WriteLine("Gracias por usar el cajero automático. ¡Hasta pronto!");
                            Continuar = false;
                            break;

                        default:
                            Console.WriteLine("Opción no válida. Por favor, seleccione una opción del 1 al 6.");
                            break;
                    }

                    if (Continuar && Opcion != "6")
                    {
                        Console.WriteLine("\nPresione cualquier tecla para continuar...");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("Credenciales incorrectas. Acceso denegado.");
            }

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
