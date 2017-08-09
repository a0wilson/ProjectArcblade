using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Models.TeamViewModels
{
    public class ManageNominationsViewModel : IValidatableObject
    {
        public Team Team { get; set; }
        public MatchTemplate MatchTemplate {get; set;}
        public List<SelectListItem> AvailablePlayers { get; set; }
        public List<SelectListItem> TeamPlayers { get; set; }
        public int[] NominatedClubPlayerIds { get; set; }
        public int[] NominatedPlayerGroupIds { get; set; }
        public int[] NominatedPlayerRankIds { get; set; }
        public int[] NomintatedPlayerIds { get; set; }
        public int CaptainId { get; set; }
        public int TeamId { get; set; }
        public List<NameValuePair> Warnings { get; set; }
        public List<NameValuePair> Errors { get; set; }
        public bool DuplicateEntries { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NominatedClubPlayerIds.Distinct().Count() != NominatedClubPlayerIds.Count())
            {
                yield return new ValidationResult("A player cannot be selected multiple times.", new[] { "DuplicateEntries" });
            }
        }
    }
}
