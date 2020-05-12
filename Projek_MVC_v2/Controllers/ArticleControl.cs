using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Projek_MVC_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projek_MVC_v2.Controllers
{
    public class ArticleControl : Controller
    {
        private readonly IConfiguration configuration;
        public ArticleControl(IConfiguration config)
        {
            this.configuration = config;
        }

      
        public IActionResult Article()
        {
          
            var model = new List<Artykuly>();

            String connectionString = configuration.GetConnectionString("DefaultConnection");

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand com = new SqlCommand("Select * from artykuly", connection);

            SqlDataReader rdr = com.ExecuteReader();

            while (rdr.Read())
            {
                var art = new Artykuly();
                art.ID_artykulu = (int)rdr["idartykuly"];
                art.Nazwa_artykulu = rdr["nazwa_art"].ToString();

                model.Add(art);
            }


            connection.Close();



            return View(model);



        }



    }
}
