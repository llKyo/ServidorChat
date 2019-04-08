using ClienteChatBidirecional.Comunicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteChatBidirecional
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Ingrese Servidor:\n> ");
            string servidor = Console.ReadLine().Trim();
            int puerto;
            do
            {
                Console.Write("Ingrese Puerto:\n> ");
            } while (!Int32.TryParse(Console.ReadLine().Trim(), out puerto));




            Console.WriteLine("Conectando a Servidor {0} en puerto {1}", servidor, puerto);
            ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);
            if (clienteSocket.Conectar())
            {
                Console.WriteLine("Conectando...");

                string mensaje;

                try
                {
                    while (true)
                    {
                        Console.Write("Escriba un mensaje:\n> ");
                        mensaje = Console.ReadLine().Trim();
                        clienteSocket.Escribir(mensaje);
                        if (mensaje.ToLower() == "chao")
                        {
                            throw new Exception();
                        }
                        Console.WriteLine("Esperando a servidor...");
                        mensaje = clienteSocket.Leer();
                        Console.WriteLine("[Server] {0}", mensaje);
                        if (mensaje.ToLower() == "chao")
                        {
                            throw new Exception();
                        }
                    }
                } catch (Exception ex)
                {
                    clienteSocket.Desconectar();
                    Console.WriteLine("[Info] Servidor se a desconectado");
                }
                
            }
            else
            {
                Console.WriteLine("Error de comunicación");
            }
            Console.ReadKey();
        }
    }
}
