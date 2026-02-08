using System;

namespace GenericParser
{
    class Program
    {
        //tempo de recepção de novos dados
        const long timeDataReceive = 3000; //tempo entre cada pacote de dados recebido para simulacao
        static long previousTimeDataReceive = 0;
        static long timeNow;

        //flags
        static bool hasNewData = false;

        static void Main(string[] arg)
        {
            ProtocolParser genericProtocolParser = new ProtocolParser();

            byte[] buffer = [0xAA, 0x15, 0x0D, 0xFF]; 

            Console.WriteLine("------ Inicializando decodificação ------");

            while(true)
            {
                timeNow = Environment.TickCount64;

                //simula recepção de dados a cada 3s
                if (timeNow - previousTimeDataReceive >= timeDataReceive)
                {
                    hasNewData = true;
                    previousTimeDataReceive = timeNow;
                }

                //trata os dados recebidos,
                if (hasNewData && genericProtocolParser.DecoderByte(buffer))
                {
                    hasNewData = false;
                }

                Thread.Sleep(1);
            }
        }
    }
}