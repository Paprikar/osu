﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using System.Collections.Generic;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Users.Drawables;
using osuTK;
using osu.Game.Users;

namespace osu.Game.Overlays.Rankings.Tables
{
    public class PerformanceTable : RankingsTable<UserStatistics>
    {
        public PerformanceTable(int page = 1)
            : base(page)
        {
        }

        protected override TableColumn[] CreateAdditionalHeaders() => new[]
        {
            new TableColumn("Accuracy", Anchor.Centre, new Dimension(GridSizeMode.AutoSize)),
            new TableColumn("Play Count", Anchor.Centre, new Dimension(GridSizeMode.AutoSize)),
            new TableColumn("Performance", Anchor.Centre, new Dimension(GridSizeMode.AutoSize)),
            new TableColumn("SS", Anchor.Centre, new Dimension(GridSizeMode.AutoSize)),
            new TableColumn("S", Anchor.Centre, new Dimension(GridSizeMode.AutoSize)),
            new TableColumn("A", Anchor.Centre, new Dimension(GridSizeMode.AutoSize)),
        };

        protected override Drawable[] CreateContent(int index, UserStatistics item)
        {
            var content = new List<Drawable>
            {
                new OsuSpriteText
                {
                    Text = $"#{index + 1}",
                    Font = OsuFont.GetFont(size: TEXT_SIZE, weight: FontWeight.Bold)
                },
            };

            var username = new LinkFlowContainer(t => t.Font = OsuFont.GetFont(size: TEXT_SIZE)) { AutoSizeAxes = Axes.Both };
            username.AddUserLink(item.User);

            content.AddRange(new Drawable[]
            {
                new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(7, 0),
                    Children = new Drawable[]
                    {
                        new UpdateableFlag(item.User.Country)
                        {
                            Size = new Vector2(20, 13),
                            ShowPlaceholderOnNull = false,
                        },
                        username
                    }
                },
                new ColoredRowText
                {
                    Text = $@"{item.Accuracy:F2}%",
                },
                new ColoredRowText
                {
                    Text = $@"{item.PlayCount:N0}",
                },
                new RowText
                {
                    Text = $@"{item.PP:N0}",
                },
                new ColoredRowText
                {
                    Text = $@"{item.GradesCount.SS + item.GradesCount.SSPlus:N0}",
                },
                new ColoredRowText
                {
                    Text = $@"{item.GradesCount.S + item.GradesCount.SPlus:N0}",
                },
                new ColoredRowText
                {
                    Text = $@"{item.GradesCount.A:N0}",
                },
            });

            return content.ToArray();
        }
    }
}
