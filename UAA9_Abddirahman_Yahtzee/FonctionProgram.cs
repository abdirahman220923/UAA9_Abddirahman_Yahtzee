using System;
using System.Collections.Generic;
using System.Text;

namespace Yahtzee;

public class FonctionProgram
{
    // ══════════════════════════════════════════════════════════
    // LIRE ENTIER
    // ══════════════════════════════════════════════════════════
    public static void LireEntier(string question, out int resultat)
    {
        do
        {
            Console.WriteLine(question);
        }
        while (!int.TryParse(Console.ReadLine(), out resultat));
    }

    // ══════════════════════════════════════════════════════════
    // LIRE CHAINE
    // ══════════════════════════════════════════════════════════
    public static void LireChaine(string question, out string resultat)
    {
        do
        {
            Console.WriteLine(question);
            resultat = Console.ReadLine() ?? "";
        }
        while (resultat == "");
    }

    // ══════════════════════════════════════════════════════════
    // 1. AFFICHER DES
    // ══════════════════════════════════════════════════════════
    public static void AfficherDes(int[] des, bool[] desGardes)
    {
        string chaine = "";       // chaîne construite avec les valeurs des dés
        string chaineGardes = ""; // chaîne construite avec les indices des dés gardés
        int i = 0;                // compteur pour parcourir les dés

        chaine = "";
        i = 0;
        while (i < 5)
        {
            chaine = chaine + "[" + des[i] + "] ";
            i = i + 1;
        }
        Console.WriteLine(chaine);

        chaineGardes = "";
        i = 0;
        while (i < 5)
        {
            if (desGardes[i] == true)
            {
                if (chaineGardes == "")
                {
                    chaineGardes = chaineGardes + (i + 1);
                }
                else
                {
                    chaineGardes = chaineGardes + ", " + (i + 1);
                }
            }
            i = i + 1;
        }
        if (chaineGardes != "")
        {
            Console.WriteLine("Dés gardés : " + chaineGardes);
        }
    }

    // ══════════════════════════════════════════════════════════
    // 2. AFFICHER FEUILLE DE SCORE
    // ══════════════════════════════════════════════════════════
    public static void AfficherFeuilleScore(string nomJoueur, int[] scores, int scoreTotal)
    {
        int sousTotal = 0; // somme des scores des catégories 1 à 6
        int i = 0;         // compteur pour parcourir les catégories

        string[] categorieNom = { "Uns", "Deux", "Trois", "Quatre", "Cinq", "Six",
                                   "Brelan", "Carré", "Full House",
                                   "Petite Suite", "Grande Suite", "Chance", "Yahtzee" };

        Console.WriteLine("JOUEUR : " + nomJoueur);

        i = 0;
        while (i < 6)
        {
            Console.WriteLine(categorieNom[i] + " : " + scores[i]);
            i = i + 1;
        }

        sousTotal = 0;
        i = 0;
        while (i < 6)
        {
            if (scores[i] > 0)
            {
                sousTotal = sousTotal + scores[i];
            }
            i = i + 1;
        }

        if (sousTotal >= 63)
        {
            Console.WriteLine("BONUS : 35");
        }
        else
        {
            Console.WriteLine("BONUS : 0");
        }

        i = 6;
        while (i < 13)
        {
            Console.WriteLine(categorieNom[i] + " : " + scores[i]);
            i = i + 1;
        }

        Console.WriteLine("SCORE TOTAL : " + scoreTotal);
    }

    // ══════════════════════════════════════════════════════════
    // 3. AFFICHER RÉSULTATS FINAUX
    // ══════════════════════════════════════════════════════════
    public static void AfficherResultatsFinaux(string[] nomsJoueurs, int nbJoueurs, int[] scoresTotaux)
    {
        int maxScore = 0; // score maximum parmi tous les joueurs
        int i = 0;        // compteur pour parcourir les joueurs

        i = 0;
        while (i < nbJoueurs)
        {
            Console.WriteLine(nomsJoueurs[i] + " : " + scoresTotaux[i] + " points");
            i = i + 1;
        }

        maxScore = 0;
        i = 0;
        while (i < nbJoueurs)
        {
            if (scoresTotaux[i] > maxScore)
            {
                maxScore = scoresTotaux[i];
            }
            i = i + 1;
        }

        i = 0;
        while (i < nbJoueurs)
        {
            if (scoresTotaux[i] == maxScore)
            {
                Console.WriteLine(nomsJoueurs[i] + "a gagné avec " + maxScore + " points");
            }
            i = i + 1;
        }
    }

