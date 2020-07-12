﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Menus;
using System;
using System.Collections.Generic;

namespace Dem1se.CustomReminders.UI
{
    /// <summary>The menu which lets the set new reminders.</summary>
    /// Contains parts of code from https://github.com/janavarro95/Stardew_Valley_Mods/blob/master/GeneralMods/HappyBirthday/Framework/BirthdayMenu.cs
    /// Thanks to janavarro95.
    internal class NewReminder_Page1 : IClickableMenu
    {
        private ClickableTextureComponent DisplayRemindersButton;

        private readonly List<ClickableComponent> Labels = new List<ClickableComponent>();
        private readonly List<ClickableTextureComponent> SeasonButtons = new List<ClickableTextureComponent>();
        private readonly List<ClickableTextureComponent> DayButtons = new List<ClickableTextureComponent>();

        private TextBox ReminderTextBox;
        protected ClickableTextureComponent OkButton;

        private string ReminderMessage;
        private string ReminderSeason;
        private int ReminderDate;

        /// <summary>The callback to invoke when the ok button is pressed</summary>
        private readonly Action<string, string, int> OnChanged;

        /// <summary>Construct an instance of the first page.</summary>
        /// <param name="onChanged"></param>
        public NewReminder_Page1(Action<string, string, int> onChanged)
            : base(Game1.viewport.Width / 2 - (632 + IClickableMenu.borderWidth * 2) / 2, Game1.viewport.Height / 2 - (600 + IClickableMenu.borderWidth * 2) / 2 - Game1.tileSize, 632 + IClickableMenu.borderWidth * 2, 600 + IClickableMenu.borderWidth * 2 + Game1.tileSize)
        {
            OnChanged = onChanged;
            SetUpPositions();
        }

        /// <summary>The method called when the game window changes size.</summary>
        /// <param name="oldBounds">The former viewport.</param>
        /// <param name="newBounds">The new viewport.</param>
        public override void gameWindowSizeChanged(Rectangle oldBounds, Rectangle newBounds)
        {
            base.gameWindowSizeChanged(oldBounds, newBounds);
            xPositionOnScreen = Game1.viewport.Width / 2 - (632 + IClickableMenu.borderWidth * 2) / 2;
            yPositionOnScreen = Game1.viewport.Height / 2 - (600 + IClickableMenu.borderWidth * 2) / 2 - Game1.tileSize;
            SetUpPositions();
        }


        /// <summary>Regenerate the UI.</summary>
        private void SetUpPositions()
        {
            Labels.Clear();
            DayButtons.Clear();

            OkButton = new ClickableTextureComponent("OK", new Rectangle(xPositionOnScreen + width - IClickableMenu.borderWidth - IClickableMenu.spaceToClearSideBorder - Game1.tileSize, yPositionOnScreen + height - IClickableMenu.borderWidth - IClickableMenu.spaceToClearTopBorder + Game1.tileSize / 4, Game1.tileSize, Game1.tileSize), "", null, Game1.mouseCursors, Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 46), 1f);
            DisplayRemindersButton = new ClickableTextureComponent(new Rectangle(xPositionOnScreen - Game1.tileSize * 5 - IClickableMenu.spaceToClearSideBorder * 2, yPositionOnScreen + IClickableMenu.spaceToClearTopBorder, Game1.tileSize * 5 + Game1.tileSize / 4 + Game1.tileSize / 8, Game1.tileSize + Game1.tileSize / 8), Utilities.Data.Helper.Content.Load<Texture2D>("assets/DisplayReminders.png", ContentSource.ModFolder), new Rectangle(), 1.5f);
            Labels.Add(new ClickableComponent(new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 1 + 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder - Game1.tileSize / 8 + 8, 1, 1), "Reminder Message: "));

            ReminderTextBox = new TextBox(null, null, Game1.smallFont, Game1.textColor)
            {
                X = xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 1,
                Y = yPositionOnScreen + IClickableMenu.spaceToClearTopBorder + IClickableMenu.borderWidth - Game1.tileSize / 4 + Game1.tileSize / 2 + Game1.tileSize,
                Width = width - IClickableMenu.borderWidth - IClickableMenu.spaceToClearSideBorder - Game1.tileSize * 3 - Game1.tileSize / 4,
                Height = 180
            };
            Game1.keyboardDispatcher.Subscriber = (IKeyboardSubscriber)ReminderTextBox;

