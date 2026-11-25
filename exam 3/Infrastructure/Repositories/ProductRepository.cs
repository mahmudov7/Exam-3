using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    public int AddProduct(Product product)
    {
        using var con = Database.Connection();
        con.Open();
        var sql = "insert into products(name, price, quantity, category_id) values(@name, @price, @quantity, @categoryId) returning id";
        var com = new NpgsqlCommand(sql, con);
        com.Parameters.AddWithValue("@name", product.Name);
        com.Parameters.AddWithValue("@price", product.Price);
        com.Parameters.AddWithValue("@quantity", product.Quantity);
        com.Parameters.AddWithValue("@categoryId", product.CategoryId);
        var Id = Convert.ToInt32(com.ExecuteScalar());

        return Id;
    }

    public bool DelateProduct(int id)
    {
        using var connection = Database.Connection();
        connection.Open();

        string sql = "delete from products where id = @id";

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        return command.ExecuteNonQuery() > 0;
    }


    public void GetById(int id)
    {

        using var connection = Database.Connection();
        connection.Open();

        string sql = "SELECT * FROM products WHERE id = @id";

        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            Console.WriteLine("ID: " + reader.GetInt32(0));
            Console.WriteLine("Name: " + reader.GetString(1));
            Console.WriteLine("Price: " + reader.GetDecimal(2));
            Console.WriteLine("Quantity: " + reader.GetInt32(3));
            Console.WriteLine("Category ID: " + reader.GetInt32(4));
        }
        else
        {
            Console.WriteLine("Product not found");
        }
    }


    public bool UpdateProduct(Product product)
    {
        using var connection = Database.Connection();
        connection.Open();

        string sql = "update products set name = @name, price = @price, quantity = @quantity, category_id = @categoryId where id = @id";

        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@id", product.Id);
        command.Parameters.AddWithValue("@name", product.Name);
        command.Parameters.AddWithValue("@price", product.Price);
        command.Parameters.AddWithValue("@quantity", product.Quantity);
        command.Parameters.AddWithValue("@categoryId", product.CategoryId);

        return command.ExecuteNonQuery() > 0;

    }


    public List<Product> GetAll()
    {
        List<Product> products = new();

        using var con = Database.Connection();
        con.Open();

        string sql = "select * from products";

        using var cmd = new NpgsqlCommand(sql, con);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            products.Add(new Product
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2),
                Quantity = reader.GetInt32(3),
                CategoryId = reader.GetInt32(4)
            });
        }

        return products;
    }
    public void SearchByName(string term)
    {
        using var connection = Database.GetConnection();
        connection.Open();

        string sql = "select * from products where name like @term";

        var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@term", "%" + term + "%");

        using var reader = command.ExecuteReader();

        bool found = false;

        while (reader.Read())
        {
            found = true;

            Console.WriteLine("ID: " + reader.GetInt32(0));
            Console.WriteLine("Name: " + reader.GetString(1));
            Console.WriteLine("Price: " + reader.GetDecimal(2));
            Console.WriteLine("Quantity: " + reader.GetInt32(3));
            Console.WriteLine("Category ID: " + reader.GetInt32(4));
            Console.WriteLine("-------------------");
        }

        if (!found)
            Console.WriteLine("Product not found.");
    }
    public void Filter(int? categoryId, decimal? minPrice, decimal? maxPrice)
    {
        using var connection = Database.Connection();
        connection.Open();

        string sql = "SELECT * FROM products WHERE 1=1";

        if (categoryId.HasValue)
            sql += " AND category_id = @categoryId";

        if (minPrice.HasValue)
            sql += " AND price >= @minPrice";

        if (maxPrice.HasValue)
            sql += " AND price <= @maxPrice";

        using var command = new NpgsqlCommand(sql, connection);

        if (categoryId.HasValue)
            command.Parameters.AddWithValue("@categoryId", categoryId.Value);

        if (minPrice.HasValue)
            command.Parameters.AddWithValue("@minPrice", minPrice.Value);

        if (maxPrice.HasValue)
            command.Parameters.AddWithValue("@maxPrice", maxPrice.Value);

        using var reader = command.ExecuteReader();

        bool found = false;

        while (reader.Read())
        {
            found = true;

            Console.WriteLine("ID: " + reader.GetInt32(0));
            Console.WriteLine("Name: " + reader.GetString(1));
            Console.WriteLine("Price: " + reader.GetDecimal(2));
            Console.WriteLine("Quantity: " + reader.GetInt32(3));
            Console.WriteLine("Category ID: " + reader.GetInt32(4));
            Console.WriteLine("-------------------");
        }

        if (!found)
            Console.WriteLine("No products found.");
    }

}