    // ══════════════════════════════════════════════════════════
    // 4. CALCULER BONUS 35 POINTS
    // ══════════════════════════════════════════════════════════
    public static void CalculerBonus(int[] scoresChiffres, out int bonus)
    {
        int total = 0; // somme des scores des 6 catégories chiffres
        int i = 0;     // compteur pour parcourir les catégories
        bonus = 0;

        total = 0;
        i = 0;
        while (i < 6)
        {
            total = total + scoresChiffres[i];
            i = i + 1;
        }

        if (total >= 63)
        {
            bonus = 35;
        }
        else
        {
            bonus = 0;
        }
    }

    // ══════════════════════════════════════════════════════════
    // 5. CALCULER SCORE POUR BRELAN ET CARRÉ
    // ══════════════════════════════════════════════════════════
    public static void CalculerBrelanCarre(int[] des, int type, out int score)
    {
        int[] compteur = new int[7]; // occurrences de chaque valeur de dé (indices 1-6)
        int somme = 0;               // somme de tous les dés
        bool trouve = false;         // vrai si brelan ou carré trouvé
        int i = 0;                   // compteur pour parcourir les dés
        int j = 0;                   // compteur pour parcourir les valeurs 1-6
        score = 0;

        compteur = new int[7];
        j = 1;
        while (j <= 6)
        {
            compteur[j] = 0;
            j = j + 1;
        }

        i = 0;
        while (i < 5)
        {
            compteur[des[i]] = compteur[des[i]] + 1;
            i = i + 1;
        }

        trouve = false;
        j = 1;
        while (j <= 6 && trouve == false)
        {
            if (type == 3 && compteur[j] >= 3)
            {
                trouve = true;
            }
            else
            {
                if (type == 4 && compteur[j] >= 4)
                {
                    trouve = true;
                }
            }
            j = j + 1;
        }

        if (trouve == true)
        {
            somme = 0;
            i = 0;
            while (i < 5)
            {
                somme = somme + des[i];
                i = i + 1;
            }
            score = somme;
        }
        else
        {
            score = 0;
        }
    }

    // ══════════════════════════════════════════════════════════
    // 6. CALCULER SCORE POUR CHANCE
    // ══════════════════════════════════════════════════════════
    public static void CalculerChance(int[] des, out int score)
    {
        int somme = 0; // somme de tous les dés
        int i = 0;     // compteur pour parcourir les dés

        somme = 0;
        i = 0;
        while (i < 5)
        {
            somme = somme + des[i];
            i = i + 1;
        }
        score = somme;
    }

    // ══════════════════════════════════════════════════════════
    // 7. CALCULER SCORE POUR CHIFFRES SIMPLES
    // ══════════════════════════════════════════════════════════
    public static void CalculerScoreChiffre(int[] des, int chiffre, out int score)
    {
        int somme = 0; // somme des dés égaux au chiffre cherché
        int i = 0;     // compteur pour parcourir les dés

        somme = 0;
        i = 0;
        while (i < 5)
        {
            if (des[i] == chiffre)
            {
                somme = somme + des[i];
            }
            i = i + 1;
        }
        score = somme;
    }

    // ══════════════════════════════════════════════════════════
    // 8. CALCULER SCORE POUR FULL HOUSE
    // ══════════════════════════════════════════════════════════
    public static void CalculerFullHouse(int[] des, out int score)
    {
        int[] compteur = new int[7]; // occurrences de chaque valeur de dé (indices 1-6)
        bool a3 = false;             // vrai si une valeur apparaît exactement 3 fois
        bool a2 = false;             // vrai si une valeur apparaît exactement 2 fois
        int i = 0;                   // compteur pour parcourir les dés
        int j = 0;                   // compteur pour parcourir les valeurs 1-6
        score = 0;

        compteur = new int[7];
        j = 1;
        while (j <= 6)
        {
            compteur[j] = 0;
            j = j + 1;
        }

        i = 0;
        while (i < 5)
        {
            compteur[des[i]] = compteur[des[i]] + 1;
            i = i + 1;
        }

        a3 = false;
        a2 = false;
        j = 1;
        while (j <= 6)
        {
            if (compteur[j] == 3)
            {
                a3 = true;
            }
            else
            {
                if (compteur[j] == 2)
                {
                    a2 = true;
                }
            }
            j = j + 1;
        }

        if (a3 == true && a2 == true)
        {
            score = 25;
        }
        else
        {
            score = 0;
        }
    }

