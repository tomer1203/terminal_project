using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO.Ports;

namespace TerminalProject.Source_files
{
    public class CustomSerialPort : SerialPort
    {
        // Status
        public const int STATUS_OK = 0, STATUS_CHECKSUM_ERROR = 1, STATUS_RECIEVING = 2;
        public static char sentChecksum;
        // Our Format
        public const string customFormat = "$[{0}]{1}|{2}|{3}\0";
        // Message types
        public const string TEXT = "Tx";
        public const string BAUDRATE = "Br";
        public const string STATUS = "St"; 

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
            //this.Write("\0".getBytes() ,1,1);
        }

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
            if (pollCnt == 2)
            {
                dataLen = int.Parse(myBuffer.Substring(7, 3));
                if (dataLen == myBuffer.Length - 11)
                {
                    lastMessage = myBuffer;
                    // get ready for next transaction
                    myBuffer = "";
                    dataLen = pollCnt = 0;
                 
                }
                else { return (STATUS, STATUS_RECIEVING.ToString(), -1); }
            }
            else { return (STATUS, STATUS_RECIEVING.ToString(), -1); }


            // Format of Data: $[--]#|___|__ while # is checksum, -- is 2 chars opc
            string opc = myBuffer.Substring(2, 2);
            char recievedCheckSum = myBuffer[5];
            int checksumStatus = validateChecksum(inData) ? STATUS_OK : STATUS_CHECKSUM_ERROR;
            string val = "0";
            if (checksumStatus == STATUS_OK)
                val = myBuffer.Substring(11);

            return (opc, val, checksumStatus);

        }

        /*
         * Checksum generator
         */ 
        private char setCheckSum(string message)
        {
            char checksum = '\0';

            for (int i = 0 ; i < message.Length ; i++ ){
                checksum ^= message[i];
            }

            return (char)(checksum ^ '1');
        }

        /*
         * Return weather checksum valid
         */ 
        private bool validateChecksum(string data)
        {
           
            char tmp = '\0';
            for(int i = 0 ; i < data.Length ; i++)
                tmp ^= data[i];
            

            return tmp == 0;
        }

       


    }

   
}
