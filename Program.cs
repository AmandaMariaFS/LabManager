using Microsoft.Data.Sqlite;
using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

var databaseConfig = new DatabaseConfig();
var databaseSetup = new DatabaseSetup(databaseConfig);
var computerRepository = new ComputerRepository(databaseConfig);
var labRepository = new LabRepository(databaseConfig);

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

        var computer = new Computer(id, ram, processador);
        computerRepository.Save(computer);
    }

    if (modelAction == "Show")
    {
        var id = Convert.ToInt32(args[2]);
        var computer = computerRepository.GetById(id);
        Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
    }

    if (modelAction == "Update")
    {
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processador = args[4];
        var computer = new Computer(id, ram, processador);

        computerRepository.Update(computer);
    }

    if (modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);
        computerRepository.Delete(id);
    }
}

if(modelName == "Lab")
{
    if(modelAction == "List")
    {
        foreach (var Lab in labRepository.GetAll())
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", Lab.Id, Lab.Number, Lab.Name, Lab.Block);
        }
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var number = Convert.ToInt32(args[3]);
        var name = args[4];
        var block = args[5];
        Console.WriteLine("New Lab");
        Console.WriteLine("{0}, {1}, {2}, {3}", id, number, name, block);

        var lab = new Lab(id, number, name, block);
        labRepository.Save(lab);
    }

    if (modelAction == "Show")
    {
        var id = Convert.ToInt32(args[2]);
        var lab = labRepository.GetById(id);
        Console.WriteLine("{0}, {1}, {2}, {3}", lab.Id, lab.Number, lab.Name, lab.Block);
    }

    if (modelAction == "Update")
    {
        var id = Convert.ToInt32(args[2]);
        var number = Convert.ToInt32(args[3]);
        var name = args[4];
        var block = args[5];
        var lab = new Lab(id, number, name, block);

        labRepository.Update(lab);
    }

    if (modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);
        labRepository.Delete(id);
    }
}