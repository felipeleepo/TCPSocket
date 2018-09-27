using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient cliente = new TcpClient();
            cliente.Connect("127.0.0.1", 8001);
            NetworkStream saida;
            BinaryWriter escreve;
            BinaryReader ler;
            String x = "";

            try
            {

                do
                {
                    saida = cliente.GetStream();

                    escreve = new BinaryWriter(saida);
                    ler = new BinaryReader(saida);
                    String resposta = ler.ReadString();
                    Console.WriteLine(resposta);
                    Console.WriteLine("Envie para o Servidor:");
                    x = Console.ReadLine();
                    escreve.Write(x);

                } while (x != "FIM");

                Console.WriteLine("Encerrando Conexão. Clique para sair");
                cliente.Close();
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }


            Console.ReadKey();

        }
    }
}
