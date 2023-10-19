using System;
using System.Net.Sockets;
using System.Text;


namespace Сomp_systems_Lab_1__client_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("127.0.0.1", 8888);
            NetworkStream stream = client.GetStream();

            Console.Write("Enter the coordinates of the point (X,Y): ");
            string coordinates = Console.ReadLine();

            // Отправляем координаты серверу.
            byte[] data = Encoding.ASCII.GetBytes(coordinates);
            stream.Write(data, 0, data.Length);

            // Получаем ответ от сервера.
            data = new byte[1024];
            int bytesRead = stream.Read(data, 0, data.Length);
            string response = Encoding.ASCII.GetString(data, 0, bytesRead);

            Console.WriteLine($"Response from the server: {response}");

            Console.ReadKey();

            // Закрываем соединение.
            client.Close();
        }
    }
}

