using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChatBidirecional.DAL
{

    public class RegistrosDALArchivos
    {
        private static string archivo = "registro.txt";
        private static string ruta = Directory.GetCurrentDirectory() + "/" + archivo;
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
