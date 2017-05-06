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
            var matchScore = EMPTY_SCORE;

            matchScore = ProcessTiedScore();

            matchScore = ProcessLoveScore(matchScore);

            matchScore = ProcessOnePlayerIsWinning(matchScore);

            matchScore = ProcessScoresWithAdvantage(matchScore);

            matchScore = ProcessWinScore(matchScore);

            return matchScore;
        }

        private string ProcessOnePlayerIsWinning(string matchScore)
        {
            bool player1DoesNotHaveAdvantage = player1Points < ADVANTAGE_THRESHOLD_POINTS;
            if (IsPlayer1Winning() && player1DoesNotHaveAdvantage)
            {
                player1Result = TranslatePlayerScoreToNaturalLanguage(player1Points);
                player2Result = TranslatePlayerScoreToNaturalLanguage(player2Points);

                matchScore = player1Result + SCORE_SEPARATOR_TOKEN + player2Result;
            }

            bool player2DoesNotHaveAdvantage = player2Points < ADVANTAGE_THRESHOLD_POINTS;
            if (IsPlayer2Winning() && player2DoesNotHaveAdvantage)
            {
                player1Result = TranslatePlayerScoreToNaturalLanguage(player1Points);
                player2Result = TranslatePlayerScoreToNaturalLanguage(player2Points);

                matchScore = player1Result + SCORE_SEPARATOR_TOKEN + player2Result;
            }

            return matchScore;
        }

        private string ProcessWinScore(string matchScore)
        {
            if (player1Points >= ADVANTAGE_THRESHOLD_POINTS && player2Points >= ZERO_POINTS && (player1Points - player2Points) >= WIN_THRESHOLD_IN_DEUCE)
            {
                matchScore = PLAYER1_WINS_SCORE;
            }
            if (player2Points >= ADVANTAGE_THRESHOLD_POINTS && player1Points >= ZERO_POINTS && (player2Points - player1Points) >= WIN_THRESHOLD_IN_DEUCE)
            {
                matchScore = PLAYER2_WINS_SCORE;
            }

            return matchScore;
        }

        private string ProcessScoresWithAdvantage(string matchScore)
        {
            bool player1HasAdvantage = IsPlayer1Winning() && (player2Points >= FORTY_POINTS);
            if (player1HasAdvantage)
            {
                matchScore = PLAYER1_ADVANTAGE_SCORE;
            }

            bool player2HasAdvantage = IsPlayer2Winning() && (player1Points >= FORTY_POINTS);
            if (player2HasAdvantage)
            {
                matchScore = PLAYER2_ADVANTAGE_SCORE;
            }

            return matchScore;
        }

        private bool IsPlayer1Winning()
        {
            return player1Points > player2Points;
        }

        private bool IsPlayer2Winning()
        {
            return player2Points > player1Points;
        }

        private string ProcessLoveScore(string matchScore)
        {
            bool player1HasPoints = player1Points > ZERO_POINTS;
            bool player2HasNoPoints = player2Points == ZERO_POINTS;

            bool player2HasPoints = player2Points > ZERO_POINTS;
            bool player1HasNoPoints = player1Points == ZERO_POINTS;

            bool onlyOnePlayerHasLoveScore = (player1HasPoints && player2HasNoPoints) ||
                                                    (player2HasPoints && player1HasNoPoints);
            if (onlyOnePlayerHasLoveScore)
            {
                player1Result = TranslatePlayerScoreToNaturalLanguage(player1Points);

                player2Result = LOVE_SCORE;
                matchScore = player1Result + SCORE_SEPARATOR_TOKEN + player2Result;
            }

            return matchScore;
        }

        private string ProcessTiedScore()
        {
            bool isDeuce = ArePlayersTied() && (player1Points >= FORTY_POINTS);

            return TranslateTiedScoreToNaturalLanguage(isDeuce);
        }

        private string TranslateTiedScoreToNaturalLanguage(bool isDeuce)
        {
            string matchScore;
            if (isDeuce)
            {
                matchScore = DEUCE_SCORE;
            }
            else
            {
                matchScore = TranslatePlayerScoreToNaturalLanguage(player1Points);
                matchScore += DRAW_SCORE_POSTFIX;
            }

            return matchScore;
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

