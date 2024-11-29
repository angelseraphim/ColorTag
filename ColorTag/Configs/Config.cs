﻿namespace ColorTag.Configs
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using Exiled.API.Interfaces;

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public string DataPath { get; set; } = "%config%/%data%";

        [Description("Rights so that the player can change his colors")]
        public string ColorRequirePermission { get; set; } = "colortag.get";

        [Description("Default limit (If the player's group is not in GroupColorLimit)")]
        public int DefaultColorLimit { get; set; } = 5;

        [Description("Limit for specific groups")]
        public Dictionary<string, int> GroupColorLimit { get; set; } = new Dictionary<string, int>()
        {
            {"owner", 10},
            {"admin", 5 },
            {"sex_specialist", 15}
        };

        [Description("Rights to remove colors from a player")]
        public string AdminRequirePermission { get; set; } = "colortag.admin";

        [Description("Rights to delete data base")]
        public string DropDataRequirePermission { get; set; } = "colortag.kill";

        [Description("Interval for color change")]
        public float Interval { get; set; } = 1f;
    }
}