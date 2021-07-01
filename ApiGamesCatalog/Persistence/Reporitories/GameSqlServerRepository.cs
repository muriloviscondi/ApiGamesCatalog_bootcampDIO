using ApiGamesCatalog.Entities;
using ApiGamesCatalog.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiGamesCatalog.Persistence.Reporitories
{
    public class GameSqlServerRepository : IGameRepository
    {
        private readonly SqlConnection sqlConnection;

        public GameSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Game>> GetAll(int page, int quantity)
        {
            var games = new List<Game>();

            var response = $"select * from Games order by id offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(response, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game((Guid)sqlDataReader["Id"], (string)sqlDataReader["Name"], (string)sqlDataReader["Producer"], (double)sqlDataReader["Price"]));
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task<Game> GetById(Guid id)
        {
            Game game = null;

            var response = $"select * from Games where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(response, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                game = new Game((Guid)sqlDataReader["Id"], (string)sqlDataReader["Name"], (string)sqlDataReader["Producer"], (double)sqlDataReader["Price"]);
            }

            await sqlConnection.CloseAsync();

            return game;
        }

        public async Task<List<Game>> GetByNameAndProducer(string name, string producer)
        {
            var games = new List<Game>();

            var response = $"select * from Games where Name = '{name}' and Producer = '{producer}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(response, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game((Guid)sqlDataReader["Id"], (string)sqlDataReader["Name"], (string)sqlDataReader["Producer"], (double)sqlDataReader["Price"]));
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task Create(Game game)
        {
            var response = $"insert Games (Id, Name, Producer, Price) values ('{game.Id}', '{game.Name}', '{game.Producer}', {game.Price.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(response, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Alter(Game game)
        {
            var response = $"update Games set Name = '{game.Name}', Producer = '{game.Producer}', Price = {game.Price.ToString().Replace(",", ".")} where Id = '{game.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(response, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Delete(Guid id)
        {
            var response = $"delete from Games where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(response, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