            SeasonButtons.Add(new ClickableTextureComponent("Spring", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 1 - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + (int)(Game1.tileSize * 3.10) - Game1.tileSize / 4, Game1.tileSize * 2, Game1.tileSize), "", "", Game1.mouseCursors, new Rectangle(188, 438, 32, 9), Game1.pixelZoom));
            SeasonButtons.Add(new ClickableTextureComponent("Summer", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 3 - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + (int)(Game1.tileSize * 3.10) - Game1.tileSize / 4, Game1.tileSize * 2, Game1.tileSize), "", "", Game1.mouseCursors, new Rectangle(220, 438, 32, 8), Game1.pixelZoom));
            SeasonButtons.Add(new ClickableTextureComponent("Fall", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 5 - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + (int)(Game1.tileSize * 3.1) - Game1.tileSize / 4, Game1.tileSize * 2, Game1.tileSize), "", "", Game1.mouseCursors, new Rectangle(188, 447, 32, 10), Game1.pixelZoom));
            SeasonButtons.Add(new ClickableTextureComponent("Winter", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 7 - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + (int)(Game1.tileSize * 3.1) - Game1.tileSize / 4, Game1.tileSize * 2, Game1.tileSize), "", "", Game1.mouseCursors, new Rectangle(220, 448, 32, 8), Game1.pixelZoom));


            DayButtons.Add(new ClickableTextureComponent("1", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 1 - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 4 - Game1.tileSize / 4, Game1.tileSize * 1, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(8, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("2", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 2 - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 4 - Game1.tileSize / 4, Game1.tileSize * 1, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(16, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("3", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 3 - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 4 - Game1.tileSize / 4, Game1.tileSize * 1, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(24, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("4", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 4 - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 4 - Game1.tileSize / 4, Game1.tileSize * 1, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(32, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("5", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 5 - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 4 - Game1.tileSize / 4, Game1.tileSize * 1, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(40, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("6", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 6 - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 4 - Game1.tileSize / 4, Game1.tileSize * 1, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(48, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("7", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 7 - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 4 - Game1.tileSize / 4, Game1.tileSize * 1, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(56, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("8", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 1 - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 5 - Game1.tileSize / 4, Game1.tileSize * 1, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(64, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("9", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 2 - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 5 - Game1.tileSize / 4, Game1.tileSize * 1, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(72, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("10", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 2.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 5 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(8, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("10", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 3.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 5 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(0, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("11", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 3.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 5 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(8, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("11", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 4.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 5 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(8, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("12", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 4.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 5 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(8, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("12", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 5.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 5 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(16, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("13", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 5.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 5 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(8, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("13", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 6.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 5 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(24, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("14", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 6.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 5 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(8, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("14", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 7.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 5 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(32, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("15", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 0.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 6 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(8, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("15", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 1.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 6 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(40, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("16", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 1.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 6 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(8, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("16", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 2.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 6 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(48, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("17", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 2.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 6 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(8, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("17", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 3.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 6 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(56, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("18", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 3.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 6 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(8, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("18", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 4.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 6 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(64, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("19", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 4.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 6 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(8, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("19", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 5.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 6 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(72, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("20", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 5.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 6 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(16, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("20", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 6.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 6 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(0, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("21", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 6.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 6 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(16, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("21", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 7.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 6 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(8, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("22", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 0.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 7 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(16, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("22", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 1.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 7 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(16, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("23", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 1.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 7 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(16, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("23", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 2.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 7 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(24, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("24", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 2.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 7 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(16, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("24", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 3.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 7 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(32, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("25", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 3.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 7 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(16, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("25", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 4.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 7 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(40, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("26", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 4.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 7 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(16, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("26", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 5.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 7 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(48, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("27", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 5.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 7 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(16, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("27", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 6.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 7 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(56, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("28", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 6.75) - Game1.tileSize / 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 7 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(16, 16, 8, 12), Game1.pixelZoom));
            DayButtons.Add(new ClickableTextureComponent("28", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + (int)(Game1.tileSize * 7.25) - Game1.tileSize / 3, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 7 - Game1.tileSize / 4, Game1.tileSize / 2, Game1.tileSize), "", "", Game1.content.Load<Texture2D>("LooseSprites\\font_bold"), new Rectangle(64, 16, 8, 12), Game1.pixelZoom));
        }

        /// <summary>Handle a button click.</summary>
        /// <param name="name">The button name that was clicked.</param>
        private void HandleButtonClick(string name)
        {
            if (name == null)
                return;

            switch (name)
            {
                // season button
                case "Spring":
                case "Summer":
                case "Fall":
                case "Winter":
                    ReminderSeason = name.ToLower();
                    break;
                // OK button
                case "OK":
                    if ((ReminderDate >= 1 || ReminderDate <= 28) && IsOkButtonReady())
                    {
                        ReminderMessage = ReminderTextBox.Text;
                        OnChanged(ReminderMessage, ReminderSeason, ReminderDate);
                    }
                    break;
                default:
                    ReminderDate = Convert.ToInt32(name);
                    break;
            }
            Game1.playSound("coin");
        }

        /// <summary>
        /// Checks if the page1 inputs are all valid
        /// </summary>
        /// <returns>True if ok button is ready False if not</returns>
        private bool IsOkButtonReady()
        {
            if (ReminderDate != 0 && !string.IsNullOrEmpty(ReminderSeason) && ReminderTextBox.Text != null && !string.IsNullOrEmpty(ReminderTextBox.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>The method invoked when the player left-clicks on the menu.</summary>
        /// <param name="x">The X-position of the cursor.</param>
        /// <param name="y">The Y-position of the cursor.</param>
        /// <param name="playSound">Whether to enable sound.</param>
        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {

            ReminderTextBox.Update();

            //If the season is not selected then the day buttons can't be clicked. Thanks to @Potato#5266 on the SDV discord for this tip.
            if (ReminderSeason == "spring" || ReminderSeason == "summer" || ReminderSeason == "fall" || ReminderSeason == "winter")
            {
                foreach (ClickableTextureComponent button in DayButtons)
                {
                    if (button.containsPoint(x, y))
                    {
                        HandleButtonClick(button.name);
                        button.scale -= 0.5f;
                        button.scale = Math.Max(3.5f, button.scale);
                    }
                }
            }

            foreach (ClickableTextureComponent button in SeasonButtons)
            {
                if (button.containsPoint(x, y))
                {
                    HandleButtonClick(button.name);
                    button.scale -= 0.5f;
                    button.scale = Math.Max(3.5f, button.scale);
                }
            }

            if (OkButton.containsPoint(x, y) && IsOkButtonReady())
            {
                HandleButtonClick(OkButton.name);
                OkButton.scale -= 0.25f;
                OkButton.scale = Math.Max(0.75f, OkButton.scale);
            }

            if (DisplayRemindersButton.containsPoint(x, y))
                Game1.activeClickableMenu = new DisplayReminders(OnChanged);

            ReminderTextBox.Update();
        }


        /// <summary>The method invoked when the player hovers the cursor over the menu.</summary>
        /// <param name="x">The X-position of the cursor.</param>
        /// <param name="y">The Y-position of the cursor.</param>
        public override void performHoverAction(int x, int y)
        {
            foreach (ClickableTextureComponent button in DayButtons)
            {
                button.scale = button.containsPoint(x, y)
                    ? Math.Min(button.scale + 0.02f, button.baseScale + 0.1f)
                    : Math.Max(button.scale - 0.02f, button.baseScale);
            }

            foreach (ClickableTextureComponent button in SeasonButtons)
            {
                button.scale = button.containsPoint(x, y)
                    ? Math.Min(button.scale + 0.02f, button.baseScale + 0.1f)
                    : Math.Max(button.scale - 0.02f, button.baseScale);
            }

            OkButton.scale = OkButton.containsPoint(x, y) && IsOkButtonReady()
                ? Math.Min(OkButton.scale + 0.02f, OkButton.baseScale + 0.1f)
                : Math.Max(OkButton.scale - 0.02f, OkButton.baseScale);

            DisplayRemindersButton.scale = DisplayRemindersButton.containsPoint(x, y)
                ? Math.Min(DisplayRemindersButton.scale + 0.02f, DisplayRemindersButton.baseScale + 0.1f)
                : Math.Max(DisplayRemindersButton.scale - 0.02f, DisplayRemindersButton.baseScale);
        }

        /// <summary>Draw the menu to the screen.</summary>
        /// <param name="b">The sprite batch.</param>
        public override void draw(SpriteBatch b)
        {
            Utilities.Data.Helper.Input.Suppress(Utilities.Data.MenuButton);

            //draw screen fade
            b.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.75f);

            // draw menu box
            Game1.drawDialogueBox(xPositionOnScreen, yPositionOnScreen, width, height, false, true);

            // draw title scroll
            SpriteText.drawStringWithScrollCenteredAt(b, "New Reminder", Game1.viewport.Width / 2, yPositionOnScreen, "New Reminder");

            // draw textbox
            ReminderTextBox.Draw(b, false);

            // draw day buttons
            foreach (ClickableTextureComponent button in DayButtons)
                button.draw(b);

            // draw season buttons
            foreach (ClickableTextureComponent button in SeasonButtons)
                button.draw(b);

            // draw labels
            foreach (ClickableComponent label in Labels)
            {
                Color color = Color.Violet;
                Utility.drawTextWithShadow(b, label.name, Game1.smallFont, new Vector2(label.bounds.X, label.bounds.Y), color);
            }
            foreach (ClickableComponent label in Labels)
            {
                string text = "";
                Color color = Game1.textColor;
                Utility.drawTextWithShadow(b, label.name, Game1.smallFont, new Vector2(label.bounds.X, label.bounds.Y), color);
                if (text.Length > 0)
                    Utility.drawTextWithShadow(b, text, Game1.smallFont, new Vector2((label.bounds.X + Game1.tileSize / 3) - Game1.smallFont.MeasureString(text).X / 2f, (label.bounds.Y + Game1.tileSize / 2)), color);
            }

            // draw OK button
            if (ReminderDate != 0 && !string.IsNullOrEmpty(ReminderSeason) && IsOkButtonReady())
                OkButton.draw(b);
            else
            {
                OkButton.draw(b);
                OkButton.draw(b, Color.Black * 0.5f, 0.97f);
            }

            // draw displayreminder button
            DisplayRemindersButton.draw(b);

            // draw cursor
            drawMouse(b);

        }
    }

    /// <summary>
    /// Second page of the menu that sets the reminder's time
    /// </summary>
    internal class NewReminder_Page2 : IClickableMenu
    {
        // todo remove
        private readonly List<ClickableComponent> Labels = new List<ClickableComponent>();

        private readonly List<ClickableTextureComponent> MinutesAndMeridiemList = new List<ClickableTextureComponent>();
        private readonly List<ClickableTextureComponent> HoursButtons = new List<ClickableTextureComponent>();
        private ClickableComponent CurrentChoiceDisplay;
        private ClickableTextureComponent OkButton;

        /// <summary>Field that contains the Time inputed</summary>
        private int Hours = 0;
        private string Minutes;
        private string Meridiem;


        /// <summary>The callback to invoke when the birthday value changes.</summary>
        private readonly Action<int> OnChanged;

        /// <summary>The callback function that gets called when ok buttong is pressed</summary>
        public NewReminder_Page2(Action<int> onChange)
            : base(Game1.viewport.Width / 2 - (632 + IClickableMenu.borderWidth * 2) / 2, Game1.viewport.Height / 2 - (600 + IClickableMenu.borderWidth * 2) / 2 - Game1.tileSize, 632 + IClickableMenu.borderWidth * 2, 600 + IClickableMenu.borderWidth * 2 + Game1.tileSize)
        {
            OnChanged = onChange;
            SetupPositions();
        }

        /// <summary>Generates the UI</summary>
        private void SetupPositions()
        {
            Labels.Clear();

            // Ok button
            OkButton = new ClickableTextureComponent("OK", new Rectangle(xPositionOnScreen + width - IClickableMenu.borderWidth - IClickableMenu.spaceToClearSideBorder - Game1.tileSize, yPositionOnScreen + height - IClickableMenu.borderWidth - IClickableMenu.spaceToClearTopBorder + Game1.tileSize / 4, Game1.tileSize, Game1.tileSize), "", null, Game1.mouseCursors, Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 46), 1f);

            // Titles
            Labels.Add(new ClickableComponent(new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 1 + 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder - Game1.tileSize / 8 + 8, 1, 1), "Reminder Time: "));
            Labels.Add(new ClickableComponent(new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 1 + 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize * 2 - Game1.tileSize / 8 + 8, 1, 1), "", "error"));

            // Current Choice
            CurrentChoiceDisplay = new ClickableComponent(new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 1 + 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder + Game1.tileSize - Game1.tileSize / 8 + 8, 1, 1), "CurrentTimeDisplay");


            // AM or PM
            MinutesAndMeridiemList.Add(new ClickableTextureComponent("00", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize + 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder - Game1.tileSize / 8 + Game1.tileSize * 6, Game1.tileSize * 2, Game1.tileSize), "", "", Utilities.Data.Helper.Content.Load<Texture2D>("assets/00.png", ContentSource.ModFolder), new Rectangle(), (int)(Game1.pixelZoom * 0.75f)));
            MinutesAndMeridiemList.Add(new ClickableTextureComponent("30", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize + 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder - Game1.tileSize / 8 + Game1.tileSize * 7, Game1.tileSize * 2, Game1.tileSize), "", "", Utilities.Data.Helper.Content.Load<Texture2D>("assets/30.png", ContentSource.ModFolder), new Rectangle(), (int)(Game1.pixelZoom * 0.75f)));

            // Minutes
            MinutesAndMeridiemList.Add(new ClickableTextureComponent("AM", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 4 + 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder - Game1.tileSize / 8 + Game1.tileSize * 6, Game1.tileSize * 2, Game1.tileSize), "", "", Utilities.Data.Helper.Content.Load<Texture2D>("assets/AM.png", ContentSource.ModFolder), new Rectangle(), (int)(Game1.pixelZoom * 0.75f)));
            MinutesAndMeridiemList.Add(new ClickableTextureComponent("PM", new Rectangle(xPositionOnScreen + IClickableMenu.spaceToClearSideBorder + IClickableMenu.borderWidth + Game1.tileSize * 4 + 4, yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder - Game1.tileSize / 8 + Game1.tileSize * 7, Game1.tileSize * 2, Game1.tileSize), "", "", Utilities.Data.Helper.Content.Load<Texture2D>("assets/PM.png", ContentSource.ModFolder), new Rectangle(), (int)(Game1.pixelZoom * 0.75f)));
            
            // Hour
            for (int i = 1; i <= 6; i++)
            {
                HoursButtons.Add(new ClickableTextureComponent($"{i}", new Rectangle(xPositionOnScreen + width / 2 - (int)(Game1.tileSize * 5.0f) + (int)(Game1.tileSize * (i * 1.25f)), yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder - Game1.tileSize / 8 + Game1.tileSize * 3, Game1.tileSize, Game1.tileSize), "", "", Utilities.Data.Helper.Content.Load<Texture2D>($"assets/{i}HourButton.png", ContentSource.ModFolder), new Rectangle(), Game1.pixelZoom));
            }
            for (int i = 7; i <= 12; i++)
            {
                HoursButtons.Add(new ClickableTextureComponent($"{i}", new Rectangle(xPositionOnScreen + width / 2 - (int)(Game1.tileSize * 5.0f) + (int)(Game1.tileSize * ((i - 6) * 1.25f)), yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder - Game1.tileSize / 8 + Game1.tileSize * 4 + Game1.tileSize / 4, Game1.tileSize, Game1.tileSize), "", "", Utilities.Data.Helper.Content.Load<Texture2D>($"assets/{i}HourButton.png", ContentSource.ModFolder), new Rectangle(), Game1.pixelZoom));
            }
        }

        /// <summary>Handles the left clicks on the menu</summary>
        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            foreach (ClickableTextureComponent hourButton in HoursButtons)
            {
                if (hourButton.containsPoint(x, y))
                {
                    Game1.playSound("coin");
                    hourButton.scale -= 0.25f;
                    Hours = Convert.ToInt32(hourButton.name);
                    hourButton.scale = Math.Max(0.75f, Game1.pixelZoom);
                }
            }

            foreach (ClickableTextureComponent button in MinutesAndMeridiemList)
            {
                if (button.containsPoint(x, y))
                {
                    Game1.playSound("coin");
                    button.scale -= 0.25f;
                    if (button.name.EndsWith("M"))
                    {
                        Meridiem = button.name;
                    }
                    else if (button.name.EndsWith("0"))
                    {
                        Minutes = button.name;
                    }
                    button.scale = Math.Max(0.75f, Game1.pixelZoom * 0.75f);
                }
            }

            foreach (ClickableComponent label in Labels)
            {
                if (label.containsPoint(x, y))
                {
                    Game1.playSound("coin");
                    label.scale -= 0.25f;
                    if (label.name.EndsWith("M"))
                    {
                        Meridiem = label.name;
                    }
                    else if (label.name.EndsWith("0"))
                    {
                        Minutes = label.name;
                    }
                    label.scale = Math.Max(0.75f, Game1.pixelZoom);
                }
            }

            if (OkButton.containsPoint(x, y))
            {
                //if (!string.IsNullOrEmpty(TimeTextBox.Text)) return;
                //ReminderTime = Convert.ToInt32(TimeTextBox.Text);

                if (IsOkButtonReady())
                {
                    Game1.playSound("coin");
                    OkButton.scale -= 0.25f;
                    int reminderTime = Convert.ToInt32($"{Hours}{Minutes}");
                    if (Meridiem == "PM")
                        reminderTime += 1200;
                    OnChanged(reminderTime);
                    OkButton.scale = Math.Max(0.75f, OkButton.scale);
                    Game1.exitActiveMenu();
                }
            }

            if (Hours != 0 && !string.IsNullOrEmpty(Minutes) && !string.IsNullOrEmpty(Meridiem))
            {
                if (!IsValidTime())
                {
                    Labels[1].name = "The entered time is invalid";
                }
                else
                {
                    Labels[1].name = "";
                }
            }
        }

        private bool IsValidTime()
        {
            int reminderTime = Convert.ToInt32($"{Hours}{Minutes}");
            if (Meridiem == "PM")
                reminderTime += 1200;
            if (reminderTime <= 2600 && reminderTime >= 600)
                return true;
            else
                return false;
        }

        /// <summary>Defines what to do when hovering over UI elements</summary>
        public override void performHoverAction(int x, int y)
        {
            OkButton.scale = OkButton.containsPoint(x, y) && IsOkButtonReady()
                ? Math.Min(OkButton.scale + 0.02f, OkButton.baseScale + 0.1f)
                : Math.Max(OkButton.scale - 0.02f, OkButton.baseScale);
        }

        /// <summary>
        /// Checks if the page1 inputs are all valid
        /// </summary>
        /// <returns>True if ok button is ready, False if not</returns>
        private bool IsOkButtonReady()
        {
            if (Hours != -1 && Meridiem != null && Minutes != null)
            {
                int reminderTime = Convert.ToInt32($"{Hours}{Minutes}");
                if (Meridiem == "PM")
                    reminderTime += 1200;
                if (reminderTime <= 2600 && reminderTime >= 600)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary> Draws the UI</summary>
        public override void draw(SpriteBatch b)
        {
            // supress the Menu button
            Utilities.Data.Helper.Input.Suppress(Utilities.Data.MenuButton);

            // draw screen fade
            b.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.75f);

            // draw menu box
            Game1.drawDialogueBox(xPositionOnScreen, yPositionOnScreen, width, height, false, true);

            // draw CurrentChoiceDisplay
            if (Hours == 0 && string.IsNullOrEmpty(Minutes) && string.IsNullOrEmpty(Meridiem))
            {
                CurrentChoiceDisplay.name = "Choose the Hour, Minutes and Period";
            }
            else
            {
                CurrentChoiceDisplay.name = $"{Hours}:{Minutes} {Meridiem}";
            }
            Utility.drawTextWithShadow(b, CurrentChoiceDisplay.name, Game1.smallFont, new Vector2(CurrentChoiceDisplay.bounds.X, CurrentChoiceDisplay.bounds.Y), Color.Black);

            foreach (ClickableTextureComponent button in HoursButtons)
            {
                button.draw(b);
            }

            // draw AM/PM and Minutes labels
            foreach (ClickableComponent label in Labels)
            {
                if (label.label == "error")
                    Utility.drawTextWithShadow(b, label.name, Game1.dialogueFont, new Vector2(label.bounds.X, label.bounds.Y), Color.Red);
                else
                    Utility.drawTextWithShadow(b, label.name, Game1.dialogueFont, new Vector2(label.bounds.X, label.bounds.Y), Color.Black);
            }

            // draw AM/PM and Minutes buttons
            foreach (ClickableTextureComponent button in MinutesAndMeridiemList)
            {
                button.draw(b);
            }

            // draw OK button
            if (IsOkButtonReady())
                OkButton.draw(b);
            else
            {
                OkButton.draw(b);
                OkButton.draw(b, Color.Black * 0.5f, 0.97f);
            }

            // draw cursor
            drawMouse(b);
        }
    }
}