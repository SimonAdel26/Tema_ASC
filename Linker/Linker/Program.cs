
//linker

//Pasul 1 - Citire , determinarea adresei de baza a fiecarui modul , crearea tabelei de simbol si adresa absoluta a 

using System.Linq;

string line =Console.ReadLine();
int NumarModul=int.Parse(line);

List<Modul> ListadeModule=new List<Modul>();
int[] ListadeBaze=new int[NumarModul];
List<string> Simbol = new List<string>();
List<int> AdrRelativa=new List<int>();
List<int> AdrAbsoluta=new List<int>();
int linii = 0;

for (int i=0;i<NumarModul;i++)
{
    if (i == 0) ListadeBaze[i] = 0;
    else
    {
        ListadeBaze[i] = ListadeBaze[i - 1] + ListadeModule[i - 1].Program[0];
    }

    Modul m=new Modul();

    line=Console.ReadLine();
    string[] p = line.Split(' ');
    m.Definitii.AddRange(p);


    line = Console.ReadLine();
    p = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    m.Utilizari.AddRange(p);

    //salvarea simbolului extern

    int NU = int.Parse(m.Utilizari[0]);
    if (NU > 0)
    {
        for (int k = 1; k < m.Utilizari.Count; k=k+2)
        {
            m.SimbolExtern.Add(m.Utilizari[k]);
            m.AdresaExterna.Add(int.Parse(m.Utilizari[k + 1]));
        }
    }


    line = Console.ReadLine();
    p = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    int[] v = new int[p.Length];

    for (int z = 0; z < p.Length; z++)
    {
        v[z] = int.Parse(p[z]);
    }

    m.Program.AddRange(v);

    //Parcurgerea numarul de definitii , determinarea tabelului de simbol
    int ND=int.Parse(m.Definitii[0]);

    if(ND>0)
    {
        for(int j=1;j<=ND*2;j=j+2)
        {
            if(j%2==1)
            {
                if (Simbol.Contains(m.Definitii[j]))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($" ! Eroare : Simbolul {m.Definitii[j]} este deja definit. Pentru acest simbol o sa fie utilizata adresa absoluta pentru simbolul respectiv prima definitie.");
                    Console.ResetColor();
                }
                else
                {
                    Simbol.Add(m.Definitii[j]);
                    if(int.Parse(m.Definitii[j + 1]) >= m.Program[0])
                    {
                       
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"! Eroare : Adresa simbolului {m.Definitii[j]} din definitie depaseste dimensiunea modulului.");
                        Console.ResetColor();


                        AdrRelativa.Add(0);
                        AdrAbsoluta.Add(ListadeBaze[i]+0);
                    }
                    else
                    {
                        AdrRelativa.Add(int.Parse(m.Definitii[j + 1]));
                        AdrAbsoluta.Add(ListadeBaze[i] + int.Parse(m.Definitii[j+1]));

                    }
                }
            }
            
        }
    }

    linii=linii+m.Program[0];

    ListadeModule.Add(m);
}

//Afisarea tabelei de simboluri finala

Console.WriteLine();
Console.WriteLine($"Symbol Table");

for(int i=0;i<Simbol.Count;i++)
    Console.WriteLine($"{Simbol[i]} = {AdrAbsoluta[i]}");

Console.WriteLine();

//Pasul 2 

Console.WriteLine($"Memory Map");

int linie = 0;

