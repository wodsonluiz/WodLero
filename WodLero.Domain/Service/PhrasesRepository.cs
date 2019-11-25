using Dapper;
using Polly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WodLero.Domain.Entities;
using WodLero.Domain.Interface;

namespace WodLero.Domain.Service
{
    public class PhrasesRepository : IPhrasesRepository
    {
        public async Task<bool> Delete(int id, string _connection)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connection))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("Id", id, DbType.String);

                    await Policy.Handle<Exception>()
                        .WaitAndRetryAsync(2, i => TimeSpan.FromSeconds(1))
                        .ExecuteAsync(async () => await conexao.ExecuteAsync("Delete Phrases where Id = @Id", parametros));
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Phrases>> GetAll(string _connection)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connection))
                {
                    return await Policy.Handle<Exception>()
                        .WaitAndRetryAsync(2, i => TimeSpan.FromSeconds(1))
                        .ExecuteAsync(async () => await conexao.QueryAsync<Phrases>("Select * from Phrases"));
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Phrases> GetById(int id, string _connection)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connection))
                {
                    var parametros = new DynamicParameters();

                    parametros.Add("Id", id, DbType.Int32);

                    return await Policy.Handle<Exception>()
                        .WaitAndRetryAsync(2, i => TimeSpan.FromSeconds(1))
                        .ExecuteAsync(async () => await conexao.QueryFirstOrDefaultAsync<Phrases>("Select * from Phrases where Id = @Id", parametros));
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Insert(Phrases phrases, string _connection)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connection))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("Descricao", phrases.Descricao, DbType.String);
                    parametros.Add("Status", phrases.Status, DbType.Int32);
                    parametros.Add("Data_Registro", phrases.Data_Registro, DbType.DateTime);
                    parametros.Add("Autor", phrases.Autor, DbType.String);

                    conexao.Open();

                    await Policy.Handle<Exception>()
                        .WaitAndRetryAsync(2, i => TimeSpan.FromSeconds(1))
                        .ExecuteAsync(async () => await conexao.ExecuteAsync("Insert into Phrases(Descricao,Status,Data_Registro,Autor)" +
                         "values(@Descricao, @Status, @Data_Registro, @Autor)", parametros));

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Phrases phrases, string _connection)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(_connection))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("Descricao", phrases.Descricao, DbType.String);
                    parametros.Add("Status", phrases.Status, DbType.Int32);
                    parametros.Add("Data_Registro", phrases.Data_Registro, DbType.DateTime);
                    parametros.Add("Autor", phrases.Autor, DbType.String);
                    parametros.Add("Id", phrases.Id, DbType.Int32);

                    await Policy.Handle<Exception>()
                        .WaitAndRetryAsync(2, i => TimeSpan.FromSeconds(1))
                        .ExecuteAsync(async () => await conexao.ExecuteAsync("Update Phrases set Descricao = @Descricao, Status = @Status, Data_Registro = @Data_Registro, Autor = @Autor where Id = @Id",
                        parametros));
                };
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
