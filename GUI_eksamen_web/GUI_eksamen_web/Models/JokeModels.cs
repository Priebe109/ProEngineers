using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace GUI_eksamen_web.Models
{
    public class Joke
    {
        [Key]
        public int Id { get; set; }
        public string NewJoke { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public string TagString { get; set; }

        private List<string> Tags { get; set; }

        //public SelectList SearchedList { get; set; }
    }


    public class JokeDbContext : DbContext
    {
        public DbSet<Joke> Jokes { get; set; }
    }
}