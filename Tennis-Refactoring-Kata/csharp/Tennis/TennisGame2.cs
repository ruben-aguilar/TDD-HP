using System.Collections.Generic;

namespace Tennis
{
    public class TennisGame2 : ITennisGame
    {
        private const string EMPTY_SCORE = "";
        private const string LOVE_SCORE = "Love";
        private const string FIFTEEN_SCORE = "Fifteen";
        private const string THIRTY_SCORE = "Thirty";
        private const string FORTY_SCORE = "Forty";
        private const string DEUCE_SCORE = "Deuce";
        private const string PLAYER1_ADVANTAGE_SCORE = "Advantage player1";
        private const string PLAYER2_ADVANTAGE_SCORE = "Advantage player2";
        private const string PLAYER1_WINS_SCORE = "Win for player1";
        private const string PLAYER2_WINS_SCORE = "Win for player2";
        private const string DRAW_SCORE_POSTFIX = "-All";

        private const string SCORE_SEPARATOR_TOKEN = "-";

        private const int ZERO_POINTS = 0;
        private const int FIFTEEN_POINTS = 1;
        private const int THIRTY_POINTS = 2;
        private const int FORTY_POINTS = 3;

        private const int ADVANTAGE_THRESHOLD_POINTS = 4;
        private const int WIN_THRESHOLD_IN_DEUCE = 2;

        private Dictionary<int, string> pointsToScore = new Dictionary<int, string>
        {
            {ZERO_POINTS, LOVE_SCORE},
            {FIFTEEN_POINTS, FIFTEEN_SCORE},
            {THIRTY_POINTS, THIRTY_SCORE},
            {FORTY_POINTS, FORTY_SCORE}
        };


        private int player1Points;
        private int player2Points;

        private string player1Result = "";
        private string player2Result = "";

        public TennisGame2(string player1Name, string player2Name)
        {
            player1Points = 0;
        }

        public string GetScore()
        {
            var score = EMPTY_SCORE;
            bool tiedBelowFourtyPoints = ArePlayersTied() && (player1Points < FORTY_POINTS);

            if (tiedBelowFourtyPoints)
            {
                score = TranslatePlayerScoreToNaturalLanguage(player1Points);
                score += DRAW_SCORE_POSTFIX;
            }

            bool tiedAboveThirtyPoints = ArePlayersTied() && (player1Points > THIRTY_POINTS);
            if (tiedAboveThirtyPoints)
                score = DEUCE_SCORE;

            bool player1HasPoints = player1Points > ZERO_POINTS;
            bool player2HasNoPoints = player2Points == ZERO_POINTS;
            if (player1HasPoints && player2HasNoPoints)
            {
                player1Result = TranslatePlayerScoreToNaturalLanguage(player1Points);

                player2Result = LOVE_SCORE;
                score = player1Result + SCORE_SEPARATOR_TOKEN + player2Result;
            }

            bool player2HasPoints = player2Points > 0;
            bool player1HasNoPoints = player1Points == 0;
            if (player2HasPoints && player1HasNoPoints)
            {
                player2Result = TranslatePlayerScoreToNaturalLanguage(player2Points);

                player1Result = LOVE_SCORE;
                score = player1Result + SCORE_SEPARATOR_TOKEN + player2Result;
            }

            bool isPlayer1Winning = player1Points > player2Points;
            bool player1DoesNotHaveAdvantage = player1Points < ADVANTAGE_THRESHOLD_POINTS;
            if (isPlayer1Winning && player1DoesNotHaveAdvantage)
            {
                player1Result = TranslatePlayerScoreToNaturalLanguage(player1Points);
                player2Result = TranslatePlayerScoreToNaturalLanguage(player2Points);

                score = player1Result + SCORE_SEPARATOR_TOKEN + player2Result;
            }

            bool isPlayer2Winning = player2Points > player1Points;
            bool player2DoesNotHaveAdvantage = player2Points < ADVANTAGE_THRESHOLD_POINTS;
            if (isPlayer2Winning && player2DoesNotHaveAdvantage)
            {
                player1Result = TranslatePlayerScoreToNaturalLanguage(player1Points);
                player2Result = TranslatePlayerScoreToNaturalLanguage(player2Points);

                score = player1Result + SCORE_SEPARATOR_TOKEN + player2Result;
            }

            bool player1HasAdvantage = isPlayer1Winning && (player2Points >= FORTY_POINTS);
            if (player1HasAdvantage)
            {
                score = PLAYER1_ADVANTAGE_SCORE;
            }

            bool player2HasAdvantage = isPlayer2Winning && (player1Points >= FORTY_POINTS);
            if (player2HasAdvantage)
            {
                score = PLAYER2_ADVANTAGE_SCORE;
            }

            if (player1Points >= ADVANTAGE_THRESHOLD_POINTS && player2Points >= ZERO_POINTS && (player1Points - player2Points) >= WIN_THRESHOLD_IN_DEUCE)
            {
                score = PLAYER1_WINS_SCORE;
            }
            if (player2Points >= ADVANTAGE_THRESHOLD_POINTS && player1Points >= ZERO_POINTS && (player2Points - player1Points) >= WIN_THRESHOLD_IN_DEUCE)
            {
                score = PLAYER2_WINS_SCORE;
            }
            return score;
        }

        private string TranslatePlayerScoreToNaturalLanguage(int points)
        {
            if (points >= FORTY_POINTS) return pointsToScore[FORTY_POINTS];

            return pointsToScore[points];
        }

        private bool ArePlayersTied()
        {
            return (player1Points == player2Points);
        }

        public void SetP1Score(int number)
        {
            for (int i = 0; i < number; i++)
            {
                P1Score();
            }
        }

        public void SetP2Score(int number)
        {
            for (var i = 0; i < number; i++)
            {
                P2Score();
            }
        }

        private void P1Score()
        {
            player1Points++;
        }

        private void P2Score()
        {
            player2Points++;
        }

        public void WonPoint(string player)
        {
            if (player == "player1")
                P1Score();
            else
                P2Score();
        }

    }
}

