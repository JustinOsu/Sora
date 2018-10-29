﻿#region copyright

/*
MIT License

Copyright (c) 2018 Robin A. P.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

#endregion

namespace Shared.Database
{
    using System;
    using Helpers;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

    public sealed class SoraContext : DbContext
    {
        private static readonly bool[] Migrated = {false};

        public SoraContext()
        {
            if (Migrated[0]) return;
            lock (Migrated)
            {
                if (Migrated[0]) return;
                Database.Migrate();
                Migrated[0] = true;
            }
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<Users> Users { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<UserStats> UserStats { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<Scores> Scores { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<Friends> Friends { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<LeaderboardStd> LeaderboardStd { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<LeaderboardRx> LeaderboardRx { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<LeaderboardTouch> LeaderboardTouch { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<Beatmaps> Beatmaps { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<BeatmapSets> BeatmapSets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Config cfg = Config.ReadConfig();
            if (cfg.MySql.Hostname == null)
                Environment.Exit(1);
            optionsBuilder.UseMySql(
                $"Server={cfg.MySql.Hostname};Database={cfg.MySql.Database};User={cfg.MySql.Username};Password={cfg.MySql.Password};Port={cfg.MySql.Port};",
                mysqlOptions => { mysqlOptions.ServerVersion(new Version(10, 2, 15), ServerType.MariaDb); }
            );
        }
    }
}