    // ══════════════════════════════════════════════════════════
    // 9. CALCULER SCORE POUR SUITES
    // ══════════════════════════════════════════════════════════
    public static void CalculerSuite(int[] des, int type, out int score)
    {
        bool[] present = new bool[7]; // vrai si la valeur est présente parmi les dés (indices 1-6)
        int consecutif = 0;           // nombre de valeurs consécutives en cours
        int maxConsecutif = 0;        // maximum de valeurs consécutives trouvé
        int i = 0;                    // compteur pour parcourir les dés
        int j = 0;                    // compteur pour parcourir les valeurs 1-6
        score = 0;

        present = new bool[7];
        j = 1;
        while (j <= 6)
        {
            present[j] = false;
            j = j + 1;
        }

        i = 0;
        while (i < 5)
        {
            present[des[i]] = true;
            i = i + 1;
        }

        maxConsecutif = 0;
        consecutif = 0;
        j = 1;
        while (j <= 6)
        {
            if (present[j] == true)
            {
                consecutif = consecutif + 1;
                if (consecutif > maxConsecutif)
                {
                    maxConsecutif = consecutif;
                }
            }
            else
            {
                consecutif = 0;
            }
            j = j + 1;
        }

        if (type == 5 && maxConsecutif >= 5)
        {
            score = 40;
        }
        else
        {
            if (type == 4 && maxConsecutif >= 4)
            {
                score = 30;
            }
            else
            {
                score = 0;
            }
        }
    }

    // ══════════════════════════════════════════════════════════
    // 10. CALCULER SCORE POUR YAHTZEE
    // ══════════════════════════════════════════════════════════
    public static void CalculerYahtzee(int[] des, bool premierYahtzee, out int score, out bool yahtzeeDejaFait)
    {
        int[] compteur = new int[7]; // occurrences de chaque valeur de dé (indices 1-6)
        bool yahtzee = false;        // vrai si les 5 dés sont identiques
        int i = 0;                   // compteur pour parcourir les dés
        int j = 0;                   // compteur pour parcourir les valeurs 1-6
        score = 0;
        yahtzeeDejaFait = premierYahtzee;

        compteur = new int[7];
        j = 1;
        while (j <= 6)
        {
            compteur[j] = 0;
            j = j + 1;
        }

        i = 0;
        while (i < 5)
        {
            compteur[des[i]] = compteur[des[i]] + 1;
            i = i + 1;
        }

        yahtzee = false;
        j = 1;
        while (j <= 6 && yahtzee == false)
        {
            if (compteur[j] == 5)
            {
                yahtzee = true;
            }
            j = j + 1;
        }

        if (yahtzee == true)
        {
            if (premierYahtzee == true)
            {
                score = 50;
                yahtzeeDejaFait = true;
            }
            else
            {
                score = 100;
                yahtzeeDejaFait = true;
            }
        }
        else
        {
            score = 0;
            yahtzeeDejaFait = premierYahtzee;
        }
    }

    // ══════════════════════════════════════════════════════════
    // 11. CHOISIR LES DÉS À GARDER
    // ══════════════════════════════════════════════════════════
    public static void ChoisirDesAGarder(ref bool[] desGardes, out int nbAGarder)
    {
        int indice = 0; // indice saisi par le joueur (1 à 5)
        int i = 0;      // compteur pour réinitialiser desGardes
        int j = 0;      // compteur pour saisir les dés à garder
        nbAGarder = 0;

        i = 0;
        while (i < 5)
        {
            desGardes[i] = false;
            i = i + 1;
        }

        LireEntier("Combien de dés voulez-vous garder ? (0 à 5) :", out nbAGarder);

        if (nbAGarder > 0)
        {
            j = 0;
            while (j < nbAGarder)
            {
                LireEntier("Entrez l'indice du dé à garder (1 à 5) :", out indice);

                while (indice < 1 || indice > 5)
                {
                    LireEntier("Entrez l'indice du dé à garder (1 à 5) :", out indice);
                }

                desGardes[indice - 1] = true;
                j = j + 1;
            }
        }
    }

