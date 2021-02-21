using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static SharedProject.Types;

namespace ClientLibrary
{
    public class ClientLibrary
    {
        static NetworkStream stream;

        static void Main(string[] args)
        {
            Connect("127.0.0.1", 6060);

            //Temporaryu
            InputType inputType = InputType.Click;
            int x = 2;
            int y = 2;
            int length = 25;
            MouseButton mouseButton = MouseButton.Left;

            Console.WriteLine("Press any button to send stuff");
            while (true)
            {
                Console.ReadLine();
                //Sends the stuff to the server
                SendToServer(stream, inputType, x, y, length, mouseButton);
            }
        }

        static void Connect(string serverName, int port)
        {
            //Connects and establishes stream
            TcpClient client = new TcpClient(serverName, port);
            stream = client.GetStream();
        }

        static void SendToServer(NetworkStream stream, InputType inputType, int xVal, int yVal, int pressLength, MouseButton button)
        {
            stream.WriteByte((Byte)inputType);

            //Conversions

            Byte[] xBytes = BitConverter.GetBytes(xVal);
            Byte[] yBytes = BitConverter.GetBytes(yVal);
            Byte[] pressBytes = BitConverter.GetBytes(pressLength);

            switch (inputType)
            {
                case InputType.Click:
                    //Sends x
                    stream.Write(xBytes, 0, xBytes.Length);

                    //Sends y
                    stream.Write(yBytes, 0, yBytes.Length);

                    //Sends presslength
                    stream.Write(pressBytes, 0, pressBytes.Length);

                    //Sends button
                    stream.WriteByte((byte)button);

                    break;
                case InputType.Move:
                    //Sends x
                    stream.Write(xBytes, 0, xBytes.Length);

                    //Sends y
                    stream.Write(yBytes, 0, yBytes.Length);

                    break;
                case InputType.Press:
                    //Sends presslength
                    stream.Write(pressBytes, 0, pressBytes.Length);

                    //Sends button
                    stream.WriteByte((byte)button);

                    break;
                default:
                    break;
            }
        }
    }
}
