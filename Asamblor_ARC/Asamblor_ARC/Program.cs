//Metode 
int Binary(int numar1)
{
    int binar = 0, putere = 1;
    int Nrbiti = 0;

    while (numar1 != 0 && Nrbiti < 6)
    {
        binar = (numar1 % 2) * putere + binar;
        putere *= 10;
        Nrbiti++;
        numar1 /= 2;
    }

    return binar;

}



//Asamblor ARC

Console.WriteLine($"Se va afisa cod masina a limbajului asamblor din fisierul de intrare 'input.txt'.");
Console.WriteLine();

string line = File.ReadAllText("input.txt");

string[] parti=line.Split(Environment.NewLine+Environment.NewLine);

//aflarea indexarea
char[] inceput = parti[1].ToCharArray();
int Index = 0;
for (int i = 0; i < inceput.Length; i++)
{
    int cifra=inceput[i]-'0';
    if (cifra >= 0 && cifra <= 9)
        Index = Index * 10 + cifra;
}

//gasirea main-ului
string main = parti[2].ToString();
int k = 3;
if (main.IndexOf("main") == -1)
{
    main = parti[k].ToString();
    k++;
}

//codificarea limbajului

List<List<int>> lista=new List<List<int>>();
List<int> masina=new List<int>();

string cod=parti[k].ToString();
while(cod.IndexOf("jmpl")==-1 || cod.IndexOf("end")==-1)
{
    char[] op3=new char[7];
    if(cod.IndexOf("ld")!=-1)
    {
        masina.Add(11);
    }
    else if( cod.IndexOf("st") != -1)
    {
        masina.Add(11);
    }

    if (cod.IndexOf("%r") != -1)
    {
        int cifra = cod.IndexOf("%r") + 2;
        int numar1 = cod[cifra] - '0';
        int numar2 = cod[cifra + 1] - '0';

        if (numar1 > 0 && numar2 > 0)
        {
            numar1 = numar1 * 10 + numar2;
            masina.Add(Binary(numar1));
        }


    }

        k++;
    cod= parti[k].ToString();
}
