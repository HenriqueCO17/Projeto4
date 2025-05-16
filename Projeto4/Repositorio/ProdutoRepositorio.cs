using System.Data;
using MySql.Data.MySqlClient;
using Projeto4.Models;

namespace Projeto4.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO Produto(Nome,Descricao,Preco,Quantidade) values (@Nome,@Descricao,@Preco,@Quantidade)", conexao);

                cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = produto.Nome;

                cmd.Parameters.Add("@Descricao", MySqlDbType.VarChar).Value = produto.Descricao;

                cmd.Parameters.Add("@Preco", MySqlDbType.Decimal).Value = produto.Preco;

                cmd.Parameters.Add("@Quantidade", MySqlDbType.Int32).Value = produto.Quantidade;

                cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }
        public bool Atualizar(Produto produto)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();

                    MySqlCommand cmd = new MySqlCommand("UPDATE Produto SET Nome=@Nome, Descricao=@Descricao, Preco=@Preco, Quantidade=@Quantidade" + "WHERE Id=@Id", conexao);

                    cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = produto.Id;

                    cmd.Parameters.Add("@Nome", MySqlDbType.VarChar).Value = produto.Nome;

                    cmd.Parameters.Add("@Descricao", MySqlDbType.VarChar).Value = produto.Descricao;

                    cmd.Parameters.Add("@Preco", MySqlDbType.Decimal).Value = produto.Preco;

                    cmd.Parameters.Add("@Quantidade", MySqlDbType.Int32).Value = produto.Quantidade;

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Produto> TodosProdutos()
        {
            List<Produto> ProdutoList = new List<Produto>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Produto", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    ProdutoList.Add(
                        new Produto
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = ((string)dr["Nome"]),
                            Descricao = ((string)dr["Descricao"]),
                            Preco = ((decimal)dr["Preco"]),
                            Quantidade = ((int)dr["Preco"]),
                        });
                }
                return ProdutoList;
            }
        }

        public Produto ObterProduto(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Produto Where Id=@Id", conexao);

                cmd.Parameters.AddWithValue("@Id", id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                MySqlDataReader dr;

                Produto produto = new Produto();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    produto.Id = Convert.ToInt32(dr["Id"]);
                    produto.Nome = ((string)dr["Nome"]);
                    produto.Descricao = ((string)dr["Descricao"]);
                    produto.Preco = ((decimal)dr["Preco"]);
                    produto.Quantidade = ((int)dr["Preco"]);
                }
                return produto;
            }
        }
        public void Excluir(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM Produto Where Id=@Id", conexao);

                cmd.Parameters.AddWithValue("@id", id);

                int i = cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }
    }
}