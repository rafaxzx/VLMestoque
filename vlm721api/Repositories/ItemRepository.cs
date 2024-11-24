using System.Data;
using System.Data.SQLite;
using vlm721api.Models;

namespace vlm721api.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly SQLiteConnection _dbConnection;
        public ItemRepository(SQLiteConnection dbConnection)
        {
            _dbConnection = dbConnection;
            _dbConnection.Open();
        }
        public void Add(DTOItem item)
        {
            string sql = @"INSERT INTO Items
                            (Codeintern, Itemname, Itemspecification,
                             Manufacturerid, UDCnumber, Locationtray,
                             Minstock, Maxstock)
                             VALUES
                             (@Codeintern, @Itemname, @Itemspecification,
                             @Manufacturerid, @UDCnumber, @Locationtray,
                             @Minstock, @Maxstock);";
            using (var command = new SQLiteCommand(sql, _dbConnection))
            {
                command.Parameters.AddWithValue("@Codeintern", item.CodIntern);
                command.Parameters.AddWithValue("@Itemname", item.ItemName);
                command.Parameters.AddWithValue("@Itemspecification", item.ItemSpecification);
                command.Parameters.AddWithValue("@Manufacturerid", item.ManufacturerId);
                command.Parameters.AddWithValue("@UDCnumber", item.UDCNumber);
                command.Parameters.AddWithValue("@Locationtray", item.LocationTray);
                command.Parameters.AddWithValue("@Minstock", item.MinStock);
                command.Parameters.AddWithValue("@Maxstock", item.MaxStock);

                command.ExecuteNonQuery();
            }

        }

        public void Delete(int id)
        {
            string sql = @"DELETE FROM Items WHERE Id = @id;";
            using (var command = new SQLiteCommand(sql, _dbConnection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }

        }
        public List<Item> GetAll()
        {
            List<Item> listItems = new List<Item>();
            string sql = @"
                    SELECT Items.*,
                        Manufacturers.Manufacturername,
                        Manufacturers.Manufacturerimagepath,
                        Manufacturers.Id as Manufacturer_id
                    FROM Items
                    INNER JOIN Manufacturers on Manufacturers.Id = Items.Manufacturerid;";
            using (var command = new SQLiteCommand(sql, _dbConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listItems.Add(new Item(
                            codIntern: reader["Codeintern"].ToString(),
                            itemName: reader["Itemname"].ToString(),
                            itemSpecification: reader["Itemspecification"].ToString(),
                            manufacturerId: Convert.ToInt32(reader["Manufacturer_id"]),
                            uDCNumber: Convert.ToInt32(reader["UDCnumber"]),
                            locationTray: reader["Locationtray"].ToString(),
                            minStock: Convert.ToInt32(reader["Minstock"]),
                            maxStock: Convert.ToInt32(reader["Maxstock"]),
                            manufacturer: new Manufacturer(name: reader["Manufacturername"].ToString(),
                                                           imagePath: reader["Manufacturerimagepath"].ToString(),
                                                           id: Convert.ToInt32(reader["Manufacturer_id"]))
                            ));
                    }
                }
            }
            return listItems;
        }

        public Item GetById(int id)
        {
            Item item = null;
            string sql = @"SELECT FROM Items WHERE Id = @id INNER JOIN Manufacturers on Manufacturers.Id = Items.Manufacturerid;";
            using (var command = new SQLiteCommand(sql, _dbConnection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = new Item(
                            codIntern: reader["Codeintern"].ToString(),
                            itemName: reader["Itemname"].ToString(),
                            itemSpecification: reader["Itemspecification"].ToString(),
                            manufacturerId: Convert.ToInt32(reader["Manufacturerid"]),
                            uDCNumber: Convert.ToInt32(reader["UDCnumber"]),
                            locationTray: reader["Locationtray"].ToString(),
                            minStock: Convert.ToInt32(reader["Minstock"]),
                            maxStock: Convert.ToInt32(reader["Maxstock"]),
                            manufacturer: new Manufacturer(
                                name: reader["Manufacturername"].ToString(),
                                imagePath: reader["Manufacturerimagepath"].ToString(),
                                id: Convert.ToInt32(reader["Manufacturerid"]))
                            );
                    }
                }
            }
            return item;
        }
        public void Update(DTOItem item, int id)
        {
            var itemOld = this.GetById(id);
            if (itemOld == null)
            {
                return;
            }
            item.Id = item.Id != 0 ? item.Id : itemOld.Id;
            string sql = @"UPDATE Items
                        SET Codeintern = @Codeintern,
                            Itemname = @Itemname,
                            Itemspecification = @Itemspecification,
                            Manufacturerid = @Manufacturerid,
                            UDCnumber = @UDCnumber,
                            Locationtray = @Locationtray,
                            Minstock = @Minstock,
                            Maxstock = @Maxstock
                        WHERE Id = @id;";
            using (var command = new SQLiteCommand(sql, _dbConnection))
            {
                command.Parameters.AddWithValue("@Codeintern", item.CodIntern);
                command.Parameters.AddWithValue("@Itemname", item.ItemName);
                command.Parameters.AddWithValue("@Itemspecification", item.ItemSpecification);
                command.Parameters.AddWithValue("@Manufacturerid", item.ManufacturerId);
                command.Parameters.AddWithValue("@UDCnumber", item.UDCNumber);
                command.Parameters.AddWithValue("@Locationtray", item.LocationTray);
                command.Parameters.AddWithValue("@Minstock", item.MinStock);
                command.Parameters.AddWithValue("@Maxstock", item.MaxStock);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
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
