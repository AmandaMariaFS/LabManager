using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;

var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);
var computerRepository = new ComputerRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        foreach (var computer in computerRepository.GetAll())
        {
            Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
        }
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processador = args[4];
        Console.WriteLine("New Computer");
        Console.WriteLine("{0}, {1}, {2}", id, ram, processador);

        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processador)";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$ram", ram);
        command.Parameters.AddWithValue("$processador", processador);

        command.ExecuteNonQuery();
        connection.Close();
        }
}

if(modelName == "Lab")
{
    if(modelAction == "List")
    {
        Console.WriteLine("List Lab");
        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Lab";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3));
        }

        connection.Close();
    }

    if(modelAction == "New")
    {
        var id_lab = Convert.ToInt32(args[2]);
        var number = Convert.ToInt32(args[3]);
        var name = args[4];
        var block = args[5];
        Console.WriteLine("New Computer");
        Console.WriteLine("{0}, {1}, {2}, {3}", id_lab, number, name, block);

        var connection = new SqliteConnection(databaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id_lab, $number, $name, $block)";
        command.Parameters.AddWithValue("$id_lab", id_lab);
        command.Parameters.AddWithValue("$number", number);  
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$block", block);

        command.ExecuteNonQuery();
        connection.Close();
        }
}