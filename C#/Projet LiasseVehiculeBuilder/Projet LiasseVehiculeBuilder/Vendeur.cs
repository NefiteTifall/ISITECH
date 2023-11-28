namespace Projet_LiasseVehiculeBuilder;

public class Vendeur
{
    private ConstructeurLiasseVehicule constructeur;
    public void Construit()
    {
        this.constructeur.construitBonDeCommande("Bon de commande client");
        this.constructeur.construitBonDeImatriculation("Demande d'immatriculation client");
    }

    public Vendeur()
    {
        string type = "pdf";
        if (type == "pdf")
        {
            this.constructeur = new ConstruceurLiasseVehiculePdf();
        }
        else
        {
            this.constructeur = new ConstruceurLiasseVehiculeHtml();
        }
        this.Construit();
    }
}