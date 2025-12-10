
//HexViewer

using System.Text;
using static System.Net.Mime.MediaTypeNames;

byte[] hex=File.ReadAllBytes("input.txt");

int linie = 0;
for(int i=0;i<hex.Length;i+=16)
{
    byte[] octet=new byte[16];

    int k = 0,Panaunde=0;

    if (i + 16 < hex.Length) Panaunde = i + 16;
    else Panaunde=hex.Length;

    for (int j = i; j < Panaunde; j++)
         octet[k++] = hex[j];

    char[] ASCII = new char[16];

    for (int j = 0; j < 16; j++)
        if (octet[j] > 31 && octet[j] <128)
            ASCII[j] = ((char)octet[j]);
        else
            ASCII[j] = '.';

    Console.Write($"{linie:X8}: ");

    for(int j = 0;j < 16; j++)
        Console.Write($"{octet[j]:X2} ");

    Console.Write($"| ");

    for(int j = 0;j<16;j++)
        Console.Write($"{ASCII[j]}");

    Console.WriteLine();

    linie++;

}