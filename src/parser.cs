using System;

namespace GenericParser
{
    public class ProtocolParser
    {
        //Variáveis da Máquina de estados
        private static bool deviceState = false;
        private static bool hasError = false;
        private static int sensitivityLevel = 0;
        private static ParseState parseState = ParseState.checkHeader;

        private enum ParseState
        {
            checkHeader,
            checkState,
            checkError,
            checkSensitivity,
            showInformations,
        }

        public bool DecoderByte(byte[] buffer)
        {
            switch(parseState)
            {
                case ParseState.checkHeader:
                {
                    //verifica header
                    if ((buffer[0] & 0xAA) == 0xAA)
                    {
                        parseState = ParseState.checkState;
                        break;
                    }

                    //Header errado retorna erro
                    else
                    {
                        Console.WriteLine("ERRO: Header is Wrong!");
                        return true;
                    }
                }
                
                case ParseState.checkState:
                {
                    /*No segundo byte do pacote:
                    compara primeiro bit para receber o status*/ 
                    deviceState = (buffer[2] & 0x01) == 0x01;
                    parseState = ParseState.checkError;
                    break;
                }

                case ParseState.checkError:
                {
                    /*No segundo byte do pacote:
                    compara o segundo bit para receber o status erro
                    */
                    hasError = (buffer[2] & (1<<1)) == 1<<1;
                    parseState = ParseState.checkSensitivity;
                    break;    
                }

                case ParseState.checkSensitivity:
                {
                    /*No segundo byte do pacote:
                    mascara apenas bit 2 e 3
                    desloca para direita e recebe o valor
                    */
                    byte senseMask = (1<<2) + (1<<3);
                    sensitivityLevel = (buffer[2] & senseMask) >> 2;

                    parseState = ParseState.showInformations; 
                    break;
                }

                case ParseState.showInformations:
                {
                    Console.WriteLine("-------------------- New Data -----------------------");
                    Console.WriteLine($"Header is right");
                    Console.WriteLine($"Status: {(deviceState ? "ON" : "OFF")}");
                    Console.WriteLine($"Error: {(hasError ? "true" : "false")}");
                    Console.WriteLine($"Sensitivity level: {sensitivityLevel}");
                    Console.WriteLine("-----------------------------------------------------");

                    parseState = ParseState.checkHeader;
                    return true;
                }
            
            }

            //não terminou ainda
            return false;
        }
    }
}