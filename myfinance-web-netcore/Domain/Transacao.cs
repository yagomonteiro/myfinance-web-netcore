using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myfinance_web_netcore.Infra;
using myfinance_web_netcore.Models;
using System.Globalization;
namespace myfinance_web_netcore.Domain
{
    public class Transacao
    {
        public void Inserir(TransacaoModel formulario)
        {
            var objDAL =  DAL.GetInstancia;
            objDAL.Conectar();
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;

            var sql="INSERT INTO TRANSACAO(data, valor, tipo, historico, id_plano_conta) "+
                    $"VALUES(" +
                    $"'{formulario.Data.ToString("yyyyMMdd")}', " +
                    $"{formulario.Valor.ToString(cultureInfo)}, " +
                    $"'{formulario.Tipo}', " +
                    $"'{formulario.Historico}', " +
                    $"{formulario.IdPlanoConta})";
            objDAL.ExecutarComandoSql(sql);
            objDAL.Desconectar();
        }
        public void Atualizar(TransacaoModel formulario)
        {
            CultureInfo cultureInfo = CultureInfo.InvariantCulture;
            var objDAL =  DAL.GetInstancia;
            objDAL.Conectar();
            
            var sql = $"UPDATE TRANSACAO SET "
                      + $"DATA='{formulario.Data.ToString("yyyyMMdd")}',"
                      + $"VALOR={formulario.Valor.ToString(cultureInfo)}, "
                      + $"TIPO='{formulario.Tipo}', "
                      + $"HISTORICO='{formulario.Historico}', "
                      + $"ID_PLANO_CONTA={formulario.IdPlanoConta} "
                      + $"WHERE id ={formulario.Id}";
            
            objDAL.ExecutarComandoSql(sql);
            objDAL.Desconectar();
        }
        public void Excluir(int id)
        {
            var objDAL =  DAL.GetInstancia;
            objDAL.Conectar();
            var sql = $"DELETE FROM TRANSACAO WHERE ID = {id}";
            objDAL.ExecutarComandoSql(sql);
            objDAL.Desconectar();
        }
        public TransacaoModel CarregarTransacaoPorId(int? id)
        {
            
        var objDAL =  DAL.GetInstancia;
        objDAL.Conectar();
            
        var sql = $"SELECT ID, DATA, VALOR, TIPO, HISTORICO, ID_PLANO_CONTA FROM TRANSACAO WHERE ID ={id}";
        var dataTable = objDAL.RetornarDataTable(sql);

            var transacao = new TransacaoModel()
            {
                Id=int.Parse(dataTable.Rows[0]["ID"].ToString()),
                Historico = dataTable.Rows[0]["HISTORICO"].ToString(),
                Tipo = dataTable.Rows[0]["TIPO"].ToString(),
                Data = DateTime.Parse(dataTable.Rows[0]["DATA"].ToString()),
                Valor = decimal.Parse(dataTable.Rows[0]["VALOR"].ToString()),
                IdPlanoConta = int.Parse(dataTable.Rows[0]["ID_PLANO_CONTA"].ToString())
            };
            
            objDAL.Desconectar();
            return transacao;
        }
        public List<TransacaoModel> FiltrarTransacaoPorPeriodo(TransacaoModel formulario)
        {
            List<TransacaoModel> lista = new List<TransacaoModel>();
            var objDAL =  DAL.GetInstancia;
            objDAL.Conectar();
            
            var sql = $"SELECT T.ID, T.DATA, T.VALOR, T.TIPO, T.HISTORICO, P.DESCRICAO FROM TRANSACAO T INNER JOIN PLANO_CONTAS P ON T.ID_PLANO_CONTA = P.ID WHERE DATA BETWEEN  '{formulario.Data1}' AND '{formulario.Data2}' ORDER BY DATA";
            var dataTable = objDAL.RetornarDataTable(sql);
        
                for(int i=0;i<dataTable.Rows.Count;i++)
                {
                var transacao = new TransacaoModel()
                    { 
                    Id=int.Parse(dataTable.Rows[i]["ID"].ToString()),
                    Historico = dataTable.Rows[i]["HISTORICO"].ToString(),
                    Tipo = dataTable.Rows[i]["TIPO"].ToString(),
                    Data = DateTime.Parse(dataTable.Rows[i]["DATA"].ToString()),
                    Valor = decimal.Parse(dataTable.Rows[i]["VALOR"].ToString()),
                    Descricao= dataTable.Rows[i]["DESCRICAO"].ToString(),
                    Data1=formulario.Data1,
                    Data2=formulario.Data2
                    };
                lista.Add(transacao);
                }
            
            objDAL.Desconectar();
            return lista;
        }
        public List<TransacaoModel>ListaTransacoes()
        {
            List<TransacaoModel> lista = new List<TransacaoModel>();
            var objDAL =  DAL.GetInstancia;
            objDAL.Conectar();
            
            var sql = "SELECT ID, DATA, VALOR, TIPO, HISTORICO, ID_PLANO_CONTA FROM TRANSACAO ORDER BY DATA";
            var dataTable = objDAL.RetornarDataTable(sql);
            
            for(int i=0;i<dataTable.Rows.Count;i++)
            {
                var transacao = new TransacaoModel()
                {
                    Id = int.Parse(dataTable.Rows[i]["ID"].ToString()),
                    Data = DateTime.Parse(dataTable.Rows[i]["DATA"].ToString()),
                    Valor = decimal.Parse(dataTable.Rows[i]["VALOR"].ToString()),
                    Tipo = dataTable.Rows[i]["TIPO"].ToString(),
                    Historico = dataTable.Rows[i]["HISTORICO"].ToString(),
                    IdPlanoConta = int.Parse(dataTable.Rows[i]["ID_PLANO_CONTA"].ToString())
                };

                lista.Add(transacao);
            }
            objDAL.Desconectar();
            return lista;
        }
    }
}