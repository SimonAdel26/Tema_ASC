
Console.WriteLine($"Fiecare produs din automat costa 20 de centi.");
Console.WriteLine($"In automat se poate introdu 5 , 10 si 25 centi.");
Console.WriteLine();

string line= Console.ReadLine();
int Total=int.Parse(line);

char Stare = 'A';
(char moneda,int produs ,int cinci, int zece) Sum=('T',0,0,0);

if(Total==5)
{
    Stare = 'B';
    Sum = ('N', 0, 0,0);
}
else if(Total==10)
{
    Stare = 'C';
    Sum = ('D', 0, 0,0);
}
else 
{
    Stare = 'A';
    Console.WriteLine($"Automata a eliberat produsul.");
    Console.WriteLine($"Rest : 5 centi .");
}

int ok = 0,Exceptie=0;
while(Stare!='A')
{
    ok = 1;
    Console.WriteLine($"Ati introdus total doar {Total} de centi.");
    line= Console.ReadLine();
    int BaniPartial=int.Parse(line);
    Total = Total + BaniPartial;

    if(BaniPartial==5)
    {
        if(Stare=='B')
        {
            Stare = 'C';
            Sum = ('N', 0, 0,0);
        }
        else
        if (Stare == 'C')
        {
            Stare = 'D';
            Sum = ('N', 0, 0, 0);
        }
        else
        if (Stare == 'D')
        {
            Stare = 'A';
            Sum = ('N', 1, 0, 0);
        }
    }
    else if(BaniPartial==10)
    {
        if(Stare=='B')
        {
            Stare = 'D';
            Sum = ('D', 0, 0,0);
        }
        else
        if (Stare == 'C')
        {
            Stare = 'A';
            Sum = ('D', 1, 0, 0);
        }
        else
        if (Stare == 'D')
        {
            Stare = 'A';
            Sum = ('D', 1, 1, 0);
        }
    }
    else
    {
        if(Stare=='B')
        {
            Stare = 'A';
            Sum = ('Q', 1, 0, 1);
        }
        else
        if (Stare == 'C')
        {
            Stare = 'A';
            Sum = ('Q', 1, 1, 1);
        }
        else
        if (Stare == 'D')
        {
            Stare = 'A';
            Exceptie = 1;
            Sum = ('Q', 1, 1, 1);
        }
    }
}

if(ok==1)
{
    Console.WriteLine();
    Console.WriteLine($"Automata a eliberat produsul.");
        if (Exceptie == 1) Console.WriteLine($"Rest : 20 centi");
    else
        if (Sum.zece == 1 && Sum.cinci == 1) Console.WriteLine($"Rest : 15 centi");
    else
        if (Sum.zece == 1) Console.WriteLine($"Rest : 10 centi");
    else 
        if(Sum.cinci ==1)Console.WriteLine($"Rest : 5 centi");
    else 
        Console.WriteLine($"Rest : 0 centi");
}


