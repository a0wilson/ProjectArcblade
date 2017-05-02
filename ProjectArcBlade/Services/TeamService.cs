using Microsoft.EntityFrameworkCore;
using ProjectArcBlade.Data;
using ProjectArcBlade.Interfaces;
using ProjectArcBlade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Services
{
    public class TeamService
    {
        private ApplicationDbContext _context;
        private HomeMatchTeam _homeMatchTeam;
        private HomeMatchTeamCaptain _homeMatchTeamCaptain;
        private AwayMatchTeam _awayMatchTeam;
        private AwayMatchTeamCaptain _awayMatchTeamCaptain;

        public async Task UpdateMatchTeamCaptainAsync<T>(ApplicationDbContext context, int matchTeamId, int captainId) where T : IMatchTeam
        {
            _context = context;
            
            if(typeof(T).Equals(typeof(HomeMatchTeam))) //Home Match Team
            {
                await UpdateHomeMatchTeamCaptainAsync(matchTeamId, captainId);
            }
            else // assume away team.
            {
                await UpdateAwayMatchTeamCaptainAsync(matchTeamId, captainId);
            }
        }

        /// <summary>
        /// Updates the home match team captain - creates record if it does not exist.
        /// </summary>
        /// <param name="homeMatchTeamId"></param>
        /// <param name="captainId"></param>
        /// <returns></returns>
        private async Task UpdateHomeMatchTeamCaptainAsync( int homeMatchTeamId, int captainId)
        {
            _homeMatchTeam = await _context.HomeMatchTeams.FindAsync(homeMatchTeamId);
            await GetHomeMatchTeamCaptainAsync(homeMatchTeamId);

            if(captainId != 0)
            {
                var clubPlayer = await _context.ClubPlayers.FindAsync(captainId);
                _homeMatchTeamCaptain.ClubPlayer = clubPlayer;
                _context.HomeMatchTeamCaptains.Update(_homeMatchTeamCaptain);
            }
            else
            {
                //if 0 then remove the club player as captain.
                _homeMatchTeamCaptain.ClubPlayer = null;
                _context.HomeMatchTeamCaptains.Update(_homeMatchTeamCaptain);
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the away match team captain - creates the record if it does not exist.
        /// </summary>
        /// <param name="awayMatchTeamId"></param>
        /// <param name="captainId"></param>
        /// <returns></returns>
        private async Task UpdateAwayMatchTeamCaptainAsync(int awayMatchTeamId, int captainId)
        {
            _awayMatchTeam = await _context.AwayMatchTeams.FindAsync(awayMatchTeamId);
            await GetAwayMatchTeamCaptainAsync(awayMatchTeamId);

            if (captainId != 0)
            {
                var clubPlayer = await _context.ClubPlayers.FindAsync(captainId);
                _awayMatchTeamCaptain.ClubPlayer = clubPlayer;
                _context.AwayMatchTeamCaptains.Update(_awayMatchTeamCaptain);
            }
            else
            {
                //if 0 then remove the club player as captain.
                _awayMatchTeamCaptain.ClubPlayer = null;
                _context.AwayMatchTeamCaptains.Update(_awayMatchTeamCaptain);
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asyncrhonously gets the home match team captain 
        /// If a record does not exist it creates it setting the home match team in the process.
        /// </summary>
        /// <param name="homeMatchTeamId"></param>
        /// <returns></returns>
        private async Task GetHomeMatchTeamCaptainAsync(int homeMatchTeamId )
        {
            _homeMatchTeamCaptain = null;

            if (homeMatchTeamId != 0)
            {
                _homeMatchTeamCaptain = await _context.HomeMatchTeamCaptains
                    .Include(hmtc => hmtc.ClubPlayer)
                    .Include(hmtc => hmtc.HomeMatchTeam)
                    .Where(hmtc => hmtc.HomeMatchTeamId == homeMatchTeamId)
                    .FirstOrDefaultAsync();

                if (_homeMatchTeamCaptain == null)
                {
                    _homeMatchTeamCaptain = new HomeMatchTeamCaptain
                    {
                        HomeMatchTeam = _homeMatchTeam,
                    };
                    _context.HomeMatchTeamCaptains.Add(_homeMatchTeamCaptain);
                }
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Asyncrhonously gets the away match team captain 
        /// If a record does not exist it creates it setting the away match team in the process.
        /// </summary>
        /// <param name="awayMatchTeamId"></param>
        /// <returns></returns>
        private async Task GetAwayMatchTeamCaptainAsync(int awayMatchTeamId)
        {
            _awayMatchTeamCaptain = null;

            if (awayMatchTeamId != 0)
            {
                _awayMatchTeamCaptain = await _context.AwayMatchTeamCaptains
                    .Include(amtc => amtc.ClubPlayer)
                    .Include(amtc => amtc.AwayMatchTeam)
                    .Where(amtc => amtc.AwayMatchTeamId == awayMatchTeamId)
                    .FirstOrDefaultAsync();

                if (_awayMatchTeamCaptain == null)
                {
                    _awayMatchTeamCaptain = new AwayMatchTeamCaptain
                    {
                        AwayMatchTeam = _awayMatchTeam,
                    };
                    _context.AwayMatchTeamCaptains.Add(_awayMatchTeamCaptain);
                }
                await _context.SaveChangesAsync();
            }
        }


    }
}
