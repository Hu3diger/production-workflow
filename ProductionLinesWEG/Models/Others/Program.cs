﻿using SocketIO.Emitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace ProductionLineServerWEG
{
    class Program
    {

        private static Program _program;
        private List<Processo> _listProcess = new List<Processo>();

        internal List<Processo> ListProcesso { get => _listProcess;}

        public static Program getProgram()
        {
            if (_program == null)
            {
                _program = new Program();
            }

            return _program;
        }

    }
}