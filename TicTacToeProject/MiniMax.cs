using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeProject
{
    public class MiniMax
    {
        //oyuncu 0 ai 1 boş indexler -1
        public static int[,] board = { { -1, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } };
        public static List<int> scores = new List<int>();
        static int MAX = 1000;
        static int MIN = -1000;
        static int bestMoveI = 0;
        static int bestMoveJ = 0;
        static List<int[]> GetPossibleMoves(int[,] board)
        {
            List<int[]> result = new List<int[]>();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == -1)
                    {
                        result.Add(new int[] { i, j });
                    }
                }
            }
            return result;
        }
        public static int[] AIMove(int i, int j,bool aiFirst)
        {
            if(!aiFirst)
                board[i, j] = 0;
            var possibleMoves = GetPossibleMoves(board); //oyun tahtasındaki boş hücrelerin indexlerini alıyoruz
            int bestScore = int.MinValue;
            foreach (var move in possibleMoves)
            {
                //boş indexlerin üzerinde foreach ile dönüp eğer ai oraya oynarsa olası sonuçları görmek için minimax algortimasını çalıştırıyozu
                board[move[0], move[1]] = 1;
                var score = Minimax(0, false, board);
                board[move[0], move[1]] = -1;
                //daha iyi bir score elde ettiysek bestScore u güncelliyoruz
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMoveI = move[0];
                    bestMoveJ = move[1];
                }
            }
            board[bestMoveI, bestMoveJ] = 1;
            int [] result = new int[2];
            result[0] = bestMoveI;
            result[1] = bestMoveJ;
            return result;
        }
        //maximizingPlayer false ise oyuncunun hamle sırası değil ise ai ın hamle sırası demektir
        static int Minimax(int depth, bool maximizingPlayer, int[,] board)
        {
            var possibleMoves = GetPossibleMoves(board);
            int winner = CheckWinner(board);//eğer ai kazandıysa 10 player kazandıysa -10 puan, beraberlik durumunda 0 dönecek
            if (winner == 10)
                return 10 - depth;
            if (winner == -10)
                return -10 + depth;
            if (possibleMoves.Count <= 0 & winner == 0)//oyun alanında boş hücre kalmadıysa ve beraberlik durumu varsa
                return 0 + depth;
            if (maximizingPlayer)//eğer ai ın sırası ise maximizin yapılacak
            {
                int best = MIN;
                foreach (var move in possibleMoves)
                {
                    board[move[0], move[1]] = 1;//ai boş bir yere oynadı
                    int score = Minimax(depth + 1, false, board);//üst satırdaki hamleden sonra player ın hamlesi için metod recusive olarak tekrar çağırılır.
                    best = Math.Max(best, score);//yeni score best score dan iyiyse yeni best score olur
                    board[move[0], move[1]] = -1;//yapılan hamle geri alınır
                }
                return best;
            }
            else
            {
                int best = MAX;
                foreach (var move in possibleMoves)
                {
                    board[move[0], move[1]] = 0;//player boş bir yere oynadı
                    int score = Minimax(depth + 1, true, board);//üst satırdaki hamleden sonra ai ın hamlesi için metod recusive olarak tekrar çağırılır.
                    best = Math.Min(best, score);//yeni score best score dan iyiyse yeni best score olur
                    board[move[0], move[1]] = -1;//yapılan hamle geri alınır

                }
                return best;
            }
        }
        private static int CheckWinner(int[,] board)
        {
            //dikey, yatay ve diagonal olarak değerleri array e atıyoruz
            int[] results =
            {
                board[0,0],board[0,1],board[0,2],
                board[1,0],board[1,1],board[1,2],
                board[2,0],board[2,1],board[2,2],
                board[0,0],board[1,0],board[2,0],
                board[0,1],board[1,1],board[2,1],
                board[0,2],board[1,2],board[2,2],
                board[0,0],board[1,1],board[2,2],
                board[0,2],board[1,1],board[2,0]
            };
            for (int i = 0; i < results.Length; i += 3)
            {
                //dikey yatay yada diagonal olarak 0 lar sıralandıysa player kazanmıştır aksi durumda ai kazanmıştır
                if (results[i] == 0 & results[i + 1] == 0 & results[i + 2] == 0)
                    return -10;
                else if (results[i] == 1 & results[i + 1] == 1 & results[i + 2] == 1)
                    return 10;
            }
            //beraberlik durumu
            return 0;
        }
        public static int FinishGame()
        {
            int score = CheckWinner(board);
            if (GetPossibleMoves(board).Count <= 0 & score == 0)
            {
                return score;
            }
            else if(score==10)
                return score;
            else if (score == -10)
                return score;
            return -1;
        }
        public static void ReInitializeBoard()
        {
            board =new int[,]{ { -1, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } };
        }
    }
}
