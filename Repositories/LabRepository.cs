using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace LabManager.Repositories;

class LabRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public LabRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public IEnumerable<Lab> GetAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var labs = connection.Query<Lab>("SELECT * FROM Lab");

        return labs;
    }

    public void Save(Lab lab)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Lab VALUES(@Id, @Number, @Name, @Block)", lab);
    }

    public Lab GetById(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
    
        var lab = connection.QuerySingle<Lab>("SELECT * FROM Lab WHERE id_lab == @Id", new { Id = id });

        return lab;
    }

    public Lab Update(Lab lab)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Lab SET number = @Number, name = @Name, block = @Block  WHERE id_lab == @id", lab);
        
        return GetById(lab.Id);
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Lab WHERE id_lab == @Id", new {Id = id});
    }

    public bool ExistsById(int id)
    {
        var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id_lab) FROM Lab WHERE id_lab = $id";
        command.Parameters.AddWithValue("$id_lab", id);

        // var reader = command.ExecuteReader();
        // reader.Read();
        // var result = reader.GetBoolean(0);
        
        bool result = Convert.ToBoolean(command.ExecuteScalar());
        connection.Close();

        return result;
    }

    private Lab ReaderToLab(SqliteDataReader reader)
    {
        var lab = new Lab(
            reader.GetInt32(0),
            reader.GetInt32(1),
            reader.GetString(2),
            reader.GetString(3)
        );

        return lab;
    }

}