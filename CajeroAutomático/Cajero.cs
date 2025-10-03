using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CajeroAutomático
{
    internal class Usuario // Clase Usuario
    {
        // Atributos de la clase Usuario
        private string Cuenta;
        private string Pin;
        private decimal Saldo;

        // Constructor
        public Usuario(string cuenta, string pin, decimal saldo)
        {
            this.Cuenta = cuenta;
            this.Pin = pin;
            this.Saldo = saldo;
        }
        // Propiedades para acceder a los atributos privados de Usuario
        // Usando miembros con cuerpo de expresión
        public string cuenta => Cuenta;
        public string pin => Pin;
        public decimal saldo => Saldo;


        public void GuardarDatos() // Metodo para guardar datos
        {
            string Carpeta = "Usuario"; // Este if crea la carpeta Usuario si no esta creada
            if (!Directory.Exists(Carpeta))
            {
                Directory.CreateDirectory(Carpeta);
            }
            string RutaArchivo = Path.Combine(Carpeta, cuenta + ".txt");
            using (StreamWriter Escribir = new StreamWriter(RutaArchivo))
            {
                Escribir.WriteLine(cuenta);
                Escribir.WriteLine(pin);
                Escribir.WriteLine(saldo);
            }
        }
        // Validacion de datos
        public static Usuario ValidacionDeDatos(string NumeroCuenta)
        {
            string Carpeta = "Usuario";
            string RutaArchivo = Path.Combine(Carpeta, NumeroCuenta + ".txt");
            if (File.Exists(RutaArchivo))
            {
                string[] Lineas = File.ReadAllLines(RutaArchivo);
                if (Lineas.Length >= 3)
                {
                    return new Usuario(Lineas[0], Lineas[1], decimal.Parse(Lineas[2]));
                }
            }
            return null;
        }
        // Verificar credenciales
        public bool VerificacionCredenciales(string CuentaIngresada, string PinIngresado)
        {
            return this.cuenta == CuentaIngresada && this.pin == PinIngresado;
        }
        // Transaccion
         public class Transaccion
        {
            public DateTime Fecha { get; set; }
            public string Tipo { get; set; } // "Depósito" o "Retiro"
            public decimal Cantidad { get; set; }
            public decimal SaldoAnterior { get; set; }
            public decimal SaldoNuevo { get; set; }

            public override string ToString()
            {
                return $"{Fecha:dd/MM/yyyy HH:mm} - {Tipo}: ${Cantidad:N2} (Saldo: ${SaldoNuevo:N2})";
            }
        }
        // Historial Transaccion
        private void GuardarTransaccion(string tipo, decimal cantidad, decimal saldoAnterior, decimal saldoNuevo)
        {
            string Carpeta = "Historiales";
            if (!Directory.Exists(Carpeta))
            {
                Directory.CreateDirectory(Carpeta);
            }

            string rutaArchivo = Path.Combine(Carpeta, this.cuenta + "_historial.txt");
            using (StreamWriter escritor = new StreamWriter(rutaArchivo, true)) // true para agregar al final
            {
                Transaccion transaccion = new Transaccion
                {
                    Fecha = DateTime.Now,
                    Tipo = tipo,
                    Cantidad = cantidad,
                    SaldoAnterior = saldoAnterior,
                    SaldoNuevo = saldoNuevo
                };
                escritor.WriteLine($"{transaccion.Fecha:yyyy-MM-dd HH:mm:ss}|{tipo}|{cantidad}|{saldoAnterior}|{saldoNuevo}");
            }
        }
        // Depositar
        public bool Deposito(decimal Cantidad)
        {
            if (Cantidad > 0)
            {
                decimal SaldoAnterior = this.Saldo;
                this.Saldo += Cantidad;
                GuardarDatos();
                GuardarTransaccion("Depósito", Cantidad, SaldoAnterior, this.saldo);
                return true;
            }
            return false;
        }
        // Retiro
        public bool Retiro(decimal Cantidad)
        {
            if(Cantidad > 00 && Cantidad <= this.saldo)
            {
                decimal SaldoAnterior = this.saldo;
                this.Saldo -= Cantidad;
                GuardarDatos();
                GuardarTransaccion("Retiro", Cantidad, SaldoAnterior, this.Saldo);
                return true;
            }
            return false;
        }
        public void ConsultarSaldo()
        {
            Console.WriteLine($"Su saldo actual es:  ${this.Saldo:N2}");
        }
        public List<Transaccion> UltimasMovimientos()
        {
            List<Transaccion> transacciones = new List<Transaccion>();
            string Carpeta = "Historiales";
            string RutaArchivo = Path.Combine(Carpeta, this.cuenta + "_historial.txt");

            if (File.Exists(RutaArchivo))
            {
                string[] lineas = File.ReadAllLines(RutaArchivo);

                // Tomar las últimas 5 líneas
                var ultimasLineas = lineas.Reverse().Take(5).Reverse();

                foreach (string linea in ultimasLineas)
                {
                    string[] partes = linea.Split('|');
                    if (partes.Length == 5)
                    {
                        Transaccion transaccion = new Transaccion
                        {
                            Fecha = DateTime.ParseExact(partes[0], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                            Tipo = partes[1],
                            Cantidad = decimal.Parse(partes[2]),
                            SaldoAnterior = decimal.Parse(partes[3]),
                            SaldoNuevo = decimal.Parse(partes[4])
                        };
                        transacciones.Add(transaccion);
                    }
                }
            }
            return transacciones;
        }
        public bool CambiarPin(string PinActual, string NuevoPin)
        {
            if (this.pin == PinActual)
            {
                if (NuevoPin.Length >= 4 && NuevoPin.Length <= 8)
                {
                    this.Pin = NuevoPin;
                    GuardarDatos(); // Guardar inmediatamente después del cambio
                    return true;
                }
            }
            return false;
        }
    }
}

