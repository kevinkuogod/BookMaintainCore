using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Net.WebSockets;
using bookMaintain.Common;
using bookMaintain.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Concurrent;
using System.Text;

namespace BookMaintainCore.Controllers
{
    public class WebSocketController : Controller //ControllerBase
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        private static ConcurrentDictionary<int, WebSocket> _webSockets = new ConcurrentDictionary<int, WebSocket>();

        [Route("/ws")]
        public async Task Get()
        {
            /*if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }*/

            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _webSockets.TryAdd(webSocket.GetHashCode(), webSocket);
                await HandleWebSocket(webSocket);
                _webSockets.TryRemove(webSocket.GetHashCode(), out _);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private async Task HandleWebSocket(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!receiveResult.CloseStatus.HasValue)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
                await BroadcastMessage(message);
                RedisTool.SetHashSet(0,System.Text.Encoding.Default.GetString(new ArraySegment<byte>(buffer, 0, receiveResult.Count)));
                receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(receiveResult.CloseStatus.Value, receiveResult.CloseStatusDescription, CancellationToken.None);
        }

        private async Task BroadcastMessage(string message)
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            var data = new ArraySegment<byte>(buffer);

            foreach (var webSocket in _webSockets.Values)
            {
                if (webSocket.State == WebSocketState.Open)
                    await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        //ConcurrentDictionary<int, WebSocket> WebSockets = new ConcurrentDictionary<int, WebSocket>();

        /*private async Task Echo(WebSocket webSocket)
        {
            try
            {
                WebSockets.TryAdd(webSocket.GetHashCode(), webSocket);
                Console.WriteLine("webSocket.GetHashCode():"+ webSocket.GetHashCode());
                var buffer = new byte[1024 * 4];
                var receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);
                while (!receiveResult.CloseStatus.HasValue)
                {
                    await webSocket.SendAsync(
                        new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                        receiveResult.MessageType,
                        receiveResult.EndOfMessage,
                        CancellationToken.None);
                    Console.WriteLine(System.Text.Encoding.Default.GetString(new ArraySegment<byte>(buffer, 0, receiveResult.Count)));
                    Broadcast(System.Text.Encoding.Default.GetString(new ArraySegment<byte>(buffer, 0, receiveResult.Count)));
                    //RedisTool.SetHashSet(0, System.Text.Encoding.Default.GetString(new ArraySegment<byte>(buffer, 0, receiveResult.Count)));
                    receiveResult = await webSocket.ReceiveAsync(
                        new ArraySegment<byte>(buffer), CancellationToken.None);
                }
                WebSockets.TryRemove(webSocket.GetHashCode(), out var removed);
                await webSocket.CloseAsync(
                    receiveResult.CloseStatus.Value,
                    receiveResult.CloseStatusDescription,
                    CancellationToken.None);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }*/

        /*public void Broadcast(string message)
        {
            //$"{DateTime.Now:MM-dd HH:mm:ss}\t
            var buff = Encoding.UTF8.GetBytes($"{message}");
            var data = new ArraySegment<byte>(buff, 0, buff.Length);
            //Console.WriteLine("WebSockets.Values:" + WebSockets.Values);
            Console.WriteLine("WebSockets.Values.Count:" + WebSockets.Values.Count);
            //Console.WriteLine("WebSockets.Count:" + WebSockets.Count);
            Parallel.ForEach(WebSockets.Values, async (webSocket) =>
            {
                if (webSocket.State == WebSocketState.Open)
                    await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
            });
        }*/
    }
}