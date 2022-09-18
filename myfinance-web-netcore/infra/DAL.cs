using System.Data.SqlClient;
using System.Data;



namespace myfinance_web_netcore.infra
{
    public class DAL
    {
        private SqlConnection conn; //objeto pra criar conexão com o DB
        private string connectionString;
        public static IConfiguration? Configuration; //a ? indica que é um singleton
        private static DAL Instancia;
        // private static object? get;

        public static DAL getInstancia()
        {            
            get{
                if(Instancia == null)
                   Instancia = new();                
                return Instancia;
            };
        }
        private DAL()
        {
            connectionString = Configuration.GetValue<string>("connectionString"); //mesmo nome pra colocar no arquivo de config
        }

        public void Conectar()
        {
            
            conn = new();
            conn.ConnectionString = connectionString;
            conn.Open();
        }
        
        public DataTable getDataTable(string sql)
        {
            var dataTable = new DataTable();
            //pegar as coisas no banco
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conn);
            dataAdapter.Fill(dataTable);
            

            return dataTable;
        }
        //insert, update, delete
        public void RunSqlCommand (string str )
        {
            SqlCommand command= new SqlCommand(str ,conn);
            command.ExecuteNonQuery();

        }

        public void Disconnect()
        {
            conn.Close();
        }

        
    }
}