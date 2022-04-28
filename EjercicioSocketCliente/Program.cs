using serverSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            //pedir numero de ip del servidor
            Console.WriteLine("Ingrese número de ip del equipo con el cual desea comunicarse");
            string ip = Console.ReadLine();
            ConfigurationManager.AppSettings.Set("puerto", ip);
            //pedir numero de puerto
            Console.WriteLine("Ingrese número del puerto del servidor al cual se desea conectar");
            string puerto = Console.ReadLine();
            ConfigurationManager.AppSettings.Set("servidor", puerto);
            //mostrar configuración ingresada por pantalla
            //ConfigurationManager.RefreshSection("appSettings");
            Console.WriteLine("Conectado a servidor {0} en puerto {1}", ip, puerto);
            ClienteSocket clienteSocket = new ClienteSocket(ip, Convert.ToInt32(puerto));
            //verificar que el cliente se pudo conectar al servidor
            if (clienteSocket.Conectar())
            {
                Console.WriteLine("Conectado...");
                Console.WriteLine("");
                generarComunicacionServer(clienteSocket);
            }
            else
            {
                Console.WriteLine("Error de comunicación con el servidor");
            }
            Console.ReadKey();
        }
        public static void generarComunicacionServer(ClienteSocket clienteSocket)
        {
            bool finalizarComunicacion = false;
            while (!finalizarComunicacion)
            {
                //enviar mensaje al servidor
                Console.WriteLine("Envía un mensaje al servidor...");
                string mensaje = Console.ReadLine().Trim();
                clienteSocket.Escribir(mensaje);
                if (mensaje.ToLower() == "chao")
                {
                    finalizarComunicacion = true;
                }
                else
                {
                    //leer mensaje o respuesta del servidor
                    mensaje = clienteSocket.Leer();
                    Console.WriteLine("El servidor te ha respondido: {0}", mensaje);
                    if (mensaje.ToLower() == "chao")
                    {
                        finalizarComunicacion = true;
                    }
                }
            }
            clienteSocket.Desconectar();
            Console.WriteLine("La comunicación con el servidor ha finalizado");
        }
    }
}
