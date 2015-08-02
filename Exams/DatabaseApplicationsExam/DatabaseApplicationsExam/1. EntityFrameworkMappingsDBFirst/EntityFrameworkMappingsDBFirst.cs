namespace _1.EntityFrameworkMappingsDBFirst
{
    using System;

    class EntityFrameworkMappingsDBFirst
    {
        static void Main()
        {
            var context = new DiabloEntities();

            var characters = context.Characters;

            foreach (var character in characters)
            {
                Console.WriteLine(character.Name);
            }
        }
    }
}
