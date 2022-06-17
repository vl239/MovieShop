﻿using System;
namespace ApplicationCore.Entities
{
	public class MovieGenre
	{
        public int MovieId { get; set; }
        public int GenreId { get; set; }

        // navigation props
        public Movie Movie { get; set; }
        public Genre Genre { get; set; }
    }
}
