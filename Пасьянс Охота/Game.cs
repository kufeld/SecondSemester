using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace Пасьянс_Охота
{

    public class Game
    {
        private List<Stack<Card>> Decks;
        private Stack<Tuple<Card, Card>> DumpDeck;
        public delegate void GameChanged(bool isWin);
        public event GameChanged ChangeNotification = win => { };

        public void Start() => RefreshGame();
        public void RefreshGame()
        {
            Decks = GetDecks();
            DumpDeck = new Stack<Tuple<Card, Card>>();
            ChangeNotification(false);
        }

        public void TryDumpCards(int firstDeckIndex, int secondDeckIndex)
        {
            if (Decks[firstDeckIndex].Count * Decks[secondDeckIndex].Count == 0 || firstDeckIndex == secondDeckIndex)
                return;
            if (Decks[firstDeckIndex].Peek().EqualsByValue(Decks[secondDeckIndex].Peek()))
            {
                DumpDeck.Push(Tuple.Create(Decks[firstDeckIndex].Pop().WithChangedStackNumber(firstDeckIndex),
                                    Decks[secondDeckIndex].Pop().WithChangedStackNumber(secondDeckIndex)));
            }
            ChangeNotification(Decks.Sum(s => s.Count) == 0);
        }

        public void Undo()
        {
            if (DumpDeck.Count == 0)
                return;
            var tuple = DumpDeck.Pop();
            Decks[tuple.Item1.stackNumber].Push(tuple.Item1);
            Decks[tuple.Item2.stackNumber].Push(tuple.Item2);
            ChangeNotification(false);
        }

        private List<Stack<Card>> GetDecks()
        {
            var deck = new List<Card>();
            for (int value = 0; value < 9; value++)
                for (int suit = 0; suit < 4; suit++)
                    deck.Add(new Card(value, suit));

            var randomSeed = new Random();
            var random = new Random(randomSeed.Next());
            for (int i = 0; i < 200; i++)
            {
                var firstIndex = random.Next() % 36;
                var secondIndex = random.Next() % 36;
                var card = deck[firstIndex];
                deck[firstIndex] = deck[secondIndex];
                deck[secondIndex] = card;
            }

            var desks = new List<Stack<Card>>();
            for(int i = 0; i < 9; i++)
                desks.Add(new Stack<Card>(deck.GetRange(i * 4, 4)));
            return desks;
        }

        public IEnumerable<Card?> GetTopDecks()
        {
            foreach (var deck in Decks)
            {
                if (!deck.TryPeek(out var result))
                    yield return null;
                else yield return result;
            }

            if (!DumpDeck.TryPeek(out var res))
            {
                yield return null;
                yield break;
            }
            yield return res.Item2;
        }
    }
}
