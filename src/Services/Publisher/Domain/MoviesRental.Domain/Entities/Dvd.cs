using MoviesRental.Core.DomainObjects;

namespace MoviesRental.Domain.Entities
{
    public class Dvd : Entity
    {
        protected Dvd()
        {}

        public Dvd(string title, int genre, DateTime published, int copies, Guid directorId)
        {
            Available = true;
            UpdateTitle(title);
            UpdateGenre(genre);
            UpdatePublishedDate(published);
            UpdateCopies(copies);
            UpdateDirector(directorId);
        }
        public string Title { get; private set; }
        public EGenre Genre { get; private set; }
        public DateTime Published { get; private set; }
        public bool Available { get; private set; }
        public int Copies { get; private set; }
        public Guid DirectorId { get; private set; }
        public Director Director { get; set; }
        public const int MIN_TITLE_LENGTH = 2;
        public const int MAX_TITLE_LENGTH = 50;

        public void RentCopy()
        {
            if(Copies == 0 || !Available) throw new DomainException($"DVD {Title} isn't available to rent");

            var copies = Copies - 1;
            UpdateCopies(copies);
        }

        public void ReturnCopy()
        {
            if (!Available) throw new DomainException($"DVD {Title} isn't available");

            var copies = Copies + 1;
            UpdateCopies(copies);
        }

        public void UpdateTitle(string title)
        {
            if (!Available)
                throw new DomainException($"DVD {Title} isn't available");
            if (string.IsNullOrWhiteSpace(title) || title.Length < MIN_TITLE_LENGTH || title.Length > MAX_TITLE_LENGTH)
                throw new DomainException($"{Title} - invalid title name");

            Title = title;
            UpdatedAt = DateTime.Now;
        }

        public void UpdateGenre(int genre)
        {
            if (!Available)
                throw new DomainException($"DVD {Title} isn't available to rent");

            Genre = genre switch
            {
                0 => EGenre.Action,
                1 => EGenre.Adventure,
                2 => EGenre.Comedy,
                3 => EGenre.Drama,
                4 => EGenre.Fantasy,
                5 => EGenre.Horror,
                6 => EGenre.Mystery,
                7 => EGenre.Romance,
                8 => EGenre.ScienceFiction,
                9 => EGenre.Thriller,
                10 => EGenre.Animation,
                11 => EGenre.Documentary,
                12 => EGenre.Musical,
                13 => EGenre.War,
                14 => EGenre.Western,
                15 => EGenre.Crime,
                16 => EGenre.Biography,
                17 => EGenre.Family,
                _ => throw new ArgumentException($"Invalid genre value: {genre}")
            };

            UpdatedAt = DateTime.Now;
        }

        public void UpdatePublishedDate(DateTime date)
        {
            if (!Available)
                throw new DomainException($"DVD {Title} isn't available");
            var todayDate = DateTime.Now;

            if (todayDate < date)
                throw new DomainException($"Invalid published date");

            Published = date;
            UpdatedAt = todayDate;
        }

        public void UpdateDirector(Guid directorId)
        {
            if (!Available)
                throw new DomainException($"DVD {Title} isn't available");

            if (directorId == Guid.Empty)
                throw new DomainException($"Invalid director's Id");

            DirectorId = directorId;
            UpdatedAt = DateTime.Now;
        }

        public void UpdateCopies(int copies)
        {
            if (!Available)
                throw new DomainException($"DVD {Title} isn't available");

            if (copies < 0)
                throw new DomainException($"Number of copies must be greater than zero");

            Copies = copies;
            UpdatedAt = DateTime.Now;
        }

        public void Delete()
        {
            if (!Available)
                throw new DomainException($"DVD {Title} is already deleted");

            Available = false;
            Copies = 0;
            DeletedAt = DateTime.Now;
        }
    }
}