    // ══════════════════════════════════════════════════════════
    // 12. GESTION DES JOUEURS
    // ══════════════════════════════════════════════════════════
    public static void GestionJoueurs(int nbJoueurs, out string[] nomsJoueurs, out int[] scoresJoueurs)
    {
        int i = 0; // compteur pour parcourir les joueurs

        nomsJoueurs = new string[nbJoueurs];
        scoresJoueurs = new int[nbJoueurs];

        i = 0;
        while (i < nbJoueurs)
        {
            Console.WriteLine("Nom du joueur " + (i + 1) + " :");
            nomsJoueurs[i] = Console.ReadLine() ?? "";
            scoresJoueurs[i] = 0;
            i = i + 1;
        }
    }

    // ══════════════════════════════════════════════════════════
    // 13. GESTION DES TOURS
    // ══════════════════════════════════════════════════════════
    public static void GestionTours(int nbJoueurs, out int[] scoresTotaux, out int[][] scoresJoueurs)
    {
        int i = 0; // compteur pour initialisation
        int j = 0; // compteur pour les catégories

        scoresJoueurs = new int[nbJoueurs][];
        scoresTotaux = new int[nbJoueurs];

        i = 0;
        while (i < nbJoueurs)
        {
            scoresJoueurs[i] = new int[13];
            j = 0;
            while (j < 13)
            {
                scoresJoueurs[i][j] = -1; // -1 = catégorie non encore remplie
                j = j + 1;
            }
            scoresTotaux[i] = 0;
            i = i + 1;
        }
    }

    // ══════════════════════════════════════════════════════════
    // 14. GESTION DES YAHTZEE BONUS
    // ══════════════════════════════════════════════════════════
    public static void GererYahtzeeBonus(bool yahtzeeDejaFait, int[] des, ref int[] scoresJoueur, out bool bonusAjoute)
    {
        int[] compteur = new int[7]; // occurrences de chaque valeur de dé (indices 1-6)
        bool yahtzee = false;        // vrai si les 5 dés sont identiques
        int i = 0;                   // compteur pour parcourir les dés
        int j = 0;                   // compteur pour parcourir les valeurs 1-6

        bonusAjoute = false;
        compteur = new int[7];

        i = 0;
        while (i < 5)
        {
            compteur[des[i]] = compteur[des[i]] + 1;
            i = i + 1;
        }

        yahtzee = false;
        j = 1;
        while (j <= 6 && yahtzee == false)
        {
            if (compteur[j] == 5)
            {
                yahtzee = true;
            }
            j = j + 1;
        }

        if (yahtzee == true && yahtzeeDejaFait == true)
        {
            scoresJoueur[12] = scoresJoueur[12] + 100;
            bonusAjoute = true;
            Console.WriteLine("Yahtzee ! +100 points bonus");
        }
    }

