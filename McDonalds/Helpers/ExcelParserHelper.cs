using ExcelDataReader;
using McDonalds.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace McDonalds.Helpers
{
    public class ExcelParserHelper
    {
        public static DeploimentDateModel CheckDeploimentDateFromExcelFile(string fileName, int restaurantId, DateTime currentDate)
        {
            // exemple de la case 32 comme date de deploiement
            DeploimentDateModel model = new DeploimentDateModel()
            {
                RestaurantId = restaurantId
            };

            #region Excel

            FileInfo file = new FileInfo(fileName);

            string nomFichier = file.FullName;
            DataSet dsExcelTest = null;

            IExcelDataReader excelReader = null;

            try
            {
                FileStream stream = File.Open(nomFichier, FileMode.Open, FileAccess.Read);

                excelReader = Path.GetExtension(nomFichier).ToLower() == ".xls" 
                    ? ExcelReaderFactory.CreateBinaryReader(stream) 
                    : ExcelReaderFactory.CreateOpenXmlReader(stream);

                dsExcelTest = excelReader.AsDataSet();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (excelReader != null && !excelReader.IsClosed)
                    excelReader.Close();
            }
            
            DataTable deploiementTable = dsExcelTest.Tables["Déploiement"];

            

            for (int i = 0; i < deploiementTable.Rows.Count; i++)
            {
                if ( Convert.ToInt32(deploiementTable.Rows[i][0]) == restaurantId)
                {
                    model.DeploimentDate = Convert.ToDateTime(deploiementTable.Rows[i][32]);
                }
            };

            #endregion

            return model;
        }
    }
}