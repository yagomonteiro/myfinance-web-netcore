namespace myfinance_web_netcore.Models
{
    public class PlanoContaModel
    {
        public int Id {get; set;}

        public string Descricao;

        public string Tipo;

        public List<PlanoContaModel> ListaPlanoContas()
        {
            List<lPlanoContaModel> lista = new List<PlanoContaModel>();
            var objDAL = new DAL.getInstancia();
            objDAL.Conectar();
            var sql = "SELECT ID, DESCRICAO, TIPO FROM PLANO_CONTAS";
            var dataTable = objDAL.getDataTable(sql);
            for(int i=0;i<dataTable.Rows.Count; i++)
            {
                var planoConta = new PlanoContaModel()
                {
                    Id= int.Parse(dataTable.Rows[i]["ID"].ToString()),
                    Descricao = dataTable.Rows[i]["DESCRICAO"].ToString(),
                    Tipo = dataTable.Rows[i]["TIPO"].ToString()
                };
                lista.Add();
            }
            return lista;

        }    
    }
}