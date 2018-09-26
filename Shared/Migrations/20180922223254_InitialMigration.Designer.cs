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

// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Database;

namespace Shared.Migrations
{
    [DbContext(typeof(SoraContext))]
    [Migration("20180922223254_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Shared.Database.Models.LeaderboardRx", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("Count100Ctb");

                    b.Property<ulong>("Count100Mania");

                    b.Property<ulong>("Count100Std");

                    b.Property<ulong>("Count100Taiko");

                    b.Property<ulong>("Count300Ctb");

                    b.Property<ulong>("Count300Mania");

                    b.Property<ulong>("Count300Std");

                    b.Property<ulong>("Count300Taiko");

                    b.Property<ulong>("Count50Ctb");

                    b.Property<ulong>("Count50Mania");

                    b.Property<ulong>("Count50Std");

                    b.Property<ulong>("Count50Taiko");

                    b.Property<ulong>("CountMissCtb");

                    b.Property<ulong>("CountMissMania");

                    b.Property<ulong>("CountMissStd");

                    b.Property<ulong>("CountMissTaiko");

                    b.Property<double>("PeppyPointsCtb");

                    b.Property<double>("PeppyPointsMania");

                    b.Property<double>("PeppyPointsStd");

                    b.Property<double>("PeppyPointsTaiko");

                    b.Property<ulong>("PlayCountCtb");

                    b.Property<ulong>("PlayCountMania");

                    b.Property<ulong>("PlayCountStd");

                    b.Property<ulong>("PlayCountTaiko");

                    b.Property<ulong>("RankedScoreCtb");

                    b.Property<ulong>("RankedScoreMania");

                    b.Property<ulong>("RankedScoreStd");

                    b.Property<ulong>("RankedScoreTaiko");

                    b.Property<ulong>("TotalScoreCtb");

                    b.Property<ulong>("TotalScoreMania");

                    b.Property<ulong>("TotalScoreStd");

                    b.Property<ulong>("TotalScoreTaiko");

                    b.HasKey("Id");

                    b.ToTable("LeaderboardRx");
                });

            modelBuilder.Entity("Shared.Database.Models.LeaderboardStd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("Count100Ctb");

                    b.Property<ulong>("Count100Mania");

                    b.Property<ulong>("Count100Std");

                    b.Property<ulong>("Count100Taiko");

                    b.Property<ulong>("Count300Ctb");

                    b.Property<ulong>("Count300Mania");

                    b.Property<ulong>("Count300Std");

                    b.Property<ulong>("Count300Taiko");

                    b.Property<ulong>("Count50Ctb");

                    b.Property<ulong>("Count50Mania");

                    b.Property<ulong>("Count50Std");

                    b.Property<ulong>("Count50Taiko");

                    b.Property<ulong>("CountMissCtb");

                    b.Property<ulong>("CountMissMania");

                    b.Property<ulong>("CountMissStd");

                    b.Property<ulong>("CountMissTaiko");

                    b.Property<double>("PeppyPointsCtb");

                    b.Property<double>("PeppyPointsMania");

                    b.Property<double>("PeppyPointsStd");

                    b.Property<double>("PeppyPointsTaiko");

                    b.Property<ulong>("PlayCountCtb");

                    b.Property<ulong>("PlayCountMania");

                    b.Property<ulong>("PlayCountStd");

                    b.Property<ulong>("PlayCountTaiko");

                    b.Property<ulong>("RankedScoreCtb");

                    b.Property<ulong>("RankedScoreMania");

                    b.Property<ulong>("RankedScoreStd");

                    b.Property<ulong>("RankedScoreTaiko");

                    b.Property<ulong>("TotalScoreCtb");

                    b.Property<ulong>("TotalScoreMania");

                    b.Property<ulong>("TotalScoreStd");

                    b.Property<ulong>("TotalScoreTaiko");

                    b.HasKey("Id");

                    b.ToTable("LeaderboardStd");
                });

            modelBuilder.Entity("Shared.Database.Models.LeaderboardTouch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("Count100Ctb");

                    b.Property<ulong>("Count100Mania");

                    b.Property<ulong>("Count100Std");

                    b.Property<ulong>("Count100Taiko");

                    b.Property<ulong>("Count300Ctb");

                    b.Property<ulong>("Count300Mania");

                    b.Property<ulong>("Count300Std");

                    b.Property<ulong>("Count300Taiko");

                    b.Property<ulong>("Count50Ctb");

                    b.Property<ulong>("Count50Mania");

                    b.Property<ulong>("Count50Std");

                    b.Property<ulong>("Count50Taiko");

                    b.Property<ulong>("CountMissCtb");

                    b.Property<ulong>("CountMissMania");

                    b.Property<ulong>("CountMissStd");

                    b.Property<ulong>("CountMissTaiko");

                    b.Property<double>("PeppyPointsCtb");

                    b.Property<double>("PeppyPointsMania");

                    b.Property<double>("PeppyPointsStd");

                    b.Property<double>("PeppyPointsTaiko");

                    b.Property<ulong>("PlayCountCtb");

                    b.Property<ulong>("PlayCountMania");

                    b.Property<ulong>("PlayCountStd");

                    b.Property<ulong>("PlayCountTaiko");

                    b.Property<ulong>("RankedScoreCtb");

                    b.Property<ulong>("RankedScoreMania");

                    b.Property<ulong>("RankedScoreStd");

                    b.Property<ulong>("RankedScoreTaiko");

                    b.Property<ulong>("TotalScoreCtb");

                    b.Property<ulong>("TotalScoreMania");

                    b.Property<ulong>("TotalScoreStd");

                    b.Property<ulong>("TotalScoreTaiko");

                    b.HasKey("Id");

                    b.ToTable("LeaderboardTouch");
                });

            modelBuilder.Entity("Shared.Database.Models.Users", b =>
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

            modelBuilder.Entity("Shared.Database.Models.UserStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte>("CountryId");

                    b.HasKey("Id");

                    b.ToTable("UserStats");
                });
#pragma warning restore 612, 618
        }
    }
}
