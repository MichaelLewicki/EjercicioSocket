using serverSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSocket
{
    public class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando servidor en el puerto {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);
            if (servidor.iniciar())
            {
                //ok, puede conectar
                Console.WriteLine("Servidor iniciado");
                while (true) 
                {
                    Console.WriteLine("Esperando la conexión de algún cliente...");
                    Console.WriteLine("");
                    //crear instancia de la clase Socket para realizar la comunicación con algún cliente que se conecte al servidor
                    Socket socketCliente = servidor.ObtenerCliente();
                    Console.WriteLine("Un cliente se ha conectado");
                    Console.WriteLine("");
                    //crear instancia de la clase ClienteCom (que tiene los métodos de comunicación de un cliente) por medio de un socket
                    ClienteCom clienteCom = new ClienteCom(socketCliente);
                    generarComunicacion(clienteCom);
                }
            }
            else
            {
                Console.WriteLine("Error, el puerto {0} esta en uso...", puerto);
            }
        }

        public static void generarComunicacion(ClienteCom clienteCom) 
        {
            bool finalizarComunicacion = false;
            while (!finalizarComunicacion)
            {
                Console.WriteLine("Esperando mensaje del cliente...");
                /* (Protocolo de comunicación) -> Desde aquí se realizará el mecanismo para escribir y leer el mensaje de un cliente */
                //primero esperaremos la respuesta del cliente que se conectó al servidor
                string respuesta = clienteCom.Leer();
                //veriricar que la respuesta no sea null
                if (respuesta != null)
                {
                    Console.WriteLine("El cliente mandó a decir: {0}", respuesta);
                    //convertir la respuesta a minúscula para realizar validación
                    if (respuesta.ToLower() == "chao")
                    {
                        finalizarComunicacion = true;
                    }
                    else
                    {
                        Console.WriteLine("Ingrese una respuesta al cliente...");
                        respuesta = Console.ReadLine().Trim();
                        clienteCom.Escribir(respuesta);
                        if (respuesta.ToLower() == "chao")
                        {
                            finalizarComunicacion = true;
                        }
                    }
                }
                else
                {
                    finalizarComunicacion = true;
                }
                if (finalizarComunicacion == true)
                {
                    clienteCom.Desconectar();
                    Console.WriteLine("La comunicación con el cliente se ha finalizado");
                }
            }
        }
    }
}
