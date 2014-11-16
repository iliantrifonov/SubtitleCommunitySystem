using SubtitleCommunitySystem.Model;
using SubtitleCommunitySystem.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleCommunitySystem.Test.Web
{
    public class ObjectGenerator
    {
        public static IEnumerable<Subtitle> GetValidDifferentSubtitles(int count)
        {
            var subtitles = new List<Subtitle>();

            for (int i = 0; i < count; i++)
            {
                subtitles.Add(new Subtitle()
                {
                    Id = i + 1,
                    IsFinished = true,
                    Language = new Language()
                    {
                        Id = i + 1,
                        Name = "Test Lang Name" + i,
                        Teams = new List<Team>()
                        {
                            new Team()
                            {
                                Id = i+ 1,
                                Name = "TeamName",
                            }
                        }
                    },
                    Movie = GetValidDifferentMovies(1).FirstOrDefault(),
                    Team = new Team()
                    {
                        Id = i + 1,
                        Name = "TeamName",
                    },
                    FinalFile = new DbFile() { Id = i + 1 }
                });
            }

            return subtitles;
        }

        public static IEnumerable<Movie> GetValidDifferentMovies(int count)
        {
            var movies = new List<Movie>();

            for (int i = 0; i < count; i++)
            {
                movies.Add(new Movie()
                {
                    Id = i + 1,
                    Name = "Test Movie" + count,
                    ReleaseDate = DateTime.Now,
                    InitialSource = new DbFile(),

                    Subtitles = new List<Subtitle>()
                    {
                        new Subtitle()
                        {
                            Id = i + 1,
                            IsFinished = true,
                            Name = "Test Name" + i,
                            Language = new Language()
                            {
                                Id = i + 1,
                                Name = "Test Lang Name" + i,
                                Teams = new List<Team>()
                                {
                                    new Team()
                                    {
                                        Id = i+ 1,
                                        Name = "TeamName",
                                    }
                                }
                            }
                        }
                    }
                });
            }

            return movies;
        }

        public static IEnumerable<SubtitleCacheSearchViewModel> GetValidDifferentSubtitleSearchViewModels(int count)
        {
            var subs = new List<SubtitleCacheSearchViewModel>();

            for (int i = 0; i < count; i++)
            {
                subs.Add(new SubtitleCacheSearchViewModel()
                {
                    Id = 1 + i,
                    Language = "Test Language" + i,
                    MovieId = 1 + i,
                    MovieName = "Test Movie Name" + i,
                    Name = "Test Name" + i,
                    ReleaseDate = DateTime.Now
                });
            }

            return subs;
        }
    }
}
