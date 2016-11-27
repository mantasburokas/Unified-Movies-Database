using System;

namespace Movies.Models.Entities
{
    public class MovieRequest
    {
        public string Name { get; }

        public DateTime Created { get; }

        public MovieRequest(string name)
        {
            Name = name;

            Created = DateTime.UtcNow;
        }
    }
}