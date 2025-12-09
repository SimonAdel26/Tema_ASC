
Console.WriteLine($"Ora curentă este afișată sub formă de ceas binar.");
Console.WriteLine();

int[] Ora = new int[5], Min = new int[6], Sec = new int[6];

void Binar(int nr, int ok)
{
    if (ok == 1)
    {
        int i = 4;
        while (nr != 0)
        {
            Ora[i] = (nr % 2);
            nr = nr / 2;
            i--;
        }
    }
    else if (ok == 2)
    {
        int i = 5;
        while (nr != 0)
        {
            Min[i] = (nr % 2);
            nr = nr / 2;
            i--;
        }
    }
    else
    {
        int i = 5;
        while (nr != 0)
        {
            Sec[i] = (nr % 2);
            nr = nr / 2;
            i--;
        }
    }
}

void Afisare(int[] vector, int ok)
{
    if (ok == 1)
    {
        for (int i = 0; i < 5; i++)
            if (vector[i] == 0) Console.Write($"O ");
            else Console.Write($"X ");
    }
    else
    {
        for (int i = 0; i < 6; i++)
            if (vector[i] == 0) Console.Write($"O ");
            else Console.Write($"X ");
    }
    Console.WriteLine();

}

int ok = 1;
while (ok == 1)
{
    DateTime Timp = DateTime.Now;

    int ora = Timp.Hour;
    int min = Timp.Minute;
    int sec = Timp.Second;

    Console.SetCursorPosition(0, 3);
    //Console.WriteLine($"Ora curenta : {ora} : {min} : {sec}");

    Binar(ora, 1);
    Console.Write($"H : ");
    Afisare(Ora, 1);

    Binar(min, 2);
    Console.Write($"M : ");
    Afisare(Min, 0);

    Binar(sec, 0);
    Console.Write($"S : ");
    Afisare(Sec, 0);

}

