using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IPAddress local = IPAddress.Parse("127.0.0.1");

                TcpListener servidor = new TcpListener(local, 8001);

                Socket conexao;
                NetworkStream socketStream;
                BinaryWriter escrever;
                BinaryReader ler;

                servidor.Start();

                Console.WriteLine("Servidor - " + servidor.LocalEndpoint);
                bool enquanto = true;
                while (enquanto)
                {
                    Console.WriteLine("Escutando...");
                    conexao = servidor.AcceptSocket();

                    socketStream = new NetworkStream(conexao);
                    escrever = new BinaryWriter(socketStream);
                    ler = new BinaryReader(socketStream);

                    escrever.Write("Servidor - Conexão Efetuada");

                    String conteudo = "";

                    while (conteudo != "FIM" && conexao.Connected)
                    {
                        try
                        {
                            conteudo = ler.ReadString();
                            Console.WriteLine("Cliente Mensagem: " + conteudo);
                            escrever.Write("Servidor - 200 OK!");
                        }
                        catch (Exception)
                        {
                            break;
                        }

                    }
                    escrever.Write("Servidor - Desligando...");
                    escrever.Close();
                    ler.Close();
                    socketStream.Close();
                    conexao.Close();
                    Console.WriteLine("Conexão Finalizada!");
                }

                servidor.Stop();
                Console.WriteLine("Servidor Foi Finalizado");
            }
            catch (Exception error)
            {
                Console.WriteLine(error.ToString());
            }

            Console.ReadKey();

        }
    }
}
