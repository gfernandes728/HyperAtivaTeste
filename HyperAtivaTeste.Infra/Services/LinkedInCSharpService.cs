using HyperAtivaTeste.Domains.Interfaces.Services;

namespace HyperAtivaTeste.Infra.Services
{
    public class LinkedInCSharpService : ILinkedInCSharpService
    {
        private string[] employees = { "Joe", "Bob", "Carol", "Alice", "Will" };

        public LinkedInCSharpService() { }

        public List<string> Question16()
        {
            var result = new List<string>();
            var employeeQuery = from person in employees orderby person select person;

            foreach (var employee in employeeQuery)
                result.Add(employee);

            return result;
        }
    }
}
