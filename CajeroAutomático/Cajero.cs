using System;
using System.Collections.Generic;
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
        public Usuario(string Cuenta, string Pin, decimal Saldo)
        {
            this.Cuenta = cuenta;
            this.Pin = pin;
            this.Saldo = saldo;
        }
        // Propiedades para acceder a los atributos privados de Usuario
        // Usando miembros con cuerpo de expresión
        public string cuenta => cuenta;
        public string pin => pin;
        public decimal saldo => saldo;


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
        // Depositar
        public bool Deposito(decimal Cantidad)
        {
            if (Cantidad > 0)
            {
                decimal SaldoAnterior = this.Saldo;
                this.Saldo += Cantidad;
                GuardarDatos();
                return true;
            }
            return false;
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

