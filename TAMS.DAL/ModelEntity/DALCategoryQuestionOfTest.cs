using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAMS.Entity.Models;

namespace TAMS.DAL.ModelEntity
{
    public class DALCategoryQuestionOfTest: BaseContext
    {
        public static int addCategoriesQuetionForTest(List<CategoryQuestionOfTest> categoryQuestionOfTests)
        {
            DataTable dbTable = new DataTable("CategoryQuestionOfTestTable");
            dbTable.Columns.Add("IdCategoryQuestion", typeof(Int32));
            dbTable.Columns.Add("IdTest", typeof(Int32));
            dbTable.Columns.Add("ScoreQuestion", typeof(Int32));
            dbTable.Columns.Add("Numquestion", typeof(Int32));
            try
            {
                foreach (var item in categoryQuestionOfTests)
                {
                    dbTable.Rows.Add(item.IdCategoryQuestion, item.IdTest, item.ScoreQuestion, item.Numquestion);
                }
            }
            catch{}
            SqlConnection connection = new SqlConnection(ConnectString);
            connection.Open();
            string sql = "CategoryQuestionOfTest_Update";
            SqlParameter sqlParameter = new SqlParameter { ParameterName = "@CategoryQuestionTable", SqlDbType = SqlDbType.Structured, Value = dbTable };
            sqlParameter.TypeName = "CategoryQuestionOfTestTable";
            using (SqlCommand sqlCommand=new SqlCommand())
            {
                sqlCommand.Connection = connection;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = sql;
                sqlCommand.Parameters.Add(sqlParameter);
                return sqlCommand.ExecuteNonQuery();
            }
        }
        
    }
}
