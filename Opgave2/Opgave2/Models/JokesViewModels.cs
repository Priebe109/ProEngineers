using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GUI_Eksamen_Opgave2.Models
{
    public class Joke
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string JokeText { get; set; }
        public string Source { get; set; }
        public string Tags { get; set; }
    }

    public class JokesDbContext : DbContext
    {
        public DbSet<Joke> Jokes { get; set; }
    }
    
}