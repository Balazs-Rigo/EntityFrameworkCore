using EntityFrameworkNet5.Data;
using EntityFrameworkNet5.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkNet5.ConsoleApp
{
    class Program
    {

        private static FootballLeagueDbContext context = new FootballLeagueDbContext();
        static async Task Main(string[] args)
        {
            var league = await GetLeagueByName("Bundesliga");
            //await context.Leagues.AddAsync(league);
            //await context.SaveChangesAsync();

            //await AddTeamsWithLeague(league);
            //await context.SaveChangesAsync();

            QueryFilter();
            
            Console.WriteLine("Press Any Key To End...");
            Console.ReadKey();

        }

        static async Task AddTeamsWithLeague(League league)
        {
            var teams = new List<Team>
            {
                new Team
                {
                    Name= "Bayern Munchen",
                    LeagueId = league.Id
                },
                  new Team
                {
                    Name= "Borossia Dortmund",
                    LeagueId = league.Id
                },
                    new Team
                {
                    Name= "Hertha BSC",
                    LeagueId = league.Id
                }
            };
            await context.AddRangeAsync(teams);
        }

        private static async void QueryFilter()
        {
            var league = await context.Leagues.Where(league => league.Name == "Serie A").ToListAsync() ;
        }

        private static async Task TrackingVsNoTracking()
        {
            var withTracking = await context.Teams.FirstOrDefaultAsync(q=>q.Id==2);
            var withNoTracking = await context.Teams.AsNoTracking().FirstOrDefaultAsync(q=>q.Id==8);

            withTracking.Name = "Inter Milan";
            withNoTracking.Name = "Rivoli United";

            var entriesBeforeSave = context.ChangeTracker.Entries();

            await context.SaveChangesAsync();

            var entriesAfterSave = context.ChangeTracker.Entries();
        }

        private static async Task<League> GetLeagueByName(string name)
        {
            return await context.Leagues.FirstOrDefaultAsync(q=>q.Name==name);
        }
    }
}
