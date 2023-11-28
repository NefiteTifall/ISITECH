namespace Projet_LiasseVehiculeBuilder;

public class ConstruceurLiasseVehiculeHtml : ConstructeurLiasseVehicule
{
    public ConstruceurLiasseVehiculeHtml()
    {
        this.liasse = new LiasseHtml();
    }

    public override void construitBonDeCommande(string nomClient)
    {
        string document;
        document = "<HTML>Bon de commande Client : " + nomClient + "</HTML>";
        this.liasse.ajouteDocument(document);
    }
}