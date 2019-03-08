﻿#region LICENSE
/*
    Sora - A Modular Bancho written in C#
    Copyright (C) 2019 Robin A. P.

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/
#endregion

// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Services;

namespace Shared.Migrations
{
    [DbContext(typeof(Database))]
    partial class DatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Shared.Models.BeatmapSets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FavouriteCount");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("PassCount");

                    b.Property<int>("PlayCount");

                    b.HasKey("Id");

                    b.ToTable("BeatmapSets");
                });

            modelBuilder.Entity("Shared.Models.Beatmaps", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Ar");

                    b.Property<string>("Artist")
                        .IsRequired();

                    b.Property<string>("BeatmapCreator")
                        .IsRequired();

                    b.Property<int>("BeatmapCreatorId");

                    b.Property<int>("BeatmapSetId");

                    b.Property<float>("Bpm");

                    b.Property<float>("Cs");

                    b.Property<double>("Difficulty");

                    b.Property<string>("DifficultyName")
                        .IsRequired();

                    b.Property<string>("FileMd5")
                        .IsRequired();

                    b.Property<int>("HitLength");

                    b.Property<float>("Hp");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<float>("Od");

                    b.Property<int>("PassCount");

                    b.Property<int>("PlayCount");

                    b.Property<byte>("PlayMode");

                    b.Property<DateTime>("RankedDate");

                    b.Property<sbyte>("RankedStatus");

                    b.Property<string>("Tags")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("TotalLength");

                    b.HasKey("Id");

                    b.ToTable("Beatmaps");
                });

            modelBuilder.Entity("Shared.Models.Friends", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FriendId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("Shared.Models.LeaderboardRx", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("Count100Ctb");

                    b.Property<ulong>("Count100Osu");

                    b.Property<ulong>("Count100Taiko");

                    b.Property<ulong>("Count300Ctb");

                    b.Property<ulong>("Count300Osu");

                    b.Property<ulong>("Count300Taiko");

                    b.Property<ulong>("Count50Ctb");

                    b.Property<ulong>("Count50Osu");

                    b.Property<ulong>("Count50Taiko");

                    b.Property<ulong>("CountMissCtb");

                    b.Property<ulong>("CountMissOsu");

                    b.Property<ulong>("CountMissTaiko");

                    b.Property<double>("PerformancePointsCtb");

                    b.Property<double>("PerformancePointsOsu");

                    b.Property<double>("PerformancePointsTaiko");

                    b.Property<ulong>("PlayCountCtb");

                    b.Property<ulong>("PlayCountOsu");

                    b.Property<ulong>("PlayCountTaiko");

                    b.Property<ulong>("RankedScoreCtb");

                    b.Property<ulong>("RankedScoreOsu");

                    b.Property<ulong>("RankedScoreTaiko");

                    b.Property<ulong>("TotalScoreCtb");

                    b.Property<ulong>("TotalScoreOsu");

                    b.Property<ulong>("TotalScoreTaiko");

                    b.HasKey("Id");

                    b.ToTable("LeaderboardRx");
                });

            modelBuilder.Entity("Shared.Models.LeaderboardStd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("Count100Ctb");

                    b.Property<ulong>("Count100Mania");

                    b.Property<ulong>("Count100Osu");

                    b.Property<ulong>("Count100Taiko");

                    b.Property<ulong>("Count300Ctb");

                    b.Property<ulong>("Count300Mania");

                    b.Property<ulong>("Count300Osu");

                    b.Property<ulong>("Count300Taiko");

                    b.Property<ulong>("Count50Ctb");

                    b.Property<ulong>("Count50Mania");

                    b.Property<ulong>("Count50Osu");

                    b.Property<ulong>("Count50Taiko");

                    b.Property<ulong>("CountMissCtb");

                    b.Property<ulong>("CountMissMania");

                    b.Property<ulong>("CountMissOsu");

                    b.Property<ulong>("CountMissTaiko");

                    b.Property<double>("PerformancePointsCtb");

                    b.Property<double>("PerformancePointsMania");

                    b.Property<double>("PerformancePointsOsu");

                    b.Property<double>("PerformancePointsTaiko");

                    b.Property<ulong>("PlayCountCtb");

                    b.Property<ulong>("PlayCountMania");

                    b.Property<ulong>("PlayCountOsu");

                    b.Property<ulong>("PlayCountTaiko");

                    b.Property<ulong>("RankedScoreCtb");

                    b.Property<ulong>("RankedScoreMania");

                    b.Property<ulong>("RankedScoreOsu");

                    b.Property<ulong>("RankedScoreTaiko");

                    b.Property<ulong>("TotalScoreCtb");

                    b.Property<ulong>("TotalScoreMania");

                    b.Property<ulong>("TotalScoreOsu");

                    b.Property<ulong>("TotalScoreTaiko");

                    b.HasKey("Id");

                    b.ToTable("LeaderboardStd");
                });

            modelBuilder.Entity("Shared.Models.Scores", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Accuracy");

                    b.Property<ulong>("Count100");

                    b.Property<ulong>("Count300");

                    b.Property<ulong>("Count50");

                    b.Property<ulong>("CountGeki");

                    b.Property<ulong>("CountKatu");

                    b.Property<ulong>("CountMiss");

                    b.Property<DateTime>("Date");

                    b.Property<string>("FileMd5")
                        .IsRequired();

                    b.Property<short>("MaxCombo");

                    b.Property<uint>("Mods");

                    b.Property<double>("PeppyPoints");

                    b.Property<byte>("PlayMode");

                    b.Property<string>("ReplayMd5")
                        .IsRequired();

                    b.Property<string>("ScoreMd5")
                        .IsRequired();

                    b.Property<ulong>("TotalScore");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("Shared.Models.UserStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte>("CountryId");

                    b.HasKey("Id");

                    b.ToTable("UserStats");
                });

            modelBuilder.Entity("Shared.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("Privileges");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}