    // ══════════════════════════════════════════════════════════
    // 15. LANCER DES DÉS
    // ══════════════════════════════════════════════════════════
    public static void LancerDes(out int[] des, out bool[] desGardes)
    {
        int nbLancers = 0;          // nombre de lancers déjà effectués
        int nbAGarder = 0;          // nombre de dés que le joueur veut garder
        string chaine = "";         // chaîne construite pour afficher les dés
        string reponse = "";        // réponse du joueur pour continuer ou arrêter
        int i = 0;                  // compteur pour parcourir les dés
        int j = 0;                  // compteur pour saisir les indices des dés à garder
        int indice = 0;             // indice d'un dé à garder saisi par le joueur (1-5)
        bool continuer = true;      // vrai si le joueur veut encore lancer
        Random alea = new Random(); // générateur de nombres aléatoires

        des = new int[5];
        desGardes = new bool[5];
        alea = new Random();

        i = 0;
        while (i < 5)
        {
            des[i] = alea.Next(1, 7);
            desGardes[i] = false;
            i = i + 1;
        }

        nbLancers = 1;
        chaine = "";
        i = 0;
        while (i < 5)
        {
            chaine = chaine + "[" + des[i] + "] ";
            i = i + 1;
        }
        Console.WriteLine("Lancer " + nbLancers + "/3 : " + chaine);

        continuer = true;
        while (nbLancers < 3 && continuer == true)
        {
            Console.WriteLine("Voulez-vous relancer ? (o = oui, n = non) :");
            reponse = Console.ReadLine() ?? "";

            while (reponse != "o" && reponse != "n")
            {
                Console.WriteLine("Réponse invalide. Entrez o ou n :");
                reponse = Console.ReadLine() ?? "";
            }

            if (reponse == "n")
            {
                continuer = false;
            }
            else
            {
                i = 0;
                while (i < 5)
                {
                    desGardes[i] = false;
                    i = i + 1;
                }

                LireEntier("Combien de dés voulez-vous garder ? (0 à 5) :", out nbAGarder);

                while (nbAGarder < 0 || nbAGarder > 5)
                {
                    Console.WriteLine("Nombre invalide ! Entrez un nombre entre 0 et 5 :");
                    LireEntier("Combien de dés voulez-vous garder ? (0 à 5) :", out nbAGarder);
                }

                if (nbAGarder > 0)
                {
                    j = 0;
                    while (j < nbAGarder)
                    {
                        LireEntier("Entrez l'indice du dé à garder (1 à 5) :", out indice);

                        while (indice < 1 || indice > 5)
                        {
                            Console.WriteLine("Indice invalide ! Entrez un indice entre 1 et 5 :");
                            LireEntier("Entrez l'indice du dé à garder (1 à 5) :", out indice);
                        }

                        desGardes[indice - 1] = true;
                        j = j + 1;
                    }
                }

                i = 0;
                while (i < 5)
                {
                    if (desGardes[i] == false)
                    {
                        des[i] = alea.Next(1, 7);
                    }
                    i = i + 1;
                }

                nbLancers = nbLancers + 1;
                chaine = "";
                i = 0;
                while (i < 5)
                {
                    chaine = chaine + "[" + des[i] + "] ";
                    i = i + 1;
                }
                Console.WriteLine("Lancer " + nbLancers + "/3 : " + chaine);
            }
        }
    }


    // ══════════════════════════════════════════════════════════
    // 16. SAISIE DES CATÉGORIES
    // ══════════════════════════════════════════════════════════
    public static void SaisieCategorie(int[] scoresJoueur, int[] des, string[] nomCategorie, out int categorieChoisie)
    {
        int choix = 0;       // numéro saisi par le joueur (1 à 13)
        int i = 0;           // compteur pour parcourir les catégories
        int scoreTemp = 0;   // score calculé pour chaque catégorie avec les dés actuels
        bool yahtzeeTemp = false; // valeur temporaire pour CalculerYahtzee
        categorieChoisie = 0;

        Console.WriteLine("Catégories disponibles :");

        i = 0;
        while (i < 13)
        {
            if (scoresJoueur[i] == -1)
            {
                if (i < 6)
                {
                    CalculerScoreChiffre(des, i + 1, out scoreTemp);
                }
                else if (i == 6)
                {
                    CalculerBrelanCarre(des, 3, out scoreTemp);
                }
                else if (i == 7)
                {
                    CalculerBrelanCarre(des, 4, out scoreTemp);
                }
                else if (i == 8)
                {
                    CalculerFullHouse(des, out scoreTemp);
                }
                else if (i == 9)
                {
                    CalculerSuite(des, 4, out scoreTemp);
                }
                else if (i == 10)
                {
                    CalculerSuite(des, 5, out scoreTemp);
                }
                else if (i == 11)
                {
                    CalculerChance(des, out scoreTemp);
                }
                else if (i == 12)
                {
                    CalculerYahtzee(des, false, out scoreTemp, out yahtzeeTemp);
                }
                Console.WriteLine((i + 1) + ". " + nomCategorie[i] + " : " + scoreTemp + " points");
            }
            i = i + 1;
        }

        LireEntier("Choisissez le numéro d'une catégorie :", out choix);
        categorieChoisie = choix - 1;

        while (choix < 1 || choix > 13 || scoresJoueur[categorieChoisie] != -1)
        {
            Console.WriteLine("Choix invalide ou catégorie déjà utilisée !");
            LireEntier("Choisissez le numéro d'une catégorie :", out choix);
            categorieChoisie = choix - 1;
        }
    }
}
