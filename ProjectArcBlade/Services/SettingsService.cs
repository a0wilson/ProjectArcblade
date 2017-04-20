using Microsoft.EntityFrameworkCore;
using ProjectArcBlade.Data;
using ProjectArcBlade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectArcBlade.Services
{
    public class SettingsService
    {
        public List<Rule> GetSettingValues(ApplicationDbContext context, int leagueId, int categoryId)
        {
            var results = new List<Rule>();

            var settings = context.Settings.ToList();
            
            foreach(Setting s in settings)
            {
                var leagueRule = context.LeagueRules.Include(lr=> lr.Rule).Where(lr => lr.League.Id == leagueId && lr.Rule.Setting.Id == s.Id).SingleOrDefault();
                var categoryRule = context.CategoryRules.Include(lr=>lr.Rule).Where(cr => cr.Category.Id == categoryId && cr.Rule.Setting.Id == s.Id).SingleOrDefault();

                var rule = new Rule { Setting = s, Value = int.MaxValue};
                if (leagueRule != null) rule = leagueRule.Rule;
                if (categoryRule != null) rule = categoryRule.Rule;

                results.Add(rule);
            }

            return results;
        }

        public Rule GetSettingValue(ApplicationDbContext context, int settingId, int leagueId, int categoryId)
        {
            var setting = context.Settings.Find(settingId);
            
            var leagueRule = context.LeagueRules.Include(lr => lr.Rule).Where(lr => lr.League.Id == leagueId && lr.Rule.Setting.Id == setting.Id).SingleOrDefault();
            var categoryRule = context.CategoryRules.Include(lr => lr.Rule).Where(cr => cr.Category.Id == categoryId && cr.Rule.Setting.Id == setting.Id).SingleOrDefault();

            var rule = new Rule { Setting = setting, Value = int.MaxValue };
            if (leagueRule != null) rule = leagueRule.Rule;
            if (categoryRule != null) rule = categoryRule.Rule;
           
            return rule;
        }

    }
}
