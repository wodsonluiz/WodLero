using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using WodLero.Domain.Entities;
using WodLero.Domain.Interface;

namespace WodLero.Domain.Service
{
    public class PhrasesRepository : IPhrasesRepository
    {
        public bool Delete(int id, string _connection)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(_connection))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("Id", id, DbType.String);

                    conexao.Execute("Delete Phrases where Id = @Id", parametros);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Phrases> GetAll(string _connection)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(_connection))
                {
                    return conexao.Query<Phrases>("Select * from Phrases");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Phrases GetById(int id, string _connection)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(_connection))
                {
                    var parametros = new DynamicParameters();

                    parametros.Add("Id", id, DbType.Int32);

                    return conexao.QueryFirstOrDefault<Phrases>("Select * from Phrases where Id = @Id", parametros);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Insert(Phrases phrases, string _connection)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(_connection))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("Descricao", phrases.Descricao, DbType.String);
                    parametros.Add("Status", phrases.Status, DbType.Int32);
                    parametros.Add("Data_Registro", phrases.Data_Registro, DbType.DateTime);
                    parametros.Add("Autor", phrases.Autor, DbType.String);

                    conexao.Open();

                    conexao.Execute("Insert into Phrases(Descricao,Status,Data_Registro,Autor)" +
                         "values(@Descricao, @Status, @Data_Registro, @Autor)", parametros);

                    return true;
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }

        public bool Update(Phrases phrases, string _connection)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(_connection))
                {
                    var parametros = new DynamicParameters();
                    parametros.Add("Descricao", phrases.Descricao, DbType.String);
                    parametros.Add("Status", phrases.Status, DbType.Int32);
                    parametros.Add("Data_Registro", phrases.Data_Registro, DbType.DateTime);
                    parametros.Add("Autor", phrases.Autor, DbType.String);
                    parametros.Add("Id", phrases.Id, DbType.Int32);

                    conexao.Execute("Update Phrases set Descricao = @Descricao, Status = @Status, Data_Registro = @Data_Registro, Autor = @Autor where Id = @Id", parametros);
                };
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
