using Microsoft.Data.Sqlite;

var connection = new SqliteConnection("Data Source=database.db");
connection.Open();

var command = connection.CreateCommand();
command.CommandText = @"
    CREATE TABLE IF NOT EXISTS Computers(
        id int not null primary key,
        ram varchar(100) not null,
        processador varchar(100) not null
    );
";

command.ExecuteNonQuery();

connection.Close();

var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        Console.WriteLine("List Computer");
        connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Computers";

        var reader = command.ExecuteReader();

        while(reader.Read())
        {
            Console.WriteLine("{0}, {1}, {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(1));
        }

        connection.Close();
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processador = args[4];
        Console.WriteLine("New Computer");
        Console.WriteLine("{0}, {1}, {2}", id, ram, processador);

        connection = new SqliteConnection("Data Source=database.db");
        connection.Open();

        command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Computers VALUES($id, $ram, $processador)";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$ram", ram);
        command.Parameters.AddWithValue("$processador", processador);

        command.ExecuteNonQuery();
        connection.Close();
        }
}