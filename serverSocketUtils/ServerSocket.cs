using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace serverSocketUtils
{
    //todas las clases siempre deben ser públicas
    public class ServerSocket
    {
        private int puerto;
        private Socket servidor;

        public ServerSocket(int puerto)
        {
            this.puerto = puerto;
        }

        //iniciar la conexión del servidor, tomando posesión del puerto
        //devolverá true o false según tome la conexión
        public bool iniciar()
        {
            try
            {
                servidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //tomar posesión del puerto
                //estamos enviando la dirección ip y el puerto
                //el bind se encarga de esto
                servidor.Bind(new IPEndPoint(IPAddress.Any, puerto));
                //denifir cola de espera
                servidor.Listen(10); //10 representa 10 cantidad de usuarios en espera
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public Socket ObtenerCliente()
        {
            return servidor.Accept();
        }

        public void Cerrar()
        {
            try
            {
                servidor.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
