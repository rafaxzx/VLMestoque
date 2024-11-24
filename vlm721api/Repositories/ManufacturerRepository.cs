using System.Data;
using System.Data.SQLite;
using vlm721api.Models;

namespace vlm721api.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository, IDisposable
    {
        private readonly SQLiteConnection _dbConnection;
        //Criar string de conexão com banco de dados [X]
        //Criar (usar) uma conexao usando a string [X]
        //Abrir a conexao [X]
        //Criar uma string com o comando SQL [X]
        //Criar (usar) um objeto comandoSQL passando a string SQL e uma conexao []
        //Utilizar o comando para executar a Query []
        public ManufacturerRepository(SQLiteConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
        }
        public void Add(Manufacturer manufacturer)
        {
            string sql = @"INSERT INTO Manufacturers (Manufacturername, Manufacturerimagepath)
                                VALUES (@Manufacturers, @Manufacturerimagepath);";
            using (var command = new SQLiteCommand(sql, _dbConnection))
            {
                command.Parameters.AddWithValue("@Manufacturers", manufacturer.Name);
                command.Parameters.AddWithValue("@Manufacturerimagepath", manufacturer.ImagePath);
                command.ExecuteNonQuery();
            }

        }
        public void Delete(int id)
        {
            string sql = @"DELETE FROM Manufacturers WHERE Id = @id;";
            using (var command = new SQLiteCommand(sql, _dbConnection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }

        }
        public List<Manufacturer> GetAll()
        {
            var manufacturers = new List<Manufacturer>();
            string sql = @"SELECT * FROM Manufacturers";
            using (var command = new SQLiteCommand(sql, _dbConnection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    manufacturers.Add(new Manufacturer(
                            name: reader["Manufacturername"].ToString() ?? "nome inválido",
                            imagePath: reader["Manufacturerimagepath"].ToString() ?? "imagem inválida",
                            id: Convert.ToInt32(reader["Id"])
                        ));
                }
            }

            return manufacturers;
        }
        public Manufacturer GetById(int id)
        {
            Manufacturer manufacturer = null;
            string sql = @"SELECT * FROM Manufacturers WHERE Id = @id";
            using (var command = new SQLiteCommand(sql, _dbConnection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        manufacturer = new Manufacturer(
                            name: reader["Manufacturername"].ToString() ?? "nome inválido",
                            imagePath: reader["Manufacturerimagepath"].ToString() ?? "imagem inválida",
                            id: Convert.ToInt32(reader["Id"])
                            );
                    }
                }
            }

            return manufacturer;
        }
        public void Update(Manufacturer manufacturer, int id)
        {
            if (this.GetById(id) != null)
            {
                string sql = @"UPDATE Manufacturers SET Manufacturername = @name, Manufacturerimagepath = @imagepath WHERE Id = @id";
                using (var command = new SQLiteCommand(sql, _dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", manufacturer.Name);
                    command.Parameters.AddWithValue("@imagepath", manufacturer.ImagePath);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void Dispose()
        {
            if (_dbConnection != null
                && _dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();

            }
        }
    }
}
