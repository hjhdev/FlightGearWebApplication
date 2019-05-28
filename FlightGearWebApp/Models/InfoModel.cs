﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightGearWebApp.Models;
using System.IO;
using System.Text;

namespace FlightGearWebApp.Models
{
    public class InfoModel
    {
        private static InfoModel s_instace = null;
        public static InfoModel Instance
        {
            get
            {
                if (s_instace == null)
                {
                    s_instace = new InfoModel();
                }
                return s_instace;
            }
        }
        private StreamWriter streamWriter;
        public int Time { get; set; }
        public int Timeout { get; set; }
        public string FilePath { get; set; }

        public NetworkConnection NetworkConnection { get; private set; }

        private InfoModel()
        {
            NetworkConnection = new NetworkConnection();
        }

        public void StartNetWorkRead()
        {
            NetworkConnection.Connect();
            NetworkConnection.read();
        }

        public void CreateFile(string filePath)
        {
            this.streamWriter = new StreamWriter(filePath);
        }

        public void WriteToFile(string filePath)
        {
            string toWrite = this.NetworkConnection.Lon.ToString() + "," + this.NetworkConnection.Lat.ToString();
            this.streamWriter.WriteLineAsync(toWrite); // the writing needs to be done in another func.
        }

        public void CloseFile(string filePath)
        {
            this.streamWriter.Close();
        }
    }
}