using AsciiArt;

static void DevinerMot(string mot)
{
    List<char> lettreDevinees = new List<char>();
    List<char> lettresExclues = new List<char>();

    const int NB_VIES = 6;
    int viesRestantes = NB_VIES;


    while (viesRestantes > 0)
    {
        Console.WriteLine(Ascii.PENDU[NB_VIES-viesRestantes]);
        Console.WriteLine();

        AfficherMot(mot, lettreDevinees);
        Console.WriteLine();
        var lettre = DemanderUneLettre();

        Console.Clear();

        if (mot.Contains(lettre))
        {
            Console.WriteLine("Cette lettre est dans le mot");
            lettreDevinees.Add(lettre);
            if (ToutesLettresDevinees(mot, lettreDevinees))
            {
                break;
            }
        }
        else
        {
            if (!lettresExclues.Contains(lettre))
            {
                viesRestantes--;
                lettresExclues.Add(lettre);
            }
            
            Console.WriteLine("Vies restantes: " + viesRestantes);
        }
        if(lettresExclues.Count > 0)
        {
            Console.WriteLine("Le mot ne contient pas les lettres: " + String.Join(", ", lettresExclues));
        }
        Console.WriteLine();
    }

    Console.WriteLine(Ascii.PENDU[NB_VIES - viesRestantes]);

    if (viesRestantes == 0)
    {
        Console.WriteLine("Perdu, le mot était " + mot);
    }
    else
    {
        AfficherMot(mot, lettreDevinees);
        Console.WriteLine();
        Console.WriteLine("Gagné");
    }
}


static bool ToutesLettresDevinees(string mot, List<char> lettres)
{
    foreach(var lettre in lettres)
    {
        mot = mot.Replace(lettre.ToString(), "");
    }

    if(mot.Length == 0)
    {
        return true;
    }
    return false;
  
}




static void AfficherMot(string mot, List<char> lettres)
{

    for (int i = 0; i < mot.Length; i++)
    {
        char lettre = mot[i];

        if (lettres.Contains(lettre))
        {
            Console.Write(lettre + " ");
        }
        else
        {
            Console.Write("_ ");
        }
    }
    Console.WriteLine();
}


static char DemanderUneLettre(string message= "Veuillez entrer une lettre ")
{
    
    while (true)
    {
        Console.Write(message);

        string reponse = Console.ReadLine();

        if (reponse.Length == 1)
        {
            reponse = reponse.ToUpper();
            return reponse[0];
        }
        Console.WriteLine("ERREUR: vous devez entrer une lettre");
    }

   

}



static string[] ChargerLesMots(string nomFichier)
{
    
    try
    {
        return File.ReadAllLines(nomFichier);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur de lecture du fichier: " + nomFichier + " (" + ex.Message + ")");
    }

    return null;
}

static bool DemanderDeRejouer()
{
    char reponse = DemanderUneLettre("Volez-vous rejouer? (o/n) ");
    if(reponse=='o' || reponse == 'O')
    {
        return true;
    }
    else if(reponse=='n' || reponse == 'N')
    {
        return false;
    }
    else
    {
        Console.WriteLine("Erreur vous devez répondre o ou n");
        return DemanderDeRejouer();
    }
}



var mots = ChargerLesMots("mots.txt");


if(mots == null || mots.Length == 0)
{
    Console.WriteLine("La liste de mots est vide");
}
else
{
    while (true)
    {
        Random random = new Random();
        int i = random.Next(mots.Length);
        string mot = mots[i].Trim().ToUpper();
        DevinerMot(mot);
        if (!DemanderDeRejouer())
        {
            break;
        }
        Console.Clear();
    }
    Console.WriteLine("Merci et à bientôt");
}


