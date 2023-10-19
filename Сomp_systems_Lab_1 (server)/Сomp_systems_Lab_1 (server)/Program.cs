using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Сomp_systems_Lab_1__server_
{
    internal class Program
    {
        static void Main(string[] args)
        {

            TcpListener server = new TcpListener(IPAddress.Any, 8888);
            server.Start();
            Console.WriteLine("The server is running. Waiting for connections...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("The client is connected.");

                // Получаем сетевой поток для чтения и записи данных.
                NetworkStream stream = client.GetStream();

                // Читаем данные от клиента.
                byte[] data = new byte[1024];
                int bytesRead = stream.Read(data, 0, data.Length);
                string coordinates = Encoding.ASCII.GetString(data, 0, bytesRead);

                // Разбираем координаты.
                string[] parts = coordinates.Split(',');
                double x = double.Parse(parts[0]);
                double y = double.Parse(parts[1]);

                // Определяем в какой четверти находится точка.
                int quadrant;
                if (x > 0 && y > 0)
                    quadrant = 1;
                else if (x < 0 && y > 0)
                    quadrant = 2;
                else if (x < 0 && y < 0)
                    quadrant = 3;
                else if (x > 0 && y < 0)
                    quadrant = 4;
                else
                    quadrant = 0; // Если точка на осях.

                // Отправляем результат обратно клиенту.
                string result = $"The point ({x}, {y}) is in the {quadrant} quarter.";
                byte[] response = Encoding.ASCII.GetBytes(result);
                stream.Write(response, 0, response.Length);

                // Закрываем соединение.
                client.Close();
                Console.WriteLine("Connection closed.");
            }
        }
    }
}
