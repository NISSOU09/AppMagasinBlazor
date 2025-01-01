using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ArtOfEngineerUI.Components.Datafiles
{
    public class Access
    {
        string ConnectionStr = @"Data Source=DESKTOP-DST2S57\SQLEXPRESS;Initial Catalog = Magasin; Integrated Security = True";
        public (string[] Designations, double[] Ventes) GetProduitsAvecVentes()
        {
            var designations = new List<string>();
            var ventes = new List<double>();

            try
            {
                using (var connection = new SqlConnection(ConnectionStr))
                {
                    var query = @"
                SELECT P.Designation, S.Vente
                FROM Produit P
                JOIN Stat S ON P.Produit_id = S.Produit_id";

                    var command = new SqlCommand(query, connection);
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            designations.Add(reader["Designation"].ToString());
                            ventes.Add(Convert.ToDouble(reader["Vente"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and handle it (e.g., return an empty result or rethrow)
                Console.WriteLine($"Error fetching data: {ex.Message}");
            }

            return (designations.ToArray(), ventes.ToArray());
        }

        public List<Produit> GetAllProduits()
        {
            List<Produit> ListProduit = new List<Produit>();
            using (SqlConnection con = new SqlConnection(ConnectionStr))
            {
                SqlCommand cmd = new SqlCommand("spGetProduits", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Produit produit = new Produit();
                    produit.ProduitId = Convert.ToInt32(reader["Produit_id"]);
                    produit.Designation = reader["Designation"].ToString();
                    produit.Quantity = Convert.ToInt32(reader["Quantity"]);
                    produit.Prix = Convert.ToDecimal(reader["Prix"]);
                    ListProduit.Add(produit);
                }
                con.Close();
                return ListProduit;
            }
        }
        public List<Magasigner> GetAllMagasiniers()
        {
            List<Magasigner> ListMagasigner = new List<Magasigner>();
            using (SqlConnection con = new SqlConnection(ConnectionStr))
            {
                SqlCommand cmd = new SqlCommand("spGetMagasinier", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Magasigner magasinier = new();
                    magasinier.MagasignerId = Convert.ToInt32(reader["Magasigner_id"]);
                    magasinier.Nom = reader["Nom"].ToString();
                    magasinier.Prenom = reader["Prenom"].ToString();
                    magasinier.NumTel = reader["Num_tel"].ToString();
                    magasinier.Horaire = reader["Horaire"].ToString();
                    magasinier.Poste = reader["Poste"].ToString();
                    magasinier.Salaire = Convert.ToDecimal(reader["Salaire"]);
                    ListMagasigner.Add(magasinier);
                }
                con.Close();
                return ListMagasigner;
            }
        }
        public List<Client> GetAllClients()
        {
            List<Client> ListClient = new List<Client>();
            using (SqlConnection con = new SqlConnection(ConnectionStr))
            {
                SqlCommand cmd = new SqlCommand("spGetClient", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Client magasinier = new();
                    magasinier.ClientId = Convert.ToInt32(reader["Client_id"]);
                    magasinier.Nom = reader["Nom"].ToString();
                    magasinier.Prenom = reader["Prenom"].ToString();
                    magasinier.NumTel = reader["Num_tel"].ToString();
                    ListClient.Add(magasinier);
                }
                con.Close();
                return ListClient;
            }
        }
        public List<Stat> GetAllStats()
        {
            List<Stat> ListStat = new List<Stat>();
            using (SqlConnection con = new SqlConnection(ConnectionStr))
            {
                SqlCommand cmd = new SqlCommand("spGetStats", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Stat stat = new();
                    stat.StatId = Convert.ToInt32(reader["Stat_id"]);
                    stat.ProduitId = Convert.ToInt32(reader["Produit_id"]);
                    stat.Vente = Convert.ToDouble(reader["Vente"]);
                    ListStat.Add(stat);
                }
                con.Close();
                return ListStat;
            }
        }
        public List<Commande> GetAllCommandes()
        {
            List<Commande> ListCommande = new List<Commande>();
            using (SqlConnection con = new SqlConnection(ConnectionStr))
            {
                SqlCommand cmd = new SqlCommand("spGetCommande", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Commande commande = new();
                    commande.CommandeId = Convert.ToInt32(reader["Commande_id"]);
                    commande.ProduitId = Convert.ToInt32(reader["Produit_id"]);
                    commande.ClientId = Convert.ToInt32(reader["Client_id"]);
                    commande.Quantite = Convert.ToInt32(reader["Quantite"]);
                    // Convert the database value to DateTime first
                    DateTime dateTimeValue = Convert.ToDateTime(reader["Date_commande"]);
                    // Then extract the date part to assign to DateOnly
                    DateOnly dateOnly = new DateOnly(dateTimeValue.Year, dateTimeValue.Month, dateTimeValue.Day);
                    commande.DateCommande = dateOnly;
                    ListCommande.Add(commande);
                }
                con.Close();
                return ListCommande;
            }
        }
    }
}
