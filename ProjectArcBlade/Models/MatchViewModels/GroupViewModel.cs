using ProjectArcBlade.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class HomeGroupViewModel : GroupViewModel
    {
        public SetViewModel[] Sets { get; set; }
        public PlayerViewModel[] Players { get; set; }

        public int SetWinTotal { get { return Sets.Where(s => s.AggregatedHomeResult == Constants.ResultType.Win).Count(); } }
        public int GameWinTotal { get { return Sets.Sum(s => s.Games.Where(g => g.AggregatedHomeResult == Constants.ResultType.Win).Count()); } }
        public int PointsTotal { get { return Sets.Sum(s => s.Games.Sum(g => g.AggregatedHomeScoreWithDefault)); } }

    }

    public class AwayGroupViewModel : GroupViewModel
    {
        public SetViewModel[] Sets { get; set; }
        public PlayerViewModel[] Players { get; set; }

        public int SetWinTotal { get { return Sets.Where(s => s.AggregatedAwayResult == Constants.ResultType.Win).Count(); } }
        public int GameWinTotal { get { return Sets.Sum(s => s.Games.Where(g => g.AggregatedAwayResult == Constants.ResultType.Win).Count()); } }
        public int PointsTotal { get { return Sets.Sum(s => s.Games.Sum(g => g.AggregatedAwayScoreWithDefault)); } }

    }
}
