﻿using System;
namespace ApplicationCore.Entities
{
	public class CrewMember
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }
        public string ProfilePath { get; set; }

        public ICollection<MovieCrewMember> MoviesOfCrewMember { get; set; }
    }
}
