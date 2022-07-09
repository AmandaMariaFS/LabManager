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
        
        if (computerRepository.GetAll().Count() > 0)
        {
            foreach (var computer in computerRepository.GetAll())
            {
                Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
            }
        }
        else
        {
            Console.WriteLine("Nenhum computador adicionado ainda");
        }
     
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4];

        if(!computerRepository.ExistsById(id))
        {
            Console.WriteLine($"Computador {id} adicionado");

            var computer = new Computer(id, ram, processor);
            computerRepository.Save(computer);
        }
        else
        {
            Console.WriteLine($"O computador {id} já existe");
        }

    }

    if (modelAction == "Show")
    {
        var id = Convert.ToInt32(args[2]);

        if(computerRepository.ExistsById(id))
        {
            var computer = computerRepository.GetById(id);
            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
        }
        else
        {
            Console.WriteLine($"O computador {id} não existe");
        }

    }

    if (modelAction == "Update")
    {
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processador = args[4];
        var computer = new Computer(id, ram, processador);

        if(computerRepository.ExistsById(id))
        {
            computerRepository.Update(computer);
            Console.WriteLine("Computador atualizado com sucesso");
        }
        else
        {
            Console.WriteLine($"O computador {id} não existe");
        }

    }

    if (modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);

        if(computerRepository.ExistsById(id))
        {
            computerRepository.Delete(id);
            Console.WriteLine("Computador deletado com sucesso");
        }
        else
        {
            Console.WriteLine($"O computador {id} não existe");
        }
    }
}

if(modelName == "Lab")
{
    if(modelAction == "List")
    {

        if (labRepository.GetAll().Count() > 0)
        {
            foreach (var Lab in labRepository.GetAll())
            {
                Console.WriteLine("{0}, {1}, {2}, {3}", Lab.Id, Lab.Number, Lab.Name, Lab.Block);
            }
        }
        else
        {
            Console.WriteLine("Nenhum lab adicionado ainda");
        }
        
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var number = Convert.ToInt32(args[3]);
        var name = args[4];
        var block = args[5];

        if(!labRepository.ExistsById(id))
        {
            Console.WriteLine($"Lab {id} adicionado");

            var lab = new Lab(id, number, name, block);
            labRepository.Save(lab);
        }
        else
        {
            Console.WriteLine($"O lab {id} já existe");
        }
        
    }

    if (modelAction == "Show")
    {
        var id = Convert.ToInt32(args[2]);

        if(labRepository.ExistsById(id))
        {
            var lab = labRepository.GetById(id);
            Console.WriteLine("{0}, {1}, {2}, {3}", lab.Id, lab.Number, lab.Name, lab.Block);
        }
        else
        {
            Console.WriteLine($"O lab {id} não existe");
        }
    }

    if (modelAction == "Update")
    {
        var id = Convert.ToInt32(args[2]);
        var number = Convert.ToInt32(args[3]);
        var name = args[4];
        var block = args[5];
        var lab = new Lab(id, number, name, block);

        if(labRepository.ExistsById(id))
        {
            labRepository.Update(lab);
            Console.WriteLine($"Lab {id} atualizado");
        }
        else
        {
            Console.WriteLine($"O lab {id} não existe");
        }
    }

    if (modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);

        if(labRepository.ExistsById(id))
        {
            labRepository.Delete(id);
            Console.WriteLine($"Lab {id} deletado");
        }
        else
        {
            Console.WriteLine($"O lab {id} não existe");
        }
        
    }
}