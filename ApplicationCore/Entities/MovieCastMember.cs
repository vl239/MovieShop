using System;
namespace ApplicationCore.Entities
{
	public class MovieCastMember
	{
        public int MovieId { get; set; }
        public int CastId { get; set; }

        public string Character { get; set; }

        public CastMember CastMember { get; set; }
        public Movie Movie { get; set; }
    }
}

