namespace Yahtzee;

class Program
{
    static void Main(string[] args)
    {
        string recommencer;             // touche saisie pour rejouer ou quitter
        string touche;                  // touche pour démarrer la partie
        int nbJoueurs;                  // nombre de joueurs (1 à 4)
        string[] nomsJoueurs;           // tableau contenant les noms des joueurs
        string[] nomCategories;         // tableau contenant les noms des 13 catégories
        int[] scoresJoueurs;            // tableau contenant les scores initiaux des joueurs
        int[] scoresTotaux;             // tableau contenant les scores totaux des joueurs
        int[][] scoresCategories;       // tableau contenant les scores par joueur et par catégorie
        int[] des;                      // valeurs des 5 dés
        bool[] desGardes;               // vrai si le dé est gardé
        int tour;                       // numéro du tour en cours (1 à 13)
        int iJoueur;                    // indice du joueur en cours
        int categorieChoisie;           // indice de la catégorie choisie (0-12)
        int bonus;                      // bonus de 35 points si sous-total >= 63
        bool bonusAjoute;               // vrai si un Yahtzee bonus a été ajouté ce tour
        bool yahtzeeDejaFait;           // vrai après le premier Yahtzee réussi
        int scoreTemp;                  // score temporaire pour les calculs
        int[] scoresChiffres;           // scores des 6 catégories chiffres pour le bonus
        int i;                          // compteur général

        nomCategories = new string[] { "Uns", "Deux", "Trois", "Quatre", "Cinq", "Six",
                                       "Brelan", "Carré", "Full House",
                                       "Petite Suite", "Grande Suite", "Chance", "Yahtzee" };

        Console.WriteLine("Bienvenue dans le jeu Yahtzee !");
        Console.WriteLine("But du jeu : remplir 13 catégories en lançant 5 dés jusqu'à 3 fois.");
        Console.WriteLine("Le joueur avec le plus de points à la fin gagne la partie.");
        Console.WriteLine("Appuyez sur Entrée pour commencer...");
        touche = Console.ReadLine();

        do
        {
            FonctionProgram.GestionJoueurs(out nbJoueurs, out nomsJoueurs, out scoresJoueurs);
            FonctionProgram.GestionTours(nbJoueurs, out scoresTotaux, out scoresCategories);

            yahtzeeDejaFait = false;
            tour = 1;
            while (tour <= 13)
            {
                iJoueur = 0;
                while (iJoueur < nbJoueurs)
                {
                    Console.WriteLine("--- Tour " + tour + "/13 — " + nomsJoueurs[iJoueur] + " ---");

                    FonctionProgram.LancerDes(out des, out desGardes);
                    FonctionProgram.AfficherFeuilleScore(nomsJoueurs[iJoueur], scoresCategories[iJoueur], scoresTotaux[iJoueur]);
                    FonctionProgram.GererYahtzeeBonus(yahtzeeDejaFait, des, ref scoresCategories[iJoueur], out bonusAjoute);

                    if (bonusAjoute)
                    {
                        scoresTotaux[iJoueur] = scoresTotaux[iJoueur] + 100;
                    }

                    FonctionProgram.SaisieCategorie(scoresCategories[iJoueur], des, nomCategories, out categorieChoisie);

                    FonctionProgram.CalculerScoreChiffre(des, categorieChoisie + 1, out scoreTemp);
                    if (categorieChoisie < 6)
                    {
                        scoresCategories[iJoueur][categorieChoisie] = scoreTemp;
                    }
                    else if (categorieChoisie == 6)
                    {
                        FonctionProgram.CalculerBrelanCarre(des, 3, out scoreTemp);
                        scoresCategories[iJoueur][categorieChoisie] = scoreTemp;
                    }
                    else if (categorieChoisie == 7)
                    {
                        FonctionProgram.CalculerBrelanCarre(des, 4, out scoreTemp);
                        scoresCategories[iJoueur][categorieChoisie] = scoreTemp;
                    }
                    else if (categorieChoisie == 8)
                    {
                        FonctionProgram.CalculerFullHouse(des, out scoreTemp);
                        scoresCategories[iJoueur][categorieChoisie] = scoreTemp;
                    }
                    else if (categorieChoisie == 9)
                    {
                        FonctionProgram.CalculerSuite(des, 4, out scoreTemp);
                        scoresCategories[iJoueur][categorieChoisie] = scoreTemp;
                    }
                    else if (categorieChoisie == 10)
                    {
                        FonctionProgram.CalculerSuite(des, 5, out scoreTemp);
                        scoresCategories[iJoueur][categorieChoisie] = scoreTemp;
                    }
                    else if (categorieChoisie == 11)
                    {
                        FonctionProgram.CalculerChance(des, out scoreTemp);
                        scoresCategories[iJoueur][categorieChoisie] = scoreTemp;
                    }
                    else if (categorieChoisie == 12)
                    {
                        FonctionProgram.CalculerYahtzee(des, yahtzeeDejaFait, out scoreTemp, out yahtzeeDejaFait);
                        scoresCategories[iJoueur][categorieChoisie] = scoreTemp;
                    }

                    scoresTotaux[iJoueur] = scoresTotaux[iJoueur] + scoresCategories[iJoueur][categorieChoisie];

                    iJoueur = iJoueur + 1;
                }
                tour = tour + 1;
            }

            i = 0;
            while (i < nbJoueurs)
            {
                scoresChiffres = new int[6];
                int k = 0;
                while (k < 6)
                {
                    scoresChiffres[k] = scoresCategories[i][k];
                    k = k + 1;
                }
                FonctionProgram.CalculerBonus(scoresChiffres, out bonus);
                scoresTotaux[i] = scoresTotaux[i] + bonus;
                if (bonus > 0)
                {
                    Console.WriteLine(nomsJoueurs[i] + " obtient le bonus de 35 points !");
                }
                i = i + 1;
            }

            i = 0;
            while (i < nbJoueurs)
            {
                FonctionProgram.AfficherFeuilleScore(nomsJoueurs[i], scoresCategories[i], scoresTotaux[i]);
                i = i + 1;
            }

            FonctionProgram.AfficherResultatsFinaux(nomsJoueurs, nbJoueurs, scoresTotaux);

            Console.WriteLine("Voulez-vous recommencer une nouvelle partie ? (Entrez un espace pour recommencer, ou n'importe quelle autre touche pour quitter)");
            recommencer = Console.ReadLine() ?? "";
        }
        while (recommencer == " ");
    }
}
