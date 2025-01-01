using ArtOfEngineer.Components.DataFiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;


namespace ArtOfEngineer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagasinApiController : ControllerBase
    {
        private IConfiguration _configuration;
        public MagasinApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("produits")]
        public JsonResult GetProduit()
        {
            string querry = "SELECT Designation, Quantity FROM dbo.Produit WHERE Quantity<20";
            DataTable table = new DataTable();
            string dataSource = _configuration.GetConnectionString("magasinDB");
            SqlDataReader reader;
            using (var connection = new SqlConnection(dataSource))
            {
                connection.Open();
                using (var mycmd = new SqlCommand(querry,connection))
                {
                    reader = mycmd.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                    connection.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpGet]
        [Route("stats")]
        public JsonResult GetStats()
        {
            string querry = "SELECT P.Designation, S.Vente FROM dbo.Stat S, dbo.Produit P WHERE P.Produit_id=S.Produit_id";
            DataTable table = new DataTable();
            string dataSource = _configuration.GetConnectionString("magasinDB");
            SqlDataReader reader;
            using (var connection = new SqlConnection(dataSource))
            {
                connection.Open();
                using (var mycmd = new SqlCommand(querry, connection))
                {
                    reader = mycmd.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                    connection.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        [Route("produits")]
        public JsonResult AddProduit([FromBody] Produit newProduit)
        {
            string query = @"
        INSERT INTO dbo.Produit (Produit_id, Designation, Quantity, Prix) 
        VALUES (@ProduitId, @Designation, @Quantity, @Prix)";

            string dataSource = _configuration.GetConnectionString("magasinDB");

            using (var connection = new SqlConnection(dataSource))
            {
                connection.Open();
                using (var mycmd = new SqlCommand(query, connection))
                {
                    // Add parameters to prevent SQL injection
                    mycmd.Parameters.AddWithValue("@ProduitId", newProduit.ProduitId);
                    mycmd.Parameters.AddWithValue("@Designation", newProduit.Designation);
                    mycmd.Parameters.AddWithValue("@Quantity", newProduit.Quantity);
                    mycmd.Parameters.AddWithValue("@Prix", newProduit.Prix);

                    mycmd.ExecuteNonQuery(); // Execute the query
                }
            }
            return new JsonResult("Produit ajouté avec succès");
        }

        [HttpDelete]
        [Route("produits")]
        public JsonResult DeleteProduit([FromBody] int IDProduit)
        {
            string query = @"Delete FROM dbo.Produit WHERE Produit_id = @id";

            string dataSource = _configuration.GetConnectionString("magasinDB");

            using (var connection = new SqlConnection(dataSource))
            {
                connection.Open();
                using (var mycmd = new SqlCommand(query, connection))
                {
                    // Add parameters to prevent SQL injection
                    mycmd.Parameters.AddWithValue("@id", IDProduit);

                    mycmd.ExecuteNonQuery(); // Execute the query
                }
            }

            return new JsonResult("Produit ajouté avec succès");
        }
    }
}
