﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using static SharedProject.Types;

namespace ClickingProject
{
    class Server
    {
        static void Main(string[] args)
        {
            StartServer();
        }

        static void StartServer()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 6060);
            tcpListener.Start();
            Console.WriteLine("Listening on " + tcpListener.LocalEndpoint.ToString());
            TcpClient client = tcpListener.AcceptTcpClient();
            Console.WriteLine("Client connected");

            NetworkStream stream = client.GetStream();

            bool isConnected = true;
            while (isConnected)
            {
                while (!stream.DataAvailable)
                {
                    System.Threading.Thread.Sleep(10);
                }

                InputType inputType = (InputType)stream.ReadByte();

                //Variables for inputting to the handler
                int xVal = 0;
                int yVal = 0;
                int pressLength = 0;
                MouseButton button = MouseButton.Left;

                switch (inputType)
                {
                    case InputType.Click:
                        Console.WriteLine("Gets click from client");

                        //x value
                        Byte[] clickBytesX = new Byte[sizeof(int)];
                        stream.Read(clickBytesX, 0, sizeof(int));
                        xVal = BitConverter.ToInt32(clickBytesX);

                        //y value
                        Byte[] clickBytesY = new Byte[sizeof(int)];
                        stream.Read(clickBytesY, 0, sizeof(int));
                        yVal = BitConverter.ToInt32(clickBytesY);

                        //press length
                        Byte[] clickBytesLength = new Byte[sizeof(int)];
                        stream.Read(clickBytesLength, 0, sizeof(int));
                        pressLength = BitConverter.ToInt32(clickBytesLength);

                        //button 
                        button = (MouseButton)stream.ReadByte();

                        break;
                    case InputType.Move:
                        Console.WriteLine("Gets move from client");

                        //x value
                        Byte[] moveBytesX = new Byte[sizeof(int)];
                        stream.Read(moveBytesX, 0, sizeof(int));
                        xVal = BitConverter.ToInt32(moveBytesX);

                        //y value
                        Byte[] moveBytesY = new Byte[sizeof(int)];
                        stream.Read(moveBytesY, 0, sizeof(int));
                        yVal = BitConverter.ToInt32(moveBytesY);

                        break;
                    case InputType.Press:
                        Console.WriteLine("Gets press from client");

                        //press length
                        Byte[] pressBytesLength = new Byte[sizeof(int)];
                        stream.Read(pressBytesLength, 0, sizeof(int));
                        pressLength = BitConverter.ToInt32(pressBytesLength);

                        //button 
                        button = (MouseButton)stream.ReadByte();

                        break;
                    default:
                        break;
                }

                ClickSimulator.InputHandler(inputType, xVal, yVal, pressLength, button);
            }
        }
    }
}