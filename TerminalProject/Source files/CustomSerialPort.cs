﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Threading;
using System.IO.Ports;

namespace TerminalProject.Source_files
{
    public class CustomSerialPort : SerialPort
    {
        public static char sentChecksum;
        // Our Format
        public const string customFormat = "$[{0}]{1}|{2}|{3}";
        public const int PACKET_SIZE = 128;
        public const int SEND_DELAY = 30;
        public const int CONFIGURE_DELAY = 30;
        // Message types
        public static class Type {
            // Messages
            public const string TEXT = "Tx";
            // Baudrate
            public const string BAUDRATE = "Br";
            // Communication Status
            public const string STATUS = "St";
            // Files 
            public const string FILE_START = "Wf";
            public const string FILE_NAME  = "Na";
            public const string FILE_SIZE  = "Sz";
            public const string FILE_DATA  = "Wd";
            public const string FILE_END   = "Fe";
        }
        // STATUS TYPE
        public static class STATUS
        {
            private static readonly string[] STATUS_NAMES = {"OK", "CHECKSUM_ERROR", "RECIEVING", "BUFFER_ERROR", "PORT_ERROR" };
            public static string ToString(int status) { return STATUS_NAMES[status]; }
            public const int OK = 0; 
            public const int CHECKSUM_ERROR = 1;
            public const int RECIEVING = 2;
            public const int BUFFER_ERROR = 3;
            public const int PORT_ERROR = 4;
        }

        private const int defaultBaudrate = 9600;
        public string dafaultPort;

        public string lastMessage = "";

        private string myBuffer = "";
        private int dataLen = 0;
        private int pollCnt = 0;

        /*
         * Constructor to set Default values
         */ 
        public CustomSerialPort()
        {
            string[] ports = CustomSerialPort.GetPortNames().Length > 0 ? 
                SerialPort.GetPortNames() : new string[] { "No Available Ports" };
            this.PortName = ports[0];
            this.BaudRate = defaultBaudrate;
            this.ReadTimeout = 300;
            this.WriteTimeout = 300;
        }

        /*
         * Sends a messge to MCU 
         * in the format of: $[--]#____
         * -- | opc
         * #  | checksum
         * __ | val
         */ 
        public void sendMessage(string opc, string val)
        {
            this.DiscardInBuffer();
            string num = val.Length.ToString("D3");
            char checksum = '1';
            String message = String.Format(customFormat, opc, checksum, num ,val);
            // Calc Checksum
            checksum = setCheckSum(message);
            message = String.Format(customFormat, opc, checksum, num, val);

            // Send
            this.Write(message);
          
        } // END sendMessage

        /*
         * Read message from MCU
         */ 
        public (string ,string, int checksumStatus) readMessage()
        {
            // Read content of buffer
            String inData = this.ReadExisting();
            inData = inData.TrimStart('\0');
            myBuffer += inData;
            // search for | |
            int cnt = inData.Count(f => f == '|');
            pollCnt += cnt;
            if (pollCnt >= 2)
            {
                try {
                    dataLen = int.Parse(myBuffer.Substring(7, 3));
                } catch (Exception)
                {
                    // get ready for next transaction
                    myBuffer = "";
                    dataLen = pollCnt = 0;
                    return (Type.STATUS, STATUS.BUFFER_ERROR.ToString(), -1);
                }
                // Check if data len ok
                if (dataLen == myBuffer.Length - 11)
                {
                    lastMessage = myBuffer;

                }
                // Buffer Length Error
                else if(dataLen < myBuffer.Length - 11)
                {
                    // get ready for next transaction
                    myBuffer = "";
                    dataLen = pollCnt = 0;
                    return (Type.STATUS, STATUS.BUFFER_ERROR.ToString(), -1);
                }
                // Still Receiving 
                else { return (Type.STATUS, STATUS.RECIEVING.ToString(), -1); }

            } // Error receiving data
            else if (myBuffer.Length > 11)
            {
                // get ready for next transaction
                myBuffer = "";
                dataLen = pollCnt = 0;
                return (Type.STATUS, STATUS.BUFFER_ERROR.ToString(), -1);
            }
            else { return (Type.STATUS, STATUS.RECIEVING.ToString(), -1); }


            // Format of Data: $[--]#|___|__ while # is checksum, -- is 2 chars opc
            string opc = myBuffer.Substring(2, 2);
            char recievedCheckSum = myBuffer[5];
            int checksumStatus = validateChecksum(myBuffer, recievedCheckSum) ? STATUS.OK : STATUS.CHECKSUM_ERROR;
            string val = "0";
            if (checksumStatus == STATUS.OK)
                val = myBuffer.Substring(11);

            // get ready for next transaction
            myBuffer = "";
            dataLen = pollCnt = 0;

            return (opc, val, checksumStatus);

        } // END readMessage

        /*
         * Sends a file to MCU
         */ 
        public void sendFile(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            // File descriptors
            sendMessage(Type.FILE_START, "");
            Thread.Sleep(SEND_DELAY);
            sendMessage(Type.FILE_NAME, file.Name);
            Thread.Sleep(SEND_DELAY);
            sendMessage(Type.FILE_SIZE, file.Length.ToString());
            Thread.Sleep(SEND_DELAY);

            string text = File.ReadAllText(filePath);
            int packetNum = text.Length / PACKET_SIZE;
            int leftovers = text.Length % PACKET_SIZE;
            packetNum += leftovers > 0 ? 1 : 0;

            // Send Packets of File Data
            for(int i = 0 ; i < packetNum ; i ++)
            {
                // if last packet
                if(i == packetNum - 1)
                {
                    sendMessage(Type.FILE_DATA, text.Substring(i * PACKET_SIZE, leftovers));
                    return;
                }
                sendMessage(Type.FILE_DATA, text.Substring(i * PACKET_SIZE, PACKET_SIZE ));
                Thread.Sleep(SEND_DELAY);
            }




        } // END sendFile

        /*
         * Checksum generator
         */ 
        private char setCheckSum(string message)
        {
            char checksum = '\0';

            for (int i = 0 ; i < message.Length ; i++ ){
                checksum ^= message[i];
            }

            checksum ^= '1';

            if (checksum == 0)
                return (char)1;
            return (char)checksum;
        }

        /*
         * Return weather checksum valid
         */ 
        private bool validateChecksum(string data, char recievedChecksum)
        {
           
            char checksum = '\0';
            for(int i = 0 ; i < data.Length ; i++)
                checksum ^= data[i];

            if (checksum == 1 && recievedChecksum == 1)
                return true;
            

            return checksum == 0;
        }

       /*
        * Clears MyBuffer
        */ 
        public void clearMyBuffer()
        {
            this.myBuffer = "";
        }


    }// END SerialPort

   
}
