using ConsoleTestApp;

List<Pallet> genPallets = GeneratePallets();
bool exit = false;
while (!exit)
{
    Console.Clear();
    Console.WriteLine(" 1. Все паллеты сгрупированые по сроку годности, отсортировные по возрастанию срока годности, в каждой группе отсортировные паллеты по весу.\n " +
      "2. 3 паллеты, которые содержат коробки с наибольшим сроком годности, отсортированные по возрастанию объема.  \n " +
      "0. Exit");
    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();

            var groupedPallets = genPallets
              .GroupBy(p => p.expirationDate)
              .Select(g => new {
                  expDate = g.Key,
                  Count = g.Count(),
                  Pallets = g.Select(p => p).OrderBy(p => p.weight)
              })
              .OrderBy(g => g.expDate);

            foreach (var g in groupedPallets)
            {
                Console.WriteLine($"\n Expiraton date: {g.expDate}, Count: {g.Count}");
                Console.WriteLine("_______________________________________________________________________________________________");
                Console.WriteLine("|_    ID    _|_  weight  _|_  width   _|_  heigth  _|_  length  _|_   size   _|_  expDate _|");
                foreach (var pal in g.Pallets)
                    Console.WriteLine($"|_{pal.ID,10}_|_{pal.weight,10}_|_{pal.width,10}_|_{pal.height,10}_|_{pal.length,10}_|_{pal.size,10}_|_{pal.expirationDate,10}_|");
                Console.WriteLine("_______________________________________________________________________________________________");
            }
            Console.WriteLine($"Total Pallets: {groupedPallets.Count()}");
            Console.WriteLine("\n Нажмите любую кнопку что бы вернуться в меню");
            Console.ReadLine();
            break;

        case "2":
            Console.Clear();

            var orderedPallets = genPallets
              .OrderByDescending(p => p.expirationDate)
              .Take(3)
              .OrderBy(p => p.size);

            Console.WriteLine("________________________________________");
            Console.WriteLine("|_    ID    _|_   size   _|_  expDate _|");
            foreach (var pal in orderedPallets)
                Console.WriteLine($"|_{pal.ID,10}_|_{pal.size,10}_|_{pal.expirationDate,10}_|");
            Console.WriteLine("________________________________________");
            Console.WriteLine("\n Нажмите любую кнопку что бы вернуться в меню");
            Console.ReadLine();
            break;

        case "0":
            Console.Clear();
            exit = true;
            break;
    }
}

List<Box> GenerateBoxes()
{
    var random = new Random();
    int count = random.Next(15);
    List<Box> boxes = new List<Box>();

    for (int i = 1; i <= count; i++)
    {
        double height = random.Next(2, 50);
        double width = random.Next(2, 50);
        double length = random.Next(2, 50);
        double weight = random.Next(2, 50);
        bool hasProductionDate = random.Next(0, 2) == 0; // будет ли дата производства
        DateOnly? productionDate = null;
        DateOnly? expirationDate = null;

        if (hasProductionDate)
        {
            DateTime startDate = new DateTime(2025, 4, 1);
            int daysRange = (DateTime.Today - startDate).Days;
            productionDate = DateOnly.FromDateTime(startDate.AddDays(random.Next(daysRange)));
            expirationDate = productionDate.Value.AddDays(100);
        }
        else
        {
            bool hasExpirationDate = random.Next(0, 2) == 0; // будет ли срок годности 
            if (hasExpirationDate)
            {
                DateTime startDate = new DateTime(2025, 4, 1);
                int daysRange = (DateTime.Today - startDate).Days;
                expirationDate = DateOnly.FromDateTime(startDate.AddDays(random.Next(daysRange)));
                productionDate = null;
            }
            else // если нет ничего, то генерирует дату производства
            {
                DateTime startDate = new DateTime(2025, 4, 1);
                int daysRange = (DateTime.Today - startDate).Days;
                productionDate = DateOnly.FromDateTime(startDate.AddDays(random.Next(daysRange)));
                expirationDate = null;
            }
        }

        try
        {
            boxes.Add(new Box(i, height, width, length, weight, expirationDate, productionDate));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании коробки {i}: {ex.Message}");
        }
    }
    return boxes;
}

List<Pallet> GeneratePallets()
{
    var random = new Random();
    int count = 100;
    List<Pallet> Pallets = new List<Pallet>();

    for (int i = 0; i < count; i++)
    {
        double height = random.Next(50, 200);
        double width = random.Next(50, 200);
        double length = 14;
        List<Box>? boxes = new List<Box>();

        List<Box> boxesToAdd = GenerateBoxes();
        if (boxesToAdd.Count > 0)
        {
            foreach (Box box in boxesToAdd)
            {
                if (box.width <= width && box.length <= length)
                {
                    try
                    {
                        boxes.Add(new Box(box.ID, box.height, box.width, box.length, box.weight, box.expirationDate, box.productionDate));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при добавлении коробки {box.ID} в палетту {i}: {ex.Message}");
                    }
                }
            }
        }
        else
        {
            boxes = null;
        }

        try
        {
            Pallets.Add(new Pallet(i, height, width, length, boxes));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании палетты {i}: {ex.Message}");
        }
    }

    return Pallets;
}