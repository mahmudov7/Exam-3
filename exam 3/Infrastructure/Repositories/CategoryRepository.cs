using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    public void AddCategory(Category category)
    {
        using var con = Database.Connection();
        con.Open();
        var sql="insert into categories (name) values(@name)";
        var com = new NpgsqlCommand(sql,con);
        com.Parameters.AddWithValue("name",category.Name);
        com.ExecuteNonQuery();
    }

    public void DelateCategory(int id)
    {
        
    }

    public List<Category> GetCategories()
    {
        throw new NotImplementedException();
    }

    public void GetCategpry(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateCategory(Category category)
    {
        throw new NotImplementedException();
    }

}
