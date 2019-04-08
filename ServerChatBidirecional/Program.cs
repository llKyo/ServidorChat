using ServerChatBidirecional.Comunicacion;
using ServerChatBidirecional.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerChatBidirecional
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);

            Console.WriteLine("Iniciando Servidor en la ip 10.22.42.192 en puerto {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);

            if (servidor.Iniciar())
            {

                Console.WriteLine("Servidor iniciado");

                while (true)
                {
                    Console.WriteLine("Esperando Cliente...");
                    Socket socketCliente = servidor.ObtenerCliente();

                    ClienteCom cliente = new ClienteCom(socketCliente);
                    RegistrosDALArchivos registro = new RegistrosDALArchivos();
                    string mensaje;
                    List<string> mensajes = new List<string>();
                    try
                    {
                        while (true)
                        {
                            Console.WriteLine("Esperando mensaje...");
                            mensaje = cliente.Leer();
                            mensajes.Add("C;"+mensaje);
                            if(mensaje.ToLower() == "chao")
                            {
                                throw new Exception();
                            }
                            Console.WriteLine("[Cliente] {0}", mensaje);
                            Console.Write("Escriba un mensaje\n> ");
                            mensaje = Console.ReadLine().Trim();
                            cliente.Escribir(mensaje);
                            mensajes.Add("S;"+mensaje);
                            if (mensaje.ToLower() == "chao")
                            {
                                throw new Exception();
                            }
                        }
                    } catch (Exception ex)
                    {
                        cliente.Desconectar();
                        registro.GenerarRegistro(mensajes);
                        Console.WriteLine("[Info] Cliente se a desconectado");
                    } 


                    


                }

            }
            else
            {
                Console.WriteLine("Error, el puerto {0} está en uso", puerto);
            }
        }
    }
}
