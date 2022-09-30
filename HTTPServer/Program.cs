using System;
using System.Net;
using System.IO;
using System.Text;

HttpListener listener = new HttpListener();
// установка адресов прослушки
listener.Prefixes.Add("http://localhost:3437/google/");
listener.Start();
Console.WriteLine("Ожидание подключений...");
// метод GetContext блокирует текущий поток, ожидая получение запроса 
while (true)
{
    HttpListenerContext context = await listener.GetContextAsync();
    HttpListenerRequest request = context.Request;
    HttpListenerResponse response = context.Response;

    FileInfo file = new FileInfo(Environment.CurrentDirectory + @"\тест\index.html");
    FileStream fs = file.OpenRead();
    byte[] buffer = new byte[fs.Length];
    response.ContentLength64 = buffer.Length;
    Stream output = response.OutputStream;
    output.Write(buffer, 0, buffer.Length);
    output.Close();
    Console.WriteLine("Обработка подключений");
}
