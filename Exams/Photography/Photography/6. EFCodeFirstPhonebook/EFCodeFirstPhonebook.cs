using System.Linq;

namespace _6.EFCodeFirstPhonebook
{
    class EFCodeFirstPhonebook
    {
        static void Main()
        {
            var context = new PhonebookContext();

            context.Channels.Count();
        }
    }
}
