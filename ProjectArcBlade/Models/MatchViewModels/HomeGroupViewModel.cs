using ProjectArcBlade.Data;
using System.Linq;

namespace ProjectArcBlade.Models.MatchViewModels
{
    public class HomeGroupViewModel : GroupViewModel
    {
        public SetViewModel[] Sets { get; set; }
        public PlayerViewModel[] Players { get; set; }

        public int SetWinTotal { get { return Sets.Where(s => s.AggregatedHomeResult == Constants.ResultType.Win).Count(); } }
        public int GameWinTotal { get { return Sets.Sum(s => s.Games.Where(g => g.AggregatedHomeResult == Constants.ResultType.Win).Count()); } }
        public int PointsTotal { get { return Sets.Sum(s => s.Games.Sum(g => g.AggregatedHomeScoreWithDefault)); } }
    }
}
