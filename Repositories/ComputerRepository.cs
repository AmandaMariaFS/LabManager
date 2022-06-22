using LabManager.Database;
using LabManager.Models;
using Microsoft.Data.Sqlite;
using Dapper;

namespace LabManager.Repositories;

class ComputerRepository
{
    private readonly DatabaseConfig _databaseConfig;

    public ComputerRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public IEnumerable<Computer> GetAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var computers =  connection.Query<Computer>("SELECT * FROM Computers");
        
        return computers;
    }

    public void Save(Computer computer)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("INSERT INTO Computers VALUES(@Id, @Ram, @Processor)",
computer);
    }

    public Computer GetById(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        var computer = connection.QuerySingle<Computer>("SELECT * FROM Computers WHERE id == @Id", new { Id = id });

        return computer;
    }

    public Computer Update(Computer computer)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("UPDATE Computers SET ram = @Ram, processor = @Processor  WHERE id == @Id", computer);
        
        return GetById(computer.Id);
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();

        connection.Execute("DELETE FROM Computers WHERE id == @Id", new {Id = id});
    }

    public bool ExistsById(int id)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
        connection.Open();
   
        bool result = connection.ExecuteScalar<bool>("SELECT count(id) FROM Computers WHERE id = $Id", new {Id = id});

        return result;
    }

}