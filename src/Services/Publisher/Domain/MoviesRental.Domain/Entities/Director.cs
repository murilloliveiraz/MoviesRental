using MoviesRental.Core.DomainObjects;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MoviesRental.Domain.Entities
{
    public class Director : Entity
    {
        public Director(string name, string surname)
        {
            UpdateName(name);
            UpdateSurname(surname);
        }
        public const int MIN_LENGTH = 3;
        public const int MAX_LENGTH = 30;
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public List<Dvd> _dvds = new List<Dvd>();
        public IReadOnlyList<Dvd> Dvds => _dvds;

        public string FullName()
        {
            return $"{Name} {Surname}";
        }

        public void UpdateName(string name)
        {
            if (!ValidateName(name))
                throw new DomainException("Invalid name for director");
            Name = name;
        }

        public void UpdateSurname(string surname)
        {
            if (!ValidateName(surname))
                throw new DomainException("Invalid surname for director");
            Surname = surname;
        }

        public bool ValidateName(string value)
        {
            if(string.IsNullOrEmpty(value) || value.Length < MIN_LENGTH || value.Length > MAX_LENGTH) return false;
            return Regex.IsMatch(value, "^[a-zA-Z]+$");
        }
    }
}
