using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Trabalho_PI
{
    class SqlUtils
    {
        public static SqlConnection conn = new SqlConnection("user id=pi_user; password=1; server=MSSQLSERVER; Trusted_Connection=yes;" +
                                               "database=Trabalho_PI; connection timeout=30");

        public static SqlDataReader process(String cmd)
        {
            SqlDataReader myReader = null;
            SqlCommand command = new SqlCommand(cmd, conn);
            conn.Open();
            myReader = command.ExecuteReader();
            conn.Close();
            return myReader;
        }

        public static IEnumerable<Board> getAllBoards()
        {
            String cmd = "Select * from Board";
            SqlDataReader myReader = process(cmd);
            
            while (myReader.Read())
            {
                int id = Int32.Parse(myReader["bid"].ToString());
                String name = myReader["boardName"].ToString();
                String description = myReader["descr"].ToString();
                yield return new Board(id, name, description);
            }
        }

        public static Board getBoard(int bid)
        {
            String cmd = "Select * from Board where bid="+bid.ToString();
            SqlDataReader myReader = process(cmd);

            int id = Int32.Parse(myReader["bid"].ToString());
            String name = myReader["boardName"].ToString();
            String description = myReader["descr"].ToString();
            return new Board(id, name, description);            
        }
    }
}
