using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AdventurousContacts.Model.DAL
{
    public abstract class DALBase
    {
        #region Fält

        // Sträng med information som används för att ansluta till "SQL Server"-databasen.
        private static string _connectionString;

        #endregion

        #region Konstruktorer

        // Initierar statiskt data. (Konstruktorn anropas automatiskt innan första instansen skapas
        // eller innan någon statisk medlem används.)
        static DALBase()
        {
            // Hämtar anslutningssträngen från web.config.
            _connectionString = WebConfigurationManager.ConnectionStrings["AdventurousContactsConnectionString"].ConnectionString;
        }

        #endregion

        #region Metod

        // Skapar och initierar ett anslutningsobjekt.
        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        #endregion
    }
}