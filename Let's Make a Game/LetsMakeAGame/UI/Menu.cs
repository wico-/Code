﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Common;
using TileManager;

namespace UI
{//ToDo Make a "Common UI" class that shares font, viewport, etc. that other classes will use.
    public class Menu : Entity
    {
        public Texture2D background { get; set; }
        public Vector2 position;
        public bool isHidden { get; set; }
        public List<Entity> tiles;
        public List<Button> buttons;

        public Menu(Texture2D background) : base (new Rectangle(0, 0, background.Width, background.Height), background, ENTITY.UI)
        {
            this.background = background;
            position = new Vector2();
            isHidden = true;
            tiles = new List<Entity>();
            buttons = new List<Button>();
        }

        public void LoadContent(eContentManager contentManager)
        {
            tiles.Add(new Tile(contentManager.getTexture("Tiles/engineeringBlock"), new Vector2(1, 1)));
            tiles.Add(new Tile(contentManager.getTexture("Tiles/rockTile"), new Vector2(1, 1)));
            tiles.Add(new Tile(contentManager.getTexture("Tiles/leopardTile"), new Vector2(1, 1)));
            tiles.Add(new Tile(contentManager.getTexture("Tiles/starsTile"), new Vector2(1, 1)));
            tiles.Add(new Tile(contentManager.getTexture("Tiles/spikes"), new Vector2(1, 1)));
            tiles.Add(new Tile(contentManager.getTexture("Tiles/tiles"), new Vector2(1, 1)));
            tiles.Add(new Tile(contentManager.getTexture("Tiles/Block"), new Vector2(1, 1)));
            buttons.Add(new Button(contentManager.getTexture("Buttons/Button"), "Play Map"));
            buttons.Add(new Button(contentManager.getTexture("Buttons/Button"), "Save Map"));
        }

        public void Update(GameTime gameTime, int speedX, int speedY)
        {
            //if (isHidden) position.X = viewport.Width - 20;
            //else
            //{
                //ToDo share out access to Viewport position.X = Game1.viewport.Width - background.Width;
                position.Y = 0;
                int x = (int)this.position.X + 10;
                int y = (int)this.position.Y + 10;
                for (int i = 0; i < tiles.Count; i++)
                {
                    tiles[i].boundary.X = x + ((i % 6) * 80);
                    tiles[i].boundary.Y = y + ((i / 6) * 80);
                }
                buttons[0].boundary.Y = background.Height + (int)position.Y - 10 - buttons[0].texture.Height;
                buttons[0].boundary.X = (int)position.X + 20;
                buttons[1].boundary.Y = buttons[0].boundary.Y;
                buttons[1].boundary.X = (int)buttons[0].boundary.X + buttons[0].texture.Width + 20;
                foreach (Button b in buttons)
                {
                    b.Update(gameTime, 0, 0);
                }
            //}
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, position, Color.White);
            if (!isHidden)
            {
                foreach (Entity e in tiles)
                {
                    e.Draw(spriteBatch);
                }
                foreach (Button b in buttons)
                {
                    b.Draw(spriteBatch);
                }
            }
        }
    }

    public class Button : Entity
    {
        public Texture2D texture;
        public Rectangle boundary;
        public string text;
        private Vector2 textCenter;
        private Vector2 position;
        SpriteFont font;

        public Button(Texture2D texture, string text)
            : base(new Rectangle(0, 0, texture.Width, texture.Height), texture, ENTITY.UI)
        {
            font = Globals.font;
            this.texture = texture;
            this.text = text;
            this.position = new Vector2();
            textCenter = font.MeasureString(text) / 2;
            boundary = new Rectangle(0, 0, (int)font.MeasureString(text).X + 20, (int)font.MeasureString(text).Y + 20);
        }

        //public Button(Vector2 position, Texture2D texture, string text)
        //{
        //    this.texture = texture;
        //    this.text = text;
        //    this.position = new Vector2();
        //    textCenter = Game1.font.MeasureString(text) / 2;
        //    boundary = Game1.getRect(position, texture);
        //}

        public void Update(GameTime gameTime, int speedX, int speedY)
        {
            boundary.X -= speedX;
            boundary.Y -= speedY;
            position.X = boundary.X;
            position.Y = boundary.Y;
            textCenter = new Vector2(position.X + 10, position.Y + 10);// +(font.MeasureString(text) / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, boundary, null, Color.White);
            spriteBatch.DrawString(font, text, textCenter, Color.Red);
        }
    }
}

