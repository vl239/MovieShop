using System;
namespace ApplicationCore.Entities
{
	public class MovieCrewMember
	{
        public int MovieId { get; set; }
        public int CrewId { get; set; }
        public string Department { get; set; }
        public string Job { get; set; }

        public CrewMember CrewMember { get; set; }
        public Movie Movie { get; set; }
    }
}

