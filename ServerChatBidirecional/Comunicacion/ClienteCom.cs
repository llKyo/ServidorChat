using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerChatBidirecional.Comunicacion
{
    public class ClienteCom
    {
        private Socket cliente;
        private StreamReader reader;
        private StreamWriter writer;

        private static string archivo = "registro.txt";
        private static string ruta = Directory.GetCurrentDirectory() + "/" + archivo;
        public ClienteCom(Socket socket)
        {
            this.cliente = socket;
            Stream stream = new NetworkStream(this.cliente);
            this.reader = new StreamReader(stream);
            this.writer = new StreamWriter(stream);

        }

        public bool Escribir(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);
                this.writer.Flush();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public String Leer()
        {
            try
            {
                return this.reader.ReadLine().Trim();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Desconectar()
        {
            try
            {
                this.cliente.Close();
            }
            catch (Exception ex)
            {

            }

        }
            public void GenerarRegistro(List<string> mensajes)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(ruta, true))
                {
                    string texto = "";
                    for (int i = 0; i < mensajes.Count(); i++)
                    {
                        texto += mensajes[i] + "\n";
                    }
                    writer.WriteLine(texto);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al escribir en archivo " + ex.Message);
            }

        }
    }
}
