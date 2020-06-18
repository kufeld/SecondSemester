using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Пасьянс_Охота
{
    public partial class Form1 : Form
    {
        private int firstDeck = -1;
        private Image white;
        private Image winImage;
        private DirectoryInfo imagesDirectory = new DirectoryInfo("Image");
        private Game game = new Game();
        private Dictionary<int, PictureBox> Decks = new Dictionary<int, PictureBox>();
        private Dictionary<Point, int> DecksPosition = new Dictionary<Point, int>();
        private Dictionary<Card, Image> CardsImage = new Dictionary<Card, Image>();
        private Dictionary<string, Image> bitmaps = new Dictionary<string, Image>();

        public Dictionary<Card, Image> GetImageDictionary()
        {
            var dict = new Dictionary<Card, Image>();
            foreach (var e in imagesDirectory.GetFiles("*.png"))
                bitmaps[e.Name] = Image.FromFile(e.FullName);
            dict[new Card(0, 0)] = bitmaps["H6.png"];
            dict[new Card(1, 0)] = bitmaps["H7.png"];
            dict[new Card(2, 0)] = bitmaps["H8.png"];
            dict[new Card(3, 0)] = bitmaps["H9.png"];
            dict[new Card(4, 0)] = bitmaps["HT.png"];
            dict[new Card(5, 0)] = bitmaps["HJ.png"];
            dict[new Card(6, 0)] = bitmaps["HQ.png"];
            dict[new Card(7, 0)] = bitmaps["HK.png"];
            dict[new Card(8, 0)] = bitmaps["HA.png"];

            dict[new Card(0, 1)] = bitmaps["C6.png"];
            dict[new Card(1, 1)] = bitmaps["C7.png"];
            dict[new Card(2, 1)] = bitmaps["C8.png"];
            dict[new Card(3, 1)] = bitmaps["C9.png"];
            dict[new Card(4, 1)] = bitmaps["CT.png"];
            dict[new Card(5, 1)] = bitmaps["CJ.png"];
            dict[new Card(6, 1)] = bitmaps["CQ.png"];
            dict[new Card(7, 1)] = bitmaps["CK.png"];
            dict[new Card(8, 1)] = bitmaps["CA.png"];

            dict[new Card(0, 2)] = bitmaps["D6.png"];
            dict[new Card(1, 2)] = bitmaps["D7.png"];
            dict[new Card(2, 2)] = bitmaps["D8.png"];
            dict[new Card(3, 2)] = bitmaps["D9.png"];
            dict[new Card(4, 2)] = bitmaps["DT.png"];
            dict[new Card(5, 2)] = bitmaps["DJ.png"];
            dict[new Card(6, 2)] = bitmaps["DQ.png"];
            dict[new Card(7, 2)] = bitmaps["DK.png"];
            dict[new Card(8, 2)] = bitmaps["DA.png"];

            dict[new Card(0, 3)] = bitmaps["S6.png"];
            dict[new Card(1, 3)] = bitmaps["S7.png"];
            dict[new Card(2, 3)] = bitmaps["S8.png"];
            dict[new Card(3, 3)] = bitmaps["S9.png"];
            dict[new Card(4, 3)] = bitmaps["ST.png"];
            dict[new Card(5, 3)] = bitmaps["SJ.png"];
            dict[new Card(6, 3)] = bitmaps["SQ.png"];
            dict[new Card(7, 3)] = bitmaps["SK.png"];
            dict[new Card(8, 3)] = bitmaps["SA.png"];

            white = bitmaps["white.png"];
            winImage = bitmaps["salute.png"];
            return dict;
        }

        public Form1()
        {
            var Menu = new MenuStrip();
            ToolStripMenuItem newGame = new ToolStripMenuItem("Новая игра");
            newGame.MouseDown += (s, a) => game.RefreshGame();
            ToolStripMenuItem undo = new ToolStripMenuItem("Отменить");
            undo.MouseDown += (s, a) => game.Undo(); 
            ToolStripMenuItem Rules = new ToolStripMenuItem("Правила");
            Rules.MouseDown += (s, a) =>
            {

                MessageBox.Show("Колода карт из 36 листов тасуется и раскладывается картинками вверх в девять стопок по четыре карты в каждой." + 
                    "Условие выигрыша - убрать все карты." + 
                    "Из числа верхних карт можно убирать пары карт одинакового достоинства.");
            };
            MouseDown += (sender, args) =>
            {
                firstDeck = -1;
            };
            Menu.Items.Add(newGame);
            Menu.Items.Add(undo);
            Menu.Items.Add(Rules);
            Controls.Add(Menu);

            CardsImage = GetImageDictionary();
            game.ChangeNotification += (isWin) => Redraw(isWin);
            int cardHeight = 150;
            int cardWidth = 110;
            InitializeComponent();
            ClientSize = new Size(
                768, 512);
            BackColor = Color.DarkOliveGreen;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var box = new PictureBox
                    {
                        Location = new Point(20 + i * (cardWidth + 10), 30 + j * (cardHeight + 10)),
                        Size = new Size(cardWidth, cardHeight)
                    };
                    var number = i + 3 * j;
                    Decks[number] = box;
                    var position = new Rectangle(box.Location, new Size(cardWidth, cardHeight));
                    DecksPosition[box.Location] = number;
                    box.Image = CardsImage[new Card(0, 0)];
                    box.MouseDown += (s, a) =>
                    {
                        var index = DecksPosition[box.Location];
                        if (index == 9)
                        {
                            game.Undo();
                            return;
                        }

                        if (firstDeck == -1)
                            firstDeck = index;
                        else
                        {
                            game.TryDumpCards(firstDeck, index);
                            firstDeck = -1;
                        }
                    };
                    Controls.Add(box);
                }
            }

            var tenthBox = new PictureBox();
            tenthBox.Location = new Point(20 + 5 * (cardWidth + 10), 20 + 1 * (cardHeight + 10));
            var tenthNumber = 9;
            tenthBox.Size = new Size(cardWidth, cardHeight);
            Decks[tenthNumber] = tenthBox;
            var tenthPosition = new Rectangle(tenthBox.Location, new Size(cardWidth, cardHeight));
            DecksPosition[tenthBox.Location] = tenthNumber;
            Controls.Add(tenthBox);
            game.Start();
        }

        private void Redraw(bool isWin)
        {
            if (isWin)
            {
                this.BackgroundImage = winImage;
            }

            var cards = game.GetTopDecks().ToArray();
            for (int i = 0; i < 10; i++)
            {
                if (cards[i] != null)
                    Decks[i].Image = CardsImage[cards[i]];
                else
                    Decks[i].Image = white;
            }
        }
    }
}