for (int j = 0; j < NumarModul; j++)
{
    int UT = -2;
    string[] Lant=new string[ListadeModule[j].Program[0]];
    

    if (int.Parse(ListadeModule[j].Utilizari[0]) == 0)
    {
        UT = 0;
    }
    else
    {
        UT = 1;
        int Pasi = ListadeModule[j].AdresaExterna.Count;

        for (int i = 0; i<Pasi; i++)
        {
            int poz = ListadeModule[j].AdresaExterna[i];
            string SimbolCurent= ListadeModule[j].SimbolExtern[i];
            List<int> Vizitat = new List<int>();

            while (poz >= 0 && poz < ListadeModule[j].Program[0] && poz != 777 && Vizitat.IndexOf(poz)==-1)
            {
                if (Lant[poz] != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"! Eroare: Adresa externa {poz} la modulul {j} apare de mai multe ori in lista de utilizare.");
                    Console.ResetColor();
                    Console.WriteLine();

                }

                Vizitat.Add(poz);
                Lant[poz] = SimbolCurent;

                if (poz + 1 >= ListadeModule[j].Program.Count)
                    break;

                int part = ListadeModule[j].Program[poz + 1];
                poz = (part / 10) % 1000;

            }

        }

    }

    for (int k = 1; k < ListadeModule[j].Program.Count; k++)
    {
        Console.Write($"{linie}: ");
        int nr = ListadeModule[j].Program[k];

        if (nr % 10 == 1)
        {
            if (Lant[k-1]!=null)
            {
                nr = nr / 10000;
                nr = nr * 1000;
                int poz = 0;

                for (int h = 0; h < ListadeModule[j].AdresaExterna.Count; h++)
                {
                    if (ListadeModule[j].AdresaExterna[h] == k-1)
                    {
                        poz = h;
                        break;
                    }
                }

                string sim = ListadeModule[j].SimbolExtern[poz];

                for (int h = 0; h < Simbol.Count; h++)
                {
                    if (Simbol[h] == sim)
                    {
                        nr = nr + AdrAbsoluta[h];
                    }
                }

                Console.Write($"{nr}   ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"! Eroare: Valoarea imediata apare intr-o lista de utilizare.");
                Console.ResetColor();
            }
            else
            {
                Console.Write($"{nr / 10}");
            }
        }
        else if (nr % 10 == 2)
        {
            if ((nr / 10) % 1000 < 199)
            {
                Console.Write($"{nr / 10}");
            }
            else
            {
                Console.Write($"{(nr / 10000) * 1000 + 199} ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"! Eroare : Adresa absoluta depaseste dimensiunea masinii");
                Console.ResetColor();
            }
        }
        else if (nr % 10 == 3)
        {
            Console.Write($"{(nr / 10) + ListadeBaze[j]}");
        }
        else if (nr % 10 == 4)
        {

            if ((nr / 10) % 1000 == 777)
            {
                if(UT==0)
                {
                    Console.Write($"{nr / 10}  ");
                }
                else if(UT==1)
                {
                    string simbol = Lant[k-1]; 

                    if (simbol != null)
                    {
                        int Pozitie = Simbol.IndexOf(simbol);
                        if (Pozitie != -1)
                        {
                            nr = (nr / 10000) * 1000 + AdrAbsoluta[Pozitie];
                            Console.Write($"{nr}  ");
                        }
                        else
                        {
                            nr = (nr / 10000) * 1000 + 0;
                            Console.Write($"{nr}  ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"! Eroare: Simbolul {simbol} nu este definit, este folosit ca fiind 0.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        
                        nr = nr / 10;
                        Console.Write($"{nr}  ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"! Eroare: Adresa externa nu apare in lista de utilizare, este tratata ca fiind imediata.");
                        Console.ResetColor();
                    }
                }
            }
            else
            {
                string simbol = Lant[k-1];

                if (simbol != null)
                {
                    int Pozitie = Simbol.IndexOf(simbol);

                    int Final;

                    if (Pozitie != -1)
                    {
                        Final = AdrAbsoluta[Pozitie];
                    }
                    else
                    {
                        Final= 0;
                    }


                    Console.Write($"{(nr/10000)*1000+Final}  ");
                    if(Pozitie==-1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"! Eroare: Simbolul {simbol} nu este definit, este folosit ca fiind 0.");
                        Console.ResetColor();
                    }


                }
                else
                {

                    nr = nr / 10;
                    Console.Write($"{nr}  ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"! Eroare: Adresa externa nu apare in lista de utilizare, este tratata ca fiind imediata.");
                    Console.ResetColor();
                }
            }
        }


        linie++;
        Console.WriteLine();

    } 
 }

Console.WriteLine();
foreach (string i in Simbol)
{
    int numarare = 0;
    for (int x = 0; x < ListadeModule.Count; x++)
    {
        if (!ListadeModule[x].SimbolExtern.Contains(i))
        {
            numarare++;
        }
    }

    if( numarare == NumarModul )
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Warning : Simbolul {i} este difinita , dar nu este utilizata.");
        Console.ResetColor();
    }
    
}



class Modul
{
     public List<string> Definitii=new List<string>();
     public List<string> Utilizari=new List<string>();
     public List<int> Program=new List<int>();
     public List<string> SimbolExtern = new List<string>();
     public List<int> AdresaExterna=new List<int>();
}