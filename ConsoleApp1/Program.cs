// See https://aka.ms/new-console-template for more information
using Netina.Stomp.Client.Interfaces;
using Netina.Stomp.Client;

Console.WriteLine("Hello, World!");
IStompClient client =new StompClient("ws://localhost:8080/metatrader");
var headers = new Dictionary<string, string>();
headers.Add("X-Authorization", "Bearer xxx");
await client.ConnectAsync(headers